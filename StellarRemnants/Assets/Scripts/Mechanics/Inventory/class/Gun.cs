using UnityEngine;
using StellarRemnants.Inventory;

namespace StellarRemnants.OldInventory {
    public class Gun : Item {
        private ManufacturedItemTemplate Template;
        private Damage DamageType;
        private float DamageAmount;
        

        int BarrelId; // Or nozzle
        int StockId;
        int MagazineId; // Or battery or tank
        int SightId;
        int ModId;

        

        // TEMPLATE VALUES:
        int pierceLevel; // Determines how many levels of armor that the projectile can ignore. 
        int negateLevel; // Determines how many levels of shield resistance that a projectile can ignore.
        bool automatic;
        bool isUnlocked; // Determines whether or not an unauthorized holder can use the weapon.
        float maxZoomLevel; // Template value from scope
        int burstFire; // Number of rounds per shot. Does not use additional ammo.
        float burstFireDelay; // Time (in seconds) between burst fire shots.
        bool chargeOptional; // Weather or not charge up is required or optional. Weapons without charge options have chargeRequired=true, chargeUpRate=1f, chargeDownRate=1f
        float chargeUpRate; // Rate at which the weapon charges to fire (begins firing at 1.0f). 
        float chargeDownRate; // Rate at which the weapon uncharges if charging is stopped (defaults to 1.0f, instantly uncharges);
        // rate of fire acceleration?
        // rate of fire tied to heat like Halo Reach plasma repeater?


        // DERIVED VALUES:
        float rateOfFire;
        float recoil;
        float precision;
        
        public float gunDrawSpeed; // The rate at which weapons are drawn (swapping weapon)
        public float gunHolsterSpeed; // The rate at which weapons are put away (swapping weapon)
        public float adsEnterSpeed; // Rate at which ADS is entered (%/s)
        public float adsExitSpeed; // Rate at which ADS is exited (%/s)
        public float magUnloadSpeed;
        public float magLoadSpeed;

        public string name;

        // PER WEAPON FIELDS:
        public float currentZoomLevel; // Current zoom multiplier
        
        delegate bool FiringBehavior();
        FiringBehavior firingBehavior;

        delegate bool ReloadBehavior();
        ReloadBehavior reloadBehavior;

        // GUN VARIANT FIELDS (or just put them all in one since some guns use both energy AND ammo):
        int magazineSize; // Determined by magazine template. What about guns that use more than one ammo type? (Grenades & rockets)
        int ammoPerShot1;
        int ammoPerShot2;
        int remainingRounds;
        bool isMagazineIn; // Whether or not the magazine is in the gun.
        int storedAmmo; // The amount of reserve ammo accompanied by the gun. Automatically added as special inventory slots while weapon is equipped.

        float energyCapacity;
        float energyPerShot;
        float currentEnergy;

        float heatCapacity;
        float heatPerShot;
        float currentHeat;

        float heatDissipationRate;


        float lastShot; // Time that the last shot was fired



        // TODO: Figure out how to add and play audio source(s) from gun template
        
        public Gun(string name) {
            this.name = name;
            this.pierceLevel = 0;
            this.automatic = false;
            this.isUnlocked = true;
            this.maxZoomLevel = 1.5f;
            this.burstFire = 1;
            this.burstFireDelay = 0;
            this.chargeOptional = false;
            this.chargeUpRate = 100.0f;
            this.chargeDownRate = 100.0f;
            this.rateOfFire = 0.5f;
            this.recoil = 0.05f;
            this.precision = 0.1f;
            this.gunDrawSpeed = 1.0f;
            this.gunHolsterSpeed = 0.5f;
            this.adsEnterSpeed = 0.25f;
            this.adsExitSpeed = 0.25f;
            this.magUnloadSpeed = 0.5f;
            this.magLoadSpeed = 0.5f;
            this.currentZoomLevel = 1.5f;
            this.magazineSize = 8;
            this.ammoPerShot1 = 1;
            this.ammoPerShot2 = 0;
            this.remainingRounds = 8;
            this.isMagazineIn = true;
            this.storedAmmo = 16;
            firingBehavior = fireStandardProjectile;
            reloadBehavior = standardReload;


        }

        public Gun(string name, bool something) {
            this.name = name;
            this.pierceLevel = 0;
            this.automatic = true;
            this.isUnlocked = true;
            this.maxZoomLevel = 1;
            this.burstFire = 1;
            this.burstFireDelay = 0;
            this.chargeOptional = false;
            this.chargeUpRate = 100.0f;
            this.chargeDownRate = 100.0f;
            this.rateOfFire = 0.1f;
            this.recoil = 0.02f;
            this.precision = 0.1f;
            this.gunDrawSpeed = 1.0f;
            this.gunHolsterSpeed = 0.5f;
            this.adsEnterSpeed = 0.25f;
            this.adsExitSpeed = 0.25f;
            this.magUnloadSpeed = 0.5f;
            this.magLoadSpeed = 0.5f;
            this.currentZoomLevel = 1.5f;
            this.magazineSize = 10;
            this.ammoPerShot1 = 1;
            this.ammoPerShot2 = 0;
            this.remainingRounds = 30;
            this.isMagazineIn = true;
            this.storedAmmo = 60;
            firingBehavior = fireStandardProjectile;
            reloadBehavior = standardReload;
        }


        public Gun(ManufacturedItemTemplate template, int barrelId, int stockId, int magazineId, int sightId, int modId) {
            this.Template = template;
            this.BarrelId = barrelId;
            this.StockId = stockId;
            this.MagazineId = magazineId;
            this.SightId = sightId;
            this.ModId = modId;
            
            firingBehavior = fireStandardProjectile;
            reloadBehavior = standardReload;
        }


        public bool fire() {

            return firingBehavior();
        }

        public bool reload() {
            return reloadBehavior();
        }


        public bool fireStandardProjectile() { // TODO: Account for animation.
            
            if(isMagazineIn && remainingRounds > 0 && Time.fixedTime - lastShot >= rateOfFire) {
                // Create projectile.
                int before = remainingRounds;
                remainingRounds -= ammoPerShot1;
                lastShot = Time.fixedTime;
                Debug.Log("Ammo: " + before + " -> " + remainingRounds);
                return true;
            }
            Debug.Log("Can't fire");
            return false;
        }

        public bool standardReload() { // TODO: Account for animation.
            // Ignore isMagazineIn for now.
            if(magazineSize == remainingRounds) {
                return false;
            }

            int bullets = magazineSize - remainingRounds;
            
            if(bullets > storedAmmo) {
                bullets = storedAmmo;
            }

            if(bullets > 0) {
                remainingRounds += bullets;
                storedAmmo -= bullets;
                return true;
            }
            return false;
        }


        //https://www.youtube.com/watch?v=2WnAOV7nHW0


        public override int GetId() {
            return Template.Id;
        }

        public override string GetName() {
            return Template.Name;
        }

        public override string GetFormattedName(int count) {
            return Template.GetFormattedName(count);
        }

        public override string GetFormattedName() {
            return Template.GetFormattedName(1);
        }

        public override string GetTitle() {
            return Template.Title;
        }

        public override ItemSize GetSize() {
            return Template.Size;
        }

        public override EquipmentType GetEquipmentType() {
            return EquipmentType.GUN;
        }

        public override Culture GetManufacturer() {
            return Template.Manufacturer;
        }
    }
}