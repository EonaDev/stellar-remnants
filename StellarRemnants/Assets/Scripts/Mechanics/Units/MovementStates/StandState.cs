using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Units {
    public class StandState : BaseMovementState {

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public StandState(PlayerCharacter p) : base(p) {}
        public StandState(BaseMovementState previous) : base(previous) { }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "standing";
        }

        public override void FixedUpdate() {


            // Vector3 velocityChange = player.parentVelocity - player.rb.velocity;
            // velocityChange = Vector3.ProjectOnPlane(velocityChange, player.transform.up);
            // float magnitude = velocityChange.magnitude;

            // //if(magnitude > player.motionlessDeceleration) {
            // //    velocityChange *= player.motionlessDeceleration / magnitude;
            // //}

            // player.rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Try to set actual speed to 0. Player shouldn't be moving anyway, but it couldn't hurt.

            player.ApplyGravity();
            base.FixedUpdate();
        }

        
        public override void CheckStateEnd() {
            if(CheckMicroGravityStart(true) || CheckSwim() || CheckAirborn() || CheckSlide(player.common.MotionlessStartSlideThreshold) || CheckSprint() || CheckCrouch() || CheckWalk()) {
                //Debug.Log("Updating state");
                return;
            }
        }


        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanJump() {
            return player.HasHeadClearance;
        }

        public override bool CanSprint() {
            return true;
        }

        public override bool CanCrouch() {
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