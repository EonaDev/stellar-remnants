using UnityEngine;

namespace StellarRemnants.Inventory {
    public class EquippedGun : MonoBehaviour, EquippedItem {
        public Gun gun {get; private set;}
        private float reloadTime;

        // This class handles time-based gun logic, like reloading, firing, cooldown, etc
        // Maybe also prevent things like dropping an item when it's on cooldown to prevent potential exploits. (When dropped, the item becomes a dropped item instead of an equipped item and has no passive logic)

        public void FixedUpdate() {
            
        }

        public void Update() {

        }

        public void SetGun(Gun gun) {
            this.gun = gun;
            // Cache important variables.
        }
    }
}