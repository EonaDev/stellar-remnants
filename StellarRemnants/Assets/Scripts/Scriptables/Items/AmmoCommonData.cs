using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "AmmoData", menuName = "Resources/ScriptableObjects/Items/Ammo", order = 1)]
    public class AmmoCommonData : StackableItemCommonData {
        public AmmoType AmmoType;
    }
}