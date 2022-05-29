using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class ManufacturedStackableItemTemplate : ManufacturedItemTemplate {
        int MaxQuantity;

        public ManufacturedStackableItemTemplate(int id, string name, string formattedName, string title, ItemSize size, Culture origin, int maxQuantity) : base(id, name, formattedName, title, size, origin) {
            this.MaxQuantity = maxQuantity;
        }
    }
}