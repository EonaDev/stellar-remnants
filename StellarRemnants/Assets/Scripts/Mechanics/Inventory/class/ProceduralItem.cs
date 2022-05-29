namespace StellarRemnants.OldInventory {
    public class ProceduralItem : Item, IProcedural {
        private ItemTemplate Template;
        private string SavedName;
        private long WorldSeed;
        private long SourceSeed;

        public ProceduralItem(ItemTemplate template, string name, long worldSeed, long sourceSeed) {
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

        public string GetBaseName() {
            return Template.Name;
        }

        public override string GetFormattedName(int count) {
            return SavedName + Template.GetFormattedName(count);
        }

        public override string GetFormattedName() {
            return SavedName + Template.GetFormattedName(1);
        }

        public override string GetTitle() {
            return Template.Title;
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
    }
}