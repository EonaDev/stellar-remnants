using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Simulation.Atmosphere {

    // TODO: Threshold should no longer be between to atmo volumes, because there is no guarantee the local or global atmo volume should be used.

    public interface Threshold {

        public float GetThresholdSize();

        public AtmoContainer GetConnectionA();
        public AtmoContainer GetConnectionB();

        public AtmoContainer GetAdjacent(AtmoContainer current) {
            return current == GetConnectionA() ? GetConnectionB() : GetConnectionA();
        }


        public void TransferAtmo() {
            if(GetThresholdSize() == 0) {
                return;
            }

            AtmoVolume a = GetConnectionA().GetAtmosphere();
            AtmoVolume b = GetConnectionB().GetAtmosphere();




        }





        
    }


    public class Threshold2 {

        public static readonly double DiffusionRateMultiplier = 1f; // TODO: Set
        public static readonly double DepressurizationRateMultiplier = 1f;
        public static readonly double DiffusionThreshold = 1f; // TODO: Set
        public AtmoContainer ContainerA;
        public AtmoContainer ContainerB;

        public float Size;
        public float PercentOpen;

        public void SteppedUpdate(float timeDelta) {
            if(PercentOpen == 0) { return; }

            AtmoVolume a = ContainerA.GetAtmosphere();
            AtmoVolume b = ContainerB.GetAtmosphere();

            if(a == b) { return; }

            double pressureDifference = a.Pressure - b.Pressure;
            double rate = Size * PercentOpen * timeDelta;

            if((pressureDifference > 0 ? pressureDifference : -pressureDifference) > DiffusionThreshold) {

                // Depressurize; Use volume A as "from" and B as "to".
                if(b.Pressure > a.Pressure) { (a, b) = (b, a); } // Swap the two so A is "from" and B is "To".
                if(b.Signature != (b.Signature & a.Signature)) {
                    //b.UpdateKeys(a);
                }
                
                rate *= pressureDifference * DepressurizationRateMultiplier;

                double tempDelta = rate * a.ThermalEnergy; // Thermal energy or temperature?
                a.AddThermalEnergy(-tempDelta); // Thermal energy or temperature?
                b.AddThermalEnergy(tempDelta); // Thermal energy or temperature?

                foreach(Gas gas in a.Composition.Keys) {
                    double gasDelta = rate * a.GetPartialPressure(gas); // Pressure or moles?
                    a.AddMoles(gas, -gasDelta); // Pressure or moles?
                    b.AddMoles(gas, gasDelta); // Pressure or moles?
                }


            }
            else {

                // Diffuse
                if(a.Signature != b.Signature) {
                    //a.UpdateKeys(b);
                    //b.UpdateKeys(a);
                }

                rate *= DiffusionRateMultiplier;

                double tempDelta = rate * (a.Temperature - b.Temperature);
                a.AddThermalEnergy(-tempDelta);
                b.AddThermalEnergy(tempDelta);

                foreach(Gas gas in a.Composition.Keys) {
                    double gasDelta = rate * (a.GetPartialPressure(gas) - b.GetPartialPressure(gas));
                    a.AddMoles(gas, -gasDelta);
                    b.AddMoles(gas, gasDelta);
                }

            }
        }







        private void Depressurize() {

        }
    }
}