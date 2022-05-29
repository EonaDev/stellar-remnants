namespace StellarRemnants.Inventory {
    public interface IAmmoModule {
        public string GetModuleName();
        public bool HasAmmoRemaining();
        public int GetAmountRemaining();
        public float GetPercentRemaining();
        public void ConsumeAmmo(int amountConsumed);
        public void ConsumeAmmo(float amountConsumed);
        public float GetReloadDuration();
        public float GetCooldownRate();
        public float GetSwapPenalty();
    }
}