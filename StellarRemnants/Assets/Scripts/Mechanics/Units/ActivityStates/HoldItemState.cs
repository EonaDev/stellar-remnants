using StellarRemnants.Inventory;
/*----------------------------------------
  TODO LIST:
- Implement focusing while holding item.
----------------------------------------*/

namespace StellarRemnants.Units {
    public class HoldItemState : BaseActivityState {
        //public Item item;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public HoldItemState(PlayerCharacter p) : base(p) { }
        public HoldItemState(BaseActivityState previous) : base(previous) { }

        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "holding item";
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanSprint() {
            //ItemSize size = item.GetSize();
            //return size == ItemSize.Tiny || size == ItemSize.SideArm || size == ItemSize.PrimaryWeapon; // TODO: Allow sprint with light-weight items.
            return true;
        }

        public override bool CanFocus() {
            return true; // Can focus on certain nodes while holding items.
        }

        public override bool CanCrouch() {
            return true;
        }

        public override bool CanJump() {
            // ItemSize size = item.GetSize();
            // return !(size == ItemSize.Lifted || size == ItemSize.Deployable);
            return true;
        }
    }
}