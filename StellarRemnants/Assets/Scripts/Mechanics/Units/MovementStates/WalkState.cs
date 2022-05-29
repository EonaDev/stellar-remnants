using UnityEngine;

namespace StellarRemnants.Units {
    public class WalkState : BaseMovementState {
        private Vector3 movementDirection;
        private float decelerationDuration;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public WalkState(PlayerCharacter p) : base(p) { }
        public WalkState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "walking";
        }

        public override void FixedUpdate() {
            if(player.doMovement) {
                movementDirection = StandardMovementUpdate(player.common.WalkSpeed, player.common.WalkAcceleration);
                decelerationDuration = 0;
            }
            else {
                ReduceSidewaysSliding(movementDirection);
                decelerationDuration += Time.fixedDeltaTime;
                // TODO: Player takes too long to decelerate down hill. Maybe switch to sliding if deceleration is too slow?
            }
            
            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckMicroGravityStart(true) || CheckSwim() || CheckAirborn() || CheckSlide(player.common.WalkStartSlideThreshold) || CheckSprint() || CheckCrouch() || CheckEnd()) {
                return;
            }
        }


        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(decelerationDuration > player.common.WalkDecelerationWindow) {
                player.SetMovementState(new SlideState(this, player.common.WalkEndSlideThreshold), "Player took too long to decelerate and began sliding");
                return true;
            }

            if(!player.doMovement && !player.HasVelocity) {
                player.SetMovementState(new StandState(this), "Player stopped walking");
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

        public override bool CanCrouch() {
            return true;
        }

        public override bool CanSprint() {
            return true;
        }

        public override bool CanBrace() {
            return true;
        }

        public override bool CanFocus() {
            return true;
        }
    }
}