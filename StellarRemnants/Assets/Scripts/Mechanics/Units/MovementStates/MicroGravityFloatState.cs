using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Units {
    public class MicroGravityFloatState : BaseMovementState {


        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public MicroGravityFloatState(PlayerCharacter p) : base(p) { }
        public MicroGravityFloatState(BaseMovementState previous) : base(previous) { }

        
        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "micro-g floating";
        }

        public override void FixedUpdate() {
            //if(player.DoMovement) {
            //    StandardMovementUpdate(player.walkSpeed, player.walkAcceleration, player.walkDeceleration);
            //}
            //else {
            //    StandardMovementUpdate(0, player.walkAcceleration, player.walkDeceleration);
            //}
            
            player.ApplyGravity();
            base.FixedUpdate();
        }


        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        public override void CheckStateEnd() {
            if(CheckMicroGravityEnd(false)) {
                return;
            }
            //if(CheckSwim() || CheckAirborn() || CheckSlide(player.walkSlideThreshold) || CheckSprint() || CheckCrouch() || CheckEnd()) {
            //    return;
            //}
        }

        private bool CheckEnd() {
            //if(!player.DoMovement && !player.HasVelocity) {
            //    player.SetMovementState(new StandState(this), "Player stopped walking");
            //    return true;
            //}
            return false;
        }


        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
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