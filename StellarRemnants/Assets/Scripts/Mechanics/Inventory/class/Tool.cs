using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class Tool : Item  {
        public override int GetId() {
            return 0; // TODO: Implement Suit template.
        }

        public override string GetName() {
            return ""; // TODO: Implement Suit template.
        }

        public override string GetFormattedName(int count) {
            return "";
        }

        public override string GetFormattedName() {
            return "";
        }

        public override string GetTitle() {
            return "";
        }

        public override ItemSize GetSize() {
            return ItemSize.HeavyWeapon; // TODO: Implement Suit template. // Why is this heavy again? That should be weapons only
        }

        public override EquipmentType GetEquipmentType() {
            return EquipmentType.TOOL;
        }
    }
}