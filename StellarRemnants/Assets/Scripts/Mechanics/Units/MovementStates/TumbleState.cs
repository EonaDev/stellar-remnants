using UnityEngine;

/*----------------------------------------
  TODO LIST:
- Re-evaluate behavior.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class TumblingMovementState : BaseMovementState {
        private bool endTumble;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public TumblingMovementState(PlayerCharacter p) : base(p) { }
        public TumblingMovementState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            player.rb.freezeRotation = false;
            base.OnStateEnter();
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "tumbling";
        }

        public override void FixedUpdate() {
            if(!endTumble && player.rb.velocity.magnitude < 1f) {
                endTumble = true;
                player.rb.freezeRotation = true;
            }
            
            if(endTumble) {
                // Correct player's up vector.
            }
            player.ApplyGravity();
            base.FixedUpdate();
        }

        public override void CheckStateEnd() {
            if(CheckSwim() || CheckEnd()) {
                return;
            }
        }


        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(endTumble) { // TODO: && player's up vector = gravity normal
                player.SetMovementState(new SlideState(player));
                return true;
            }

            return false;
        }

        public override bool CanBrace() {
            return true;
        }
    }
}