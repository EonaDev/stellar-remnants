namespace StellarRemnants.Inventory {
    public class Gun : Item {
        public IGunBase gunBase;
        public IAmmoModule ammoModule;
        public ISightModule sightModule;
        public IStockModule stockModule;
        public IBarrelModule barrelModule;
        public IAttachmentModule attachmentModule;
        public WeaponCommonData common; // Revise common data

        public override string GetName() {
            return "";
        }

        public override string GetCategory() {
            return "";
        }

        public override ItemSize GetSize() {
            return ItemSize.PrimaryWeapon;
        }

        public override Culture GetCultureOfOrigin() {
            return Culture.Unknown;
        }

        public override float GetUnitMass() {
            return 1f;
        }

        public override float GetTotalMass() {
            return 1f;
        }

        public Manufacturer GetManufacturer() {
            return Manufacturer.CoalitionOfRemants;
        }
        
    }
}