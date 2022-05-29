using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "StackableItemData", menuName = "Resources/ScriptableObjects/Items/Stackable Item", order = 1)]
    public class StackableItemCommonData : ItemCommonData {
        public int MaxStackSize = 1;
    }
}