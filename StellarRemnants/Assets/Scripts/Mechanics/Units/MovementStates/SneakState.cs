using UnityEngine;

/*----------------------------------------
  TODO LIST:
- Make player visibly crouch & shift perspective location.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class SneakState : BaseMovementState {
        private Vector3 movementDirection;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public SneakState(PlayerCharacter p) : base(p) { }
        public SneakState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "sneaking";
        }

        public override void FixedUpdate() {
            if(player.doMovement) {
                movementDirection = StandardMovementUpdate(player.common.SneakSpeed, player.common.SneakAcceleration);
            }
            else {
                ReduceSidewaysSliding(movementDirection);
            }
            
            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckSwim() || CheckAirborn() || CheckSlide(player.common.SneakStartSlideThreshold) || CheckEnd()) {
                return;
            }
        }

        
        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(player.doCrouch) {
                if(player.doMovement || player.HasVelocity) {
                    return false;
                }
                else {
                    player.SetMovementState(new CrouchState(this), "Player stopped moving while sneaking");
                    return true;
                }
            }
            else if(player.HasHeadClearance) {
                SwitchToGroundState();
                return true;
            }
            return false;
        }

        
        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            return player.HasHeadClearance;
        }

        public override bool CanSprint() {
            return true;
        }

        public override bool CanFocus() {
            return true;
        }
    }
}