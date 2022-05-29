using UnityEngine;
using StellarRemnants.Units;

/*----------------------------------------
  TODO LIST:
- Enable/disable appropriate controls when entering/exiting state.
- Determine how controls are transferred to vehicle/whatever. Do they pass through here or somewhere else? Through the movement state? Implement "Operable" class and add ref here?
----------------------------------------*/

namespace StellarRemnants.Units {
    public class OperateState : BaseActivityState {

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public OperateState(PlayerCharacter p) : base(p) { }
        public OperateState(BaseActivityState previous) : base(previous) { }


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            //player.controller.SetIdleControlMode(true); // TODO: Set controls to whatever is appropriate
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            //player.controller.SetIdleControlMode(false); // TODO: Set controls to whatever is appropriate
            base.OnStateExit();
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "operating";
        }

        public override void CheckStateEnd() {
            if(CheckEnd()) {
                return;
            }
        }

        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            return true;
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanLookAround() {
            return false;
        }
    }
}