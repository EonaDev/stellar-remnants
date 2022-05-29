using UnityEngine;
using StellarRemnants.Units;

/*----------------------------------------
  TODO LIST:
- Set active tool in SelectOption(...)
----------------------------------------*/
namespace StellarRemnants.Units {
    public class IdleState : BaseActivityState {

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public IdleState(PlayerCharacter p) : base(p) { }
        public IdleState(BaseActivityState previous) : base(previous) { }


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            player.controller.SetIdleControlMode(true);
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            player.controller.SetIdleControlMode(false);
            base.OnStateExit();
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "idle";
        }

        public override void CheckStateEnd() {
            if(CheckFocus()) {
                return;
            }
        }

        public override bool OptionPress(int selection) {
            // TODO: Actually select tool and set selected on player
            if(selection > 0 && selection <= 4) {
                Debug.Log("Selected tool: " + selection);
                return true;
            }
            
            return false;
        }

        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        // No end-state functions unique to Idle.

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanFocus() {
            return true;
        }

        public override bool CanSprint() {
            return true;
        }

        public override bool CanJump() {
            return true;
        }

        public override bool CanCrouch() {
            return true;
        }

        public override bool CanBrace() {
            return true;
        }
    }
}