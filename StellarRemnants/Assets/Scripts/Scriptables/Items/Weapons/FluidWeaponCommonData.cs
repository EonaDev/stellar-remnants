using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "FluidWeaponData", menuName = "Resources/ScriptableObjects/Items/Weapons/Fluid-Based", order = 1)]
    public class FluidWeaponCommonData : WeaponCommonData {
        [Header("Fluid Details")]
        public FluidAmmoType fluidType;
        public float TankSize = 1f;
        public float ReserveTankSize = 1f;
    }
}