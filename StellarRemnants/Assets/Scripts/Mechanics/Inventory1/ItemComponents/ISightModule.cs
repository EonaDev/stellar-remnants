using UnityEngine;

namespace StellarRemnants.Inventory {
    public interface ISightModule {
        public float GetZoomMultiplier(int level);
        public float GetAccuracyBonus();
        public float GetSwapPenalty();
    }
}
