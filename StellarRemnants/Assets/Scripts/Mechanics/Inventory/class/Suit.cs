using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class Suit : Item {


        public override int GetId() {
            return 0; // TODO: Implement Suit template.
        }

        public override string GetName() {
            return ""; // TODO: Implement Suit template.
        }

        public override string GetFormattedName(int count) {
            return "";
        }

        public override string GetFormattedName() {
            return "";
        }

        public override string GetTitle() {
            return "";
        }

        public override ItemSize GetSize() {
            return ItemSize.HeavyWeapon; // TODO: Implement Suit template.
        }

        public override EquipmentType GetEquipmentType() {
            return EquipmentType.SUIT;
        }
    }
}