using UnityEngine;

/*----------------------------------------
  TODO LIST:
- Improve method for friction damage and get rid of magic numbers.
- Re-evaluate sliding behavior.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class SlideState : BaseMovementState {
        private BaseMovementState returnState;
        
        private float returnSpeedThreshold;


        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public SlideState(PlayerCharacter p) : base(p) { }
        public SlideState(BaseMovementState previous, float speedLimit) : base(previous) { 
            returnState = previous;
            returnSpeedThreshold = speedLimit;
        }


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            player.capsuleCollider.material.dynamicFriction = player.common.SlideFriction;
            player.capsuleCollider.material.staticFriction = player.common.SlideFriction;
            player.capsuleCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            player.capsuleCollider.material = player.capsuleCollider.sharedMaterial;
            base.OnStateExit();
        }

        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "sliding";
        }

        public override void FixedUpdate() {
            // Apply friction damage.
            if(player.TrueSpeed > 10f) {
                player.DoDamage(player.TrueSpeed - 10f);
            }

            // Apply down-slope velocity if slope is too steep.
            if(Vector3.Angle(player.gravityNormal, player.groundNormal) > player.common.SlideEndAngle) {
                Vector3 downSlope = Vector3.Cross(Vector3.Cross(player.gravityNormal, player.groundNormal), player.groundNormal);
                player.rb.AddForce(downSlope * player.common.SlideDownSlopeVelocityMultiplier * -player.gravity, ForceMode.VelocityChange);
            }


            // Vector3 targetVelocity = player.parentVelocity;
            // Vector3 velocityChange = targetVelocity - player.rb.velocity;
            // velocityChange = Vector3.ProjectOnPlane(velocityChange, player.transform.up);
            // float magnitude = velocityChange.magnitude;
            // if(magnitude > player.slideDeceleration) {
            //     if(player.DoBrace) {
            //         Debug.Log("Bracing");
            //         velocityChange *= ((player.slideDeceleration * player.slideBraceDecelerationMultiplier) / magnitude);
            //     }
            //     else {
            //         velocityChange *= (player.slideDeceleration / magnitude);
            //     }
            // }
            // player.rb.AddForce(velocityChange, ForceMode.VelocityChange);

            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckMicroGravityStart(true) || CheckSwim() || CheckAirborn() || CheckTumble() || CheckEnd()) {
                return;
            }
        }

        
        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckTumble() {
            if(player.HorizontalSpeed > player.common.SlideTumbleThreshold) {
                player.SetMovementState(new TumblingMovementState(this), "Player sliding speed exceeded tumbling threshold");
                return true;
            }
            return false;
        }

        private bool CheckEnd() {
            if(Vector3.Angle(player.gravityNormal, player.groundNormal) > (player.doBrace ? player.common.SlideEndAngleBracing : player.common.SlideEndAngle)){
                return false;
            }

            if(player.doMovement) {
                if(player.doSprint && player.HorizontalSpeed <= player.common.SprintSpeed) {
                    player.SetMovementState(new SprintState(this), "Player exited slide into sprinting");
                    return true;
                }
                else if(player.doCrouch && player.HorizontalSpeed <= player.common.SneakSpeed) {
                    player.SetMovementState(new SneakState(this), "Player exited slide into crouch walking");
                    return true;
                } 
                else if(player.HorizontalSpeed <= player.common.WalkSpeed) {
                    player.SetMovementState(new WalkState(this), "Player exited slide into walking");
                    return true;
                }
            }
            else {

            }

            // if(player.HorizontalSpeed < 3f) { //returnSpeedThreshold
            //     if(returnState.GetType() == typeof(AirbornState)) {
            //         SwitchToGroundState();
            //     }
            //     else {
            //         player.SetMovementState(returnState, "Player completed slide and returned to previous state");
            //     }
                
            //     return true;
            // }
            return false;
        }


        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            return player.HasHeadClearance;
        }

        public override bool CanBrace() {
            return true;
        }

        public override bool CanFocus() {
            return true;
        }
    }
}