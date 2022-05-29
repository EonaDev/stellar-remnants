using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public abstract class Item {
        public abstract int GetId();
        public abstract string GetName();
        public abstract string GetFormattedName();
        public abstract string GetFormattedName(int count);
        public abstract string GetTitle();
        public abstract ItemSize GetSize();
        

        public virtual int GetQuantity() {
            return 1;
        }

        public virtual int GetMaxQuantity() {
            return 1;
        }

        public virtual Culture GetManufacturer() {
            return Culture.None;
        }

        public virtual EquipmentType GetEquipmentType() {
            return EquipmentType.NONE;
        }
    }
}