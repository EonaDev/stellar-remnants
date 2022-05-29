using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class ManufacturedStackableItem : Item {
        private ManufacturedStackableItemTemplate Template;
        private int Quantity;

        public ManufacturedStackableItem(ManufacturedStackableItemTemplate template) {
            this.Template = template;
            this.Quantity = 0;
        }

        public ManufacturedStackableItem(ManufacturedStackableItemTemplate template, int quantity) {
            this.Template = template;
            this.Quantity = quantity;
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

        public override int GetQuantity() {
            return Quantity;
        }
    }
}