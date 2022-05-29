using UnityEngine;

namespace StellarRemnants.Units {
    public class UncontrolledSlideState : BaseMovementState {
        private BaseMovementState returnState;
        private float returnSpeedThreshold;
        
        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public UncontrolledSlideState(PlayerCharacter p) : base(p) { }
        public UncontrolledSlideState(BaseMovementState previous, float speedLimit) : base(previous) { 
            returnState = previous;
            returnSpeedThreshold = speedLimit;
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "uncontrolled slide";
        }

        public override void FixedUpdate() {
            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            //if(CheckSwim() || CheckAirborn() || CheckTumble() || CheckEnd()) {
            //    return;
            //}
        }

        
        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanBrace() {
            return true;
        }
    }
}