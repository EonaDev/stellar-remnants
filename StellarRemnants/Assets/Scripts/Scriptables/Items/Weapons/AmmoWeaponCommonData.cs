using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "AmmoWeaponData", menuName = "Resources/ScriptableObjects/Items/Weapons/Ammo-Based", order = 1)]
    public class AmmoWeaponCommonData : WeaponCommonData {
        [Header("Ammo Details")]
        public AmmoType AmmoType;
        public int magazineSize = 10;
        public int reserveAmmo = 20;
        public float reloadTime = 1f;
        public int ammoPerShot = 1;
    }
}