using UnityEngine;

/*----------------------------------------
  TODO LIST:
- While sprinting and holding both sprint and crouch buttons, state swaps between sprinting and sneaking. It should not do this.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class SprintState : BaseMovementState {
        private Vector3 movementDirection;
        private bool continueSprint;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public SprintState(PlayerCharacter p) : base(p) { }
        public SprintState(BaseMovementState previous) : base(previous) { }
        

        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "sprinting";
        }

        public override void FixedUpdate() {
            if(player.doSprint) {
                continueSprint = true;
            }

            if(!player.doMovement || !player.MovementDirectionAllowsSprint()) {
                continueSprint = false;
            }

            if(continueSprint) {
                movementDirection = StandardMovementUpdate(player.common.SprintSpeed, player.common.SprintAcceleration);
            }
            else {
                ReduceSidewaysSliding(movementDirection);
            }
            
            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckMicroGravityStart(true) || CheckSwim() || CheckAirborn() || CheckSlide(player.common.SprintStartSlideThreshold) || CheckCrouch() || CheckEnd()) {
                return;
            }
        }

        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(player.HorizontalSpeed <= player.common.WalkSpeed && !continueSprint) {
                player.SetMovementState(new WalkState(this), "Player stopped sprinting");
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
            return false;
        }

        public override bool CanCrouch() {
            return false;
        }

        public override bool CanBrace() {
            return true;
        }

        public override bool CanFocus() {
            return true;
        }
    }
}