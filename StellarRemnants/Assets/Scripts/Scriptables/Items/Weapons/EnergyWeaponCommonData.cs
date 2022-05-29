using UnityEngine;

namespace StellarRemnants.Inventory {
    [CreateAssetMenu(fileName = "EnergyWeaponData", menuName = "Resources/ScriptableObjects/Items/Weapons/Energy-Based", order = 1)]
    public class EnergyWeaponCommonData : WeaponCommonData {
        [Header("Energy Details")]
        public EnergyAmmoType energyType;
        public float batteryCapacity = 1f;
        public float heatPerShot = 0.2f;
        public float heatCapacity = 1f; // When reached, overheat
        public float cooldownRate = 0.3333f; // Amount per second
        public float overheatCooldownRate = 0.3333f; // Amount per second
        public float energyPerShot = 0.04f;
    }
}