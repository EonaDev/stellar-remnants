using UnityEngine;

//[CreateAssetMenu(fileName = "WeaponData", menuName = "Resources/ScriptableObjects/Items/Weapons/Common", order = 1)]
namespace StellarRemnants.Inventory {
    public abstract class WeaponCommonData : ItemCommonData {
        [Header("Weapon Details")]
        public WeaponCategory Category;
        public Manufacturer Manufacturer;
        public DamageType DamageType;
        public float Damage {get; private set;} = 0.2f;
        public int BurstFire {get; private set;} = 1;
        public float TimeBetweenBursts {get; private set;} = 0.01f; // Amount of time between each round of burst fire.
        public float RoundsPerSecond {get; private set;} = 2f;
        public bool IsAutomatic {get; private set;} = false;
        public float MaxAngle {get; private set;} = 1f; // Accuracy of weapon, measured by maximum angle from center that the projectile may be fired.
        public float Recoil {get; private set;} = 1f; // Angle of recoil. Maybe power of recoil?
        public float CritMultiplier {get; private set;} = 2f;
        //public DamageType damageType; // Kinetic, plasma, crystal, hardlight, fire, corrosive, cryo, etc
        public int Pierce {get; private set;} = 0;

        // projectile type
    }
}