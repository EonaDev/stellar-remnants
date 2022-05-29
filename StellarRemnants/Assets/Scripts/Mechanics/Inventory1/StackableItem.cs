using UnityEngine;

namespace StellarRemnants.Inventory {
    public class StackableItem : Item {
        private int quantity;
        public StackableItemCommonData common;
        
        public override string GetName() {
            return common.ItemName;
        }

        public override string GetCategory() {
            return common.CategoryTitle;
        }

        public override ItemSize GetSize() {
            return common.Size;
        }

        public override Culture GetCultureOfOrigin() {
            return common.Culture;
        }

        public override float GetUnitMass() {
            return common.UnitMass;
        }

        public override float GetTotalMass() {
            return common.UnitMass;
        }

        public override int GetQuantity() {
            return quantity;
        }

        public override int GetMaxQuantity() {
            return common.MaxStackSize;
        }
    }
}