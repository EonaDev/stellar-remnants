using UnityEngine;
using StellarRemnants.Interact;

/*----------------------------------------
  TODO LIST:
- Exit seat with movement input when not operating.
- Make class play nicely with OperateState control state.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class SeatedState : BaseMovementState {
        public Seat seat;
        public bool operating; // Whether or not player is operating vehicle/controls.


        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public SeatedState(PlayerCharacter p) : base(p) { }
        public SeatedState(BaseMovementState previous) : base(previous) { }

        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "seated";
        }

        public override void FixedUpdate() {
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckSwim() || CheckAirborn() || CheckEnd()) {
                return;
            }
        }

        
        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            return player.movementInput != Vector3.zero; // If player has any movement, exit state.
        }
        

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanFocus() {
            return true;
        }
    }
}