using UnityEngine;

namespace StellarRemnants.Units {
    public abstract class BaseActivityState {
        protected PlayerCharacter player;
        protected float duration;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public BaseActivityState(PlayerCharacter p) {
            this.player = p;
        }

        public BaseActivityState(BaseActivityState previousState) {
            this.player = previousState.player;
        }

        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

        /*----------------------------------------
        |   IMPORTANT FUNCTIONS
        ----------------------------------------*/
        public virtual string GetStateName() {
            return "unnamed state";
        }

        public virtual void FixedUpdate() {
            duration += Time.fixedDeltaTime;
        }

        public virtual void Update() {}
        public virtual void CheckStateEnd() {}

        public virtual bool OptionPress(int selection) {
            return false;
        }

        public virtual bool OptionRelease(int selection) {
            return false;
        }

        public virtual bool PerformInteraction1() { return false; }
        public virtual bool PerformInteraction2() { return false; }
        public virtual bool PerformInteraction3() { return false; }
        public virtual bool PerformInteraction4() { return false; }

        public virtual bool PerformInteraction1Alt() { return false; }
        public virtual bool PerformInteraction2Alt() { return false; }
        public virtual bool PerformInteraction3Alt() { return false; }
        public virtual bool PerformInteraction4Alt() { return false; }


        public virtual bool SetGlance(Vector3 position, float duration) {
            return false;
        }
        

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public virtual bool CanUse() { // AKA firing.
            return false;
        }

        public virtual bool CanFocus() {
            return false;
        }

        public virtual bool CanLookAround() {
            return true;
        }

        public virtual bool CanSprint() {
            return false;
        }

        public virtual bool CanCrouch() {
            return false;
        }

        public virtual bool CanJump() {
            return false;
        }

        public virtual bool CanBrace() {
            return false;
        }

        /*----------------------------------------
        |   COMMON END-STATE FUNCTIONS
        ----------------------------------------*/
        protected bool CheckFocus() {
            if(player.doFocus && !player.cancelFocus && player.movementState.CanFocus()) {
                player.SetActivityState(new FocusState(player), "Player began focusing");
                return true;
            }
            return false;
        }
    }
}