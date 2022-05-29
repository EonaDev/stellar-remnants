using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class ManufacturedItemTemplate : ItemTemplate {
        public Culture Manufacturer;

        public ManufacturedItemTemplate(int id, string name, string formattedName, string title, ItemSize size, Culture origin) : base(id, name, formattedName, title, size) {
            this.Manufacturer = origin;
        }
    }
}