using UnityEngine;

namespace StellarRemnants.Inventory {
    public abstract class Item {
        public abstract string GetName();
        public abstract string GetCategory();
        public abstract ItemSize GetSize();
        public abstract float GetUnitMass();
        public abstract float GetTotalMass();
        public virtual Culture GetCultureOfOrigin() { return Culture.None; }
        public virtual int GetQuantity() { return 1; }
        public virtual int GetMaxQuantity() { return 1; }


        // TODO: Since function of equipped items (while ADS) can vary, behaviors must be displayed on screen
        //       This means they will need node-like interaction options.
    }
}