using UnityEngine;
using StellarRemnants.Inventory;

/*----------------------------------------
  TODO LIST:
- Implement
----------------------------------------*/
namespace StellarRemnants.Units {
    public class AimState : BaseActivityState {
        //private Gun gun; // TODO: Shouldn't this be handled by player class?
        
        private float adsPercent; // This is separate from the aiming animation.
        private bool aimComplete;


        
        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public AimState(PlayerCharacter p) : base(p) { }
        public AimState(BaseActivityState previous) : base(previous) { }

        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            // If player has controller, enable aim down sight controls.
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            // If player has controller, disable aim down sight controls.
            base.OnStateExit();
        }

        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "aiming down sight";
        }

        public override void FixedUpdate() {
            updateAimProgress();

            if(player.doUse && player.doADS && adsPercent >= 1f) {
                //gun.fire(); // TODO: fire() returns a bool. What is this for?
            }


        }

        public override void Update() {
            // Update animation here.

        }

        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(!player.doADS && adsPercent <= 0) {
                player.SetActivityState(new IdleState(player), "Player stopped aiming");
                return true;
            }

            return false;
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanSprint() {
            //ItemSize size = gun.GetSize();
            //return size == ItemSize.SideArm || size == ItemSize.Tiny;
            return true;
        }

        public override bool CanJump() {
            //return gun.GetSize() != ItemSize.HeavyWeapon;
            return true;
        }

        public override bool CanCrouch() {
            return true;
        }

        public override bool CanUse() {
            return true;
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        private void updateAimProgress() {
            if(player.doADS) {
                if(adsPercent < 1f) {
                    //adsPercent += Time.fixedDeltaTime * gun.adsEnterSpeed;

                    if(adsPercent > 1f) {
                        adsPercent = 1f;
                    }
                }
            }
            else {
                if(adsPercent > 0f) {
                    //adsPercent -= Time.fixedDeltaTime * gun.adsExitSpeed;

                    if(adsPercent < 0) {
                        adsPercent = 0f;
                    }
                }
            }
        }
    }
}