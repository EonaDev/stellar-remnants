using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class ManufacturedItem : Item {
        private ManufacturedItemTemplate Template;

        public ManufacturedItem(ManufacturedItemTemplate template) {
            this.Template = template;
        }

        public override int GetId() {
            return Template.Id;
        }

        public override string GetName() {
            return Template.Name;
        }

        public override string GetFormattedName(int count) {
            return Template.GetFormattedName(count);
        }

        public override string GetFormattedName() {
            return Template.GetFormattedName(1);
        }

        public override string GetTitle() {
            return Template.Title;
        }

        public override ItemSize GetSize() {
            return Template.Size;
        }

        public override Culture GetManufacturer() {
            return Template.Manufacturer;
        }
    }
}