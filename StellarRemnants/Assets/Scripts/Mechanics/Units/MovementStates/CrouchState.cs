using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Units {
    public class CrouchState : BaseMovementState {


        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public CrouchState(PlayerCharacter p) : base(p) { }
        public CrouchState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "crouching";
        }

        public override void CheckStateEnd() {
            if(CheckSwim() || CheckAirborn() || CheckSlide(player.common.MotionlessStartSlideThreshold) || CheckEnd() || CheckCrouchWalk()) {
                return;
            }
        }

        public override void FixedUpdate() {
            player.ApplyGravity();
            base.FixedUpdate();
        }


        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckCrouchWalk() {
            if(player.doMovement) {
                player.SetMovementState(new SneakState(this), "Player started moving while crouched");
                return true;
            }
            else {
                return false;
            }
        }

        private bool CheckEnd() {
            if(player.doCrouch || !player.HasHeadClearance) {
                return false;
            }
            else {
                player.SetMovementState(new StandState(this), "Player stopped crouching");
                return true;
            }
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            return player.HasHeadClearance;
        }

        public override bool CanCrouch() {
            return false;
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