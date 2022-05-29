using UnityEngine;

/*----------------------------------------
  TODO LIST:
- Correct behavior of airborn movement input.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class AirbornState : BaseMovementState {
        private bool alreadyJumped;
        private Vector3 startPosition;
        private Vector3 highestPosition;
        private bool isFalling; // TODO: Set to true once player's agnostic height has begun to decrease.

        private Vector3 startDirection; // Direction of movement input when jump began. Magnitude ranges from 0 to 1.
        private float startSpeed; // Horizontal speed when jump began.

        private bool endState;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public AirbornState(PlayerCharacter p, bool jumped) : base(p) {
            alreadyJumped = jumped;
        }

        public AirbornState(BaseMovementState previous, bool jumped) : base(previous) { 
            alreadyJumped = jumped;
        }

        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            startPosition = player.rb.position;
            highestPosition = startPosition;
            endState = false;

            startSpeed = player.HorizontalSpeed;
            //startDirection = player.movementDirection; // TODO: Get movement direction at time of jump.

            //Vector3 b = new Vector3(-2.45f, 3.2f, 0);
            //b.Normalize();
            //CalculateCollision(new Vector3(3, -4, 0), new Vector3(0, 1, 0));
            //CalculateCollision(new Vector3(0, -5, 0), b);
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            //Debug.Log("Jump height: " + (highestPosition.y - startPosition.y));
            float fallDistance = highestPosition.y - player.rb.position.y;

            base.OnStateExit();
        }

        public override void OnCollisionEnter(Collision collision) { // Does this even get used?
            // if(!hasLanded & !player.JumpThisFrame) {
            //     ContactPoint contact = collision.contacts[0];
            //     Debug.Log("Fall Angle: " + Vector3.Angle(player.gravityNormal, contact.point - contact.thisCollider.transform.position));
            //     if(Vector3.Angle(player.gravityNormal, contact.point - contact.thisCollider.transform.position) > 135f) {
            //         hasLanded = true;
            //     }
            // }
            
            base.OnCollisionEnter(collision);
        }
        
        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "airborn";
        }

        public override void CheckStateEnd() {
            if(CheckLanded()) { // TODO: Swim, micro gravity, etc
                return;
            }
        }

        public override void FixedUpdate() { // TODO: Allow limited control while airborn.
            if(player.doJumpThisFrame) {
                player.rb.AddForce(player.gravityNormal * (player.common.JumpStrength * player.GravityJumpMultiplier), ForceMode.VelocityChange);
                player.doJumpThisFrame = false;
                alreadyJumped = true;
            }
            else if(player.OnTrueGround) {
                Debug.Log("Player has landed!");
                endState = true;
            }
            else {
                Vector3 currentPosition = player.rb.position;
                if(currentPosition.y > highestPosition.y) { // TODO: Height is not always on the Y axis.
                    highestPosition = currentPosition;
                }


                UpdateVelocity();
            }

            // TODO: implement fall damage
            // TODO: implement bracing to reduce fall damage
            // TODO: Hitting ground at angle reduces fall damage
            // TODO: Hitting surface at angle >45 degrees results in sliding

            //if(player.OnSurface && !player.JumpThisFrame) { // TODO: Only check if player is moving downward.
                // if (impact velocity too high) {player is dead; return;}
                // if (relative velocity is too high) {player.SetMovementState(new TumbleState(this)); return;}
                // if (angle inpact is too accute ) {player.SetMovementState(new SlideState(this)); return}
                


                //player.SetMovementState(new WalkState(this));
            //   return;
            //}
            
            player.ApplyGravity();
            base.FixedUpdate();
        }

        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckLanded() {
            if(endState) {
                SwitchToGroundState();
                return true;
            }
            RaycastHit hit = player.UpdateGroundedStatus();
        // Debug.Log("Hit landed");
            if(player.OnSurface) {
                //Debug.Log("Landing.");
                Vector3 normal = hit.normal;
                Vector3 velocity = player.rb.velocity;

                Vector3 verticalVector = Vector3.Dot(velocity, normal) * normal;
                Vector3 horizontalVector = velocity - verticalVector;

                //player.rb.AddForce(-horizontalVector-verticalVector, ForceMode.Impulse);
                //Debug.Log("Initial real vector: " + player.rb.velocity + "(" + player.rb.velocity.magnitude + ")");
                
                // if(player.customLandingPhysics) {
                //     Vector3 newHorizontal = Vector3.ProjectOnPlane(velocity, normal);
                //     Vector3 newVertical = Vector3.ProjectOnPlane(velocity, newHorizontal.normalized);
                //     //player.rb.AddForce(horizontalVector, ForceMode.Impulse);
                //     //Debug.Log("About to add velocity: " + newHorizontal.ToString() + " (" + newHorizontal.magnitude.ToString() + ")");
                //     //player.rb.AddForce(horizontalVector-velocity, ForceMode.VelocityChange);
                // }
                
                //Debug.Log("Horizontal vector: " + horizontalVector + "(" + horizontalVector.magnitude + ")");
                //Debug.Log("Vertical vector" + verticalVector + "(" + verticalVector.magnitude + ")");
            // Debug.Log("Sum vector: " + (horizontalVector-verticalVector) + "(" + (horizontalVector-verticalVector).magnitude + ")");
            // Debug.Log("Real vector: " + player.rb.velocity + "(" + player.rb.velocity.magnitude + ")");
                //player.rb.AddForce(, ForceMode.Impulse);

                //Debug.Log("Hit ground with speed: " + verticalVector.magnitude);
                //Debug.Log("Result Velocity: " + player.rb.velocity.magnitude);
                //CalculateCollision(velocity, normal);

                //float angle = Vector3.Angle(normal, velocity);
                //float forwardPercent = angle/90f;
                //float fallPercent = 1f - forwardPercent;

                //Debug.Log("Fell on surface at angle: " + angle + "; Fall%: " + fallPercent + "; Forward%: " + forwardPercent);
                
                return true;
            }

            return false;
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            if(!alreadyJumped && duration < player.common.AirbornGraceJumpWindow) {
                return true;
            }
            return false;
        }

        public override bool CanSprint() {
            return false;
        }

        public override bool CanCrouch() {
            return false;
        }

        public override bool CanBrace() {
            return true;
        }



        /*----------------------------------------
        |   PRIVATE FUNCTIONS
        ----------------------------------------*/
        private void UpdateVelocity() {
            Vector3 lookDirection = Vector3.Cross(player.lookTransform.right, player.transform.up);
            Vector3 targetVelocity = Vector3.zero;
            // float deltaVelocity = 0;
            Vector3 movementDirection = Quaternion.LookRotation(lookDirection, player.transform.up) * player.movementInput;
            float magnitudeMultiplier = 3f;
            player.rb.AddForce(movementDirection*Time.fixedDeltaTime*magnitudeMultiplier, ForceMode.VelocityChange);
            // TODO: Prevent (when input vector magnitude is 1) additional acceleration in the direction of initial jump. 
            // If input vector magnitude is not 1, limit speed as if it were magnitude 1.
        }


        /*----------------------------------------
        |   OLD CODE
        ----------------------------------------*/

        // public void CalculateCollision(Vector3 relativeVelocity, Vector3 objectSurfaceNormal) {
        //     Vector3 verticalVector = Vector3.Dot(relativeVelocity, objectSurfaceNormal) * objectSurfaceNormal;
        //     Debug.Log(relativeVelocity + " " + objectSurfaceNormal);

        //     Vector3 horizontalVector = relativeVelocity - verticalVector;
        //     //player.rb.AddForce(-horizontalVector, ForceMode.Impulse);
        //     // NOT NORMALIZED

        //     //verticalVector.x = Mathf.Sqrt(verticalVector.x);
        //    //verticalVector.y = Mathf.Sqrt(verticalVector.y);
        //     //verticalVector.z = Mathf.Sqrt(verticalVector.z);

        //     //horizontalVector.x = Mathf.Sqrt(horizontalVector.x);
        //     //horizontalVector.y = Mathf.Sqrt(horizontalVector.y);
        //     //horizontalVector.z = Mathf.Sqrt(horizontalVector.z);

        //     // Divide by square root of initial vector

        //     //float length = (relativeVelocity.magnitude) / Mathf.Sqrt(relativeVelocity.magnitude);

        //     //verticalVector *= magnitude/2;
        //     //horizontalVector *= magnitude/2;
        //     Debug.Log("Vertical: " + verticalVector.ToString("F4") + "; Horizontal: " + horizontalVector.ToString("F4"));
        //     Debug.Log("V: " + verticalVector.magnitude.ToString("F4") + "; H: " + horizontalVector.magnitude.ToString("F4"));

        // }
    }
}