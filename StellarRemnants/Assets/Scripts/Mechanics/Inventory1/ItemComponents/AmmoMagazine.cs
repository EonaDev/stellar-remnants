namespace StellarRemnants.Inventory {
    public class AmmoMagazine : IAmmoModule {
        int ammoRemaining;
        //common data

        public string GetModuleName() {
            return "";
        }

        public bool HasAmmoRemaining() {
            return false;
        }

        public int GetAmountRemaining() {
            return ammoRemaining;
        }

        public float GetPercentRemaining() {
            return 0f;
        }

        public float GetReloadDuration() {
            return 1f;
        }

        public float GetCooldownRate() {
            return 0f;
        }

        public float GetSwapPenalty() {
            return 0f;
        }

        public void ConsumeAmmo(int amountConsumed) {

        }

        public void ConsumeAmmo(float amountConsumed) {

        }
    }
}