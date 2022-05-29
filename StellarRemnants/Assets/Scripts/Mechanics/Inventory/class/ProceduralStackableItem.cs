using StellarRemnants.Inventory;
namespace StellarRemnants.OldInventory {
    public class ProceduralStackableItem : Item, IProcedural {
        private StackableItemTemplate Template;
        private string SavedName;
        private long WorldSeed;
        private long SourceSeed;
        private int Quantity;

        public ProceduralStackableItem(StackableItemTemplate template, string name, long worldSeed, long sourceSeed) {
            this.Template = template;
            this.SavedName = name;
            this.WorldSeed = worldSeed;
            this.SourceSeed = sourceSeed;
        }

        public override int GetId() {
            return Template.Id;
        }

        public override string GetName() {
            return SavedName + Template.Name;
        }

        public override string GetFormattedName(int count) {
            return SavedName + Template.GetFormattedName(count);
        }

        public override string GetFormattedName() {
            return SavedName + Template.GetFormattedName(Quantity);
        }

        public override string GetTitle() {
            return Template.Title;
        }

        public string GetBaseName() {
            return Template.Name;
        }

        public string GetSavedName() {
            return SavedName;
        }

        public override ItemSize GetSize() {
            return Template.Size;
        }

        public long GetOriginWorldSeed() {
            return WorldSeed;
        }

        public long GetSourceSeed() {
            return SourceSeed;
        }

        public override int GetQuantity() {
            return Quantity;
        }

        public override int GetMaxQuantity() {
            return Template.MaxQuantity;
        }
    }
}