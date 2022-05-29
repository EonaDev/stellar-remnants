using UnityEngine;

namespace StellarRemnants.Units {
    public class UseMenuState : BaseActivityState {

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public UseMenuState(PlayerCharacter p) : base(p) { }
        public UseMenuState(BaseActivityState previous) : base(previous) { }


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            // Enable menu controls
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            // Disable menu controls
            base.OnStateExit();
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "on menu";
        }


        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanLookAround() {
            return false;
        }
    }
}