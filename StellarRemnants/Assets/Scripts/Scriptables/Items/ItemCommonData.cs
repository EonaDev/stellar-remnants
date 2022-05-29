using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "ItemData", menuName = "Resources/ScriptableObjects/Items/Basic Item", order = 1)]
    public class ItemCommonData : ScriptableObject {
        public string ItemName = "test item";
        public string CategoryTitle = "placeholder";
        public ItemSize Size = ItemSize.Tiny;
        public Culture Culture = Culture.None;
        public float UnitMass = 1f;
    }
}