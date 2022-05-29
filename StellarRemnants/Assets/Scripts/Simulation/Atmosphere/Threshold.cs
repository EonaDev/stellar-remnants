using System.Collections.Generic;

namespace StellarRemnants.Simulation.Atmosphere {

    // TODO: Threshold should no longer be between to atmo volumes, because there is no guarantee the local or global atmo volume should be used.

    public interface Threshold {
        // public AtmoVolume VolumeA;
        // public AtmoVolume VolumeB;

        public float GetThresholdSize();
        // TODO: Shouldn't this be able to determine what's on the other side?


        




        
    }
}