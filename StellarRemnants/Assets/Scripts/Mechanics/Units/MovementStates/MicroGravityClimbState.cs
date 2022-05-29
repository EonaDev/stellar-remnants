using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Units {
    public class MicroGravityClimbState : BaseMovementState {


        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public MicroGravityClimbState(PlayerCharacter p) : base(p) { }
        public MicroGravityClimbState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override void FixedUpdate() {
            player.ApplyGravity(); // Should gravity be applied?
            base.FixedUpdate();
        }


        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/




        
        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            return true;
        }

        public override bool CanFocus() {
            return true;
        }
    }
}