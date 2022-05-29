namespace StellarRemnants.OldInventory {
    public class StackableItem : Item {
        private StackableItemTemplate Template;
        private int Quantity;

        public StackableItem(StackableItemTemplate template, int quantity) {
            this.Template = template;
            this.Quantity = quantity;

            if(this.Quantity > template.MaxQuantity) {
                this.Quantity = template.MaxQuantity;
            }
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
            return Template.GetFormattedName(Quantity);
        }

        public override string GetTitle() {
            return Template.Title;
        }

        public override ItemSize GetSize() {
            return Template.Size;
        }

        public override int GetQuantity() {
            return Quantity;
        }

        public override int GetMaxQuantity() {
            return Template.MaxQuantity;
        }
    }
}