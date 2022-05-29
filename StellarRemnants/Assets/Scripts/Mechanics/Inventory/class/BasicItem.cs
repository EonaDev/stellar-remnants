namespace StellarRemnants.OldInventory {
    public class BasicItem : Item {
        private ItemTemplate Template;

        public BasicItem(ItemTemplate template) {
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
    }
}