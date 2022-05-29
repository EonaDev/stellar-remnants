using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------
  TODO LIST:
- Add action governor function for whether or not player can catch items while bracing in state.
----------------------------------------*/

namespace StellarRemnants.Units {
    public abstract class BaseMovementState {
        protected PlayerCharacter player;
        protected float duration;

        public virtual void CheckStateEnd() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {
            duration += Time.fixedDeltaTime;
        }

        public virtual void OnStateEnter() {
            //Debug.Log("Real vector: " + player.rb.velocity + "(" + player.rb.velocity.magnitude + ")");
        }

        public virtual void OnStateExit() {

        }

        public BaseMovementState(PlayerCharacter p) {
            this.player = p;
        }

        public BaseMovementState(BaseMovementState previousState) {
            this.player = previousState.player;
        }

        /**
            Standard movement update. It will accelerate the player until a specified speed is reached.
        **/
        protected Vector3 StandardMovementUpdate(float speed, float acceleration) {
            Vector3 lookDirection = Vector3.Cross(player.lookTransform.right, player.transform.up);
            Vector3 targetVelocity = Vector3.zero;
            float deltaVelocity = 0;
            Vector3 movementDirection = Quaternion.LookRotation(lookDirection, player.transform.up) * player.movementInput;
            

            if(player.HorizontalSpeed <= speed) {
                float movementMagnitude = speed * GetDirectionalSpeedMultipier();

                Quaternion slopeRotation = Quaternion.FromToRotation(player.transform.up, player.groundNormal);
                //Vector3 movementDirection = Quaternion.LookRotation(lookDirection, player.transform.up) * player.movementInput;
                
                targetVelocity += slopeRotation * (movementDirection * movementMagnitude);
                deltaVelocity = acceleration * player.frictionCoefficient;
            }


            Vector3 velocityChange = targetVelocity - player.rb.velocity;
            float magnitude = velocityChange.magnitude;

            if(magnitude > deltaVelocity) {
                velocityChange *= (deltaVelocity / magnitude);
                
            }
            player.rb.AddForce(velocityChange*Time.fixedDeltaTime*100f, ForceMode.VelocityChange);
            //player.rb.AddRelativeForce(velocityChange*Time.fixedDeltaTime*100f, ForceMode.VelocityChange);
            //player.rb.velocity
            
            return movementDirection;

        }

        /**
            Reduces sideways sliding when coming to a stop.
        **/
        protected void ReduceSidewaysSliding(Vector3 lastMovementDirection) {
            // TODO: If player lets off one input key slightly before the other, the correction is applied incorrectly. Fix this.

            Vector3 relativeSlideVector = Quaternion.FromToRotation(lastMovementDirection, Vector3.forward) * (player.rb.velocity);
            Vector3 correction = Vector3.Cross(lastMovementDirection, player.gravityNormal) * relativeSlideVector.x;
            player.rb.AddForce(correction, ForceMode.VelocityChange);
        }




        protected bool CheckSwim() {
            return false;
        }

        protected bool CheckAirborn() {
            player.UpdateGroundedStatus();

            if(player.doJumpThisFrame) {
                player.OnSurface = false;
                player.SetMovementState(new AirbornState(this, true), "Player jumped");
                return true;
            }
            else {
                player.UpdateGroundedStatus();
                if(!player.OnSurface) {
                    player.SetMovementState(new AirbornState(this, false), "Player fell");
                    return true;
                }
            }

            return false;
        }

        protected bool CheckSlide(float slideThreshold) {
            if(player.HorizontalSpeed >= slideThreshold) {
                player.SetMovementState(new SlideState(this, slideThreshold), "Player speed (" + player.HorizontalSpeed + ") exceeded slide threshold (" + slideThreshold + ") and began sliding");
                return true;
            }

            float angle = Vector3.Angle(player.gravityNormal, player.groundNormal);
            if(angle > player.common.SlideStartAngle){
                player.SetMovementState(new SlideState(this, slideThreshold), "Ground slope (" + angle + ") exceeded slide angle threshold (" + player.common.SlideStartAngle + ") and began sliding");
                return true;
            }

            return false;
        }

        protected bool CheckWalk() {
            if(player.doMovement) {
                player.SetMovementState(new WalkState(this), "Player began walking");
                return true;
            }

            return false;
        }

        protected bool CheckCrouch() {
            if(player.doCrouch) {
                if(player.doMovement || player.HasVelocity) {
                    player.SetMovementState(new SneakState(this), "Player began crouch walking");
                }
                else {
                    player.SetMovementState(new CrouchState(this), "Player began crouching");
                }
                return true;
            }

            return false;
        }

        protected bool CheckSprint() {
            if(player.doSprint && player.doMovement && player.MovementDirectionAllowsSprint()) {
                player.SetMovementState(new SprintState(this), "Player began sprinting through normal means");
                return true;
            }

            return false;
        }

        protected bool CheckMicroGravityStart(bool grounded) {
            if(player.gravity > player.common.MicroGravityThreshold) { // Use greater-than because gravity is negative.
                if(grounded) {
                    player.SetMovementState(new MicroGravityClimbState(this), "Player entered microgravity while on a surface");
                }
                else {
                    player.SetMovementState(new MicroGravityFloatState(this), "Player entered microgravity while airborn");
                }

                return true;
            }
            return false;
        }

        protected bool CheckMicroGravityEnd(bool grounded) {
            if(player.gravity <= player.common.MicroGravityThreshold) { // Use greater-than because gravity is negative.
                if(grounded) {
                    SwitchToGroundState();
                }
                else {
                    player.SetMovementState(new AirbornState(this, false), "Player exited gravity field while floating");
                }
                
                return true;
            }
            return false;
        }

        public void SwitchToGroundState() {
            // Swap to sprint/walk/crouch/crouchwalk/stand based on appropriate conditions. Mostly used when landing.
            if(player.doMovement) {
                if(player.doSprint) {
                    player.SetMovementState(new SprintState(this), "Player defaulted to sprinting");
                }
                else if(player.doCrouch) {
                    player.SetMovementState(new SneakState(this), "Player defaulted to sneaking");
                }
                else {
                    player.SetMovementState(new WalkState(this), "Player defaulted to walking");
                }
            }
            else {
                if(player.doCrouch) {
                    player.SetMovementState(new CrouchState(this), "Player defaulted to crouching");
                }
                else {
                    player.SetMovementState(new StandState(this), "Player defaulted to standing");
                }
            }
        }

        public virtual string GetStateName() {
            return "";
        }

        public virtual bool CanSprint() {
            return false;
        }

        public virtual bool CanJump() {
            return false;
        }

        public virtual bool CanCrouch() {
            return false;
        }

        public virtual bool CanBrace() {
            return false;
        }

        public virtual bool CanFocus() {
            return false;
        }

        private float GetDirectionalSpeedMultipier() {
            // TODO: Modified speed may be affecting state transitions. Consider updating multiplier once per tick and modify state transition speed values with directional speed multiplier.

            float forward = player.movementInput.z;
            float ratio;
            float zValue;

            if(forward < 0) {
                ratio = -forward / (-forward + Mathf.Abs(player.movementInput.x));
                zValue = player.common.ReverseSpeedMultiplier;
            }
            else {
                ratio = forward / (forward + Mathf.Abs(player.movementInput.x));
                zValue = 1f;
            }
            
            return ratio * zValue + (1f - ratio) * player.common.StrafeSpeedMultiplier;
        }

        public virtual void OnCollisionEnter(Collision collision) {}
    }

}
