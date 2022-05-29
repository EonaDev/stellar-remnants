namespace StellarRemnants.Simulation.Atmosphere {
    using System.Linq;
    using UnityEngine;
    using StellarRemnants.Utilities;

    public class OldThreshold {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        private static readonly float DIFFUSION_THRESHOLD = 0.01f; // Pressure differential in atm.
        private static readonly float ATMO_EQUILIBRIUM_THRESHOLD = 0.001f; // Pressure differential in atm.
        private static readonly float THERMAL_EQUILIBRIUM_THRESHOLD = 0.001f;
        private static readonly float DIFFUSION_RATE_MULTIPLIER = 0.01f; // Atmosphere diffusion rate. Percentage per second per meters^2 of threshold size.
        private static readonly float DEPRESSURIZATION_RATE_MULTIPLIER = 0.1f;

        // TODO: Rooms connect to the life support system. As an optimization, do not perform complicated atmo sim calculations on connected rooms.
        //       Instead, atmosphere in these rooms connect directly to the life support system.
        //       Complicated atmo sim calculations should only be done for disconnected rooms (destroyed atmo controls / blocked vent).
        //       Any gas changes in these disconnected rooms can then spread into the life support system and all connected rooms, assuming there is an open pathway.


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public AtmoVolume VolumeA;
        public AtmoVolume VolumeB;
        public float PercentOpen;
        public float Size;
        
        private bool inEquilibrium; // No longer needed since atmo merges back in with main once it equilizes


        /*----------------------------------------
        |   CONSTRUCTOR(S)
        ----------------------------------------*/
        public OldThreshold(AtmoVolume a, AtmoVolume b, float size, float percentOpen) {
            this.VolumeA = a;
            this.VolumeB = b;
            this.Size = size;
            this.PercentOpen = percentOpen;
            this.inEquilibrium = false;
        }




        public void AtmoUpdate() {
            // OPTIMIZATION "CLOSED": Thresholds that are not open do not need to perform atmo-sim calculations.
            if(PercentOpen == 0.0f) { // TODO: Check against tolerance instead.
                return;
            }

            if(false && inEquilibrium) { // TODO: Implement better equilibrium optimization.
                // if(Mathf.Abs(VolumeA.GetTemperature() - VolumeB.GetTemperature()) > THERMAL_EQUILIBRIUM_THRESHOLD) {
                //     DiffuseHeat();
                // }
            }
            else {
                // float pressureDifference = Mathf.Abs(VolumeA.GetPressure() - VolumeB.GetPressure());
                // if(pressureDifference > DIFFUSION_THRESHOLD) {
                //     Depressurize(pressureDifference); // Includes heat.
                // }
                // else {
                //     DiffuseAtmosphere();

                //     if(Mathf.Abs(VolumeA.GetTemperature() - VolumeB.GetTemperature()) > THERMAL_EQUILIBRIUM_THRESHOLD) {
                //         DiffuseHeat();
                //     }
                // }
            }
        }



        public void DiffuseHeat() {
            // float tempDelta = Size * PercentOpen * DIFFUSION_RATE_MULTIPLIER * (VolumeA.GetTemperature() - VolumeB.GetTemperature());
            // VolumeA.AddThermalEnergy(-tempDelta);
            // VolumeB.AddThermalEnergy(tempDelta);
        }


        private void DiffuseAtmosphere() {
            // if(VolumeA.Signature != VolumeB.Signature) { // OPTIMIZATION "SIGNATURE": Only update keys in atmosphere dictionary if the volumes have mismatched keys.
            //     VolumeA.UpdateKeys(VolumeB);
            //     VolumeB.UpdateKeys(VolumeA);
            // }
            
            // float rate = Size * PercentOpen * DIFFUSION_RATE_MULTIPLIER;
            // float highestDelta = 0;

            // VolumeA.Composition.ToList().ForEach(a => {
            //     float delta = rate * (VolumeA.GetPartialPressure(a.Value) - VolumeB.GetPartialPressure(a.Key));
            //     VolumeA.AddPartialPressure(a.Key, -delta);
            //     VolumeB.AddPartialPressure(a.Key, delta);

            //     // OPTIMIZATION "EQUILIBRIUM": Find highest change to compare to equilibrium threshold.
            //     if(delta > highestDelta) {
            //         highestDelta = delta;
            //     }
            // });

            // // OPTIMIZATION "EQUILIBRIUM": If the highest atmosphere change delta is less than equilibrium threshold, set equilibrium to true.
            // if(highestDelta < ATMO_EQUILIBRIUM_THRESHOLD) {
            //     inEquilibrium = true;
            // }
        }


        private void Depressurize(float pressureDifference) {
            // AtmoVolume source;
            // AtmoVolume destination;

            // if(VolumeA.GetPressure() > VolumeB.GetPressure()) {
            //     source = VolumeA;
            //     destination = VolumeB;
            // }
            // else {
            //     source = VolumeB;
            //     destination = VolumeA;
            // }

            // if(source.Signature != destination.Signature) { // OPTIMIZATION "SIGNATURE": Only update keys in atmosphere dictionary if volumes have mismatched keys.
            //     destination.UpdateKeys(source);
            // }

            // float rate = Size * PercentOpen * pressureDifference * DEPRESSURIZATION_RATE_MULTIPLIER; // Percentage of atmosphere moved per tick

            // // NOTE: Heat should be included in this function because thermal energy will always be transferred with depressurization.
            // float tempDelta = rate * source.ThermalEnergy; // Transfer percentage of thermal energy based on total atmosphere moved always the same percentage of atmo moved.
            // source.AddThermalEnergy(-tempDelta);
            // destination.AddThermalEnergy(tempDelta);

            // source.Composition.ToList().ForEach(s => {
            //     float delta = rate * source.GetPartialPressure(s.Value);
            //     source.AddPartialPressure(s.Key, -delta);
            //     destination.AddPartialPressure(s.Key, delta);
            // });
        }



        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public AtmoVolume GetOppositeVolume(AtmoVolume initial) {
            if(!VolumeA.Equals(initial) && !VolumeB.Equals(initial)) {
                return null; // Provided room is not connected to this threshold.
            }

            return VolumeA.Equals(initial) ? VolumeB : VolumeA;
        }


        /*----------------------------------------
        |   IMPLEMENTED FUNCTIONS
        ----------------------------------------*/
        public void SteppedUpdate(float timeDelta) {
        }

    }
}
