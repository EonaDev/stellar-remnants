using System.Collections.Generic;

namespace StellarRemnants.Simulation.Atmosphere {
    public class InfiniteAtmoVolume : AtmoVolume {


        /*----------------------------------------
        |   CONSTRUCTOR(S)
        ----------------------------------------*/
        public InfiniteAtmoVolume() {
            this.Composition = new Dictionary<Gas, double>();
            this.Signature = 0;
            this.ThermalEnergy = 0;
        }

        public InfiniteAtmoVolume(float thermalEnergy, params (Gas, double)[] gases) {
            this.Composition = new Dictionary<Gas, double>();

            foreach((Gas gas, double moles) in gases) {
                Composition[gas] = moles;
                TotalMoles += moles;
                Signature |= gas.Signature;
            }
            
            this.ThermalEnergy = thermalEnergy;
        }

        /*----------------------------------------
        |   OVERRIDES
        ----------------------------------------*/
        public override double Pressure {
            get{ return TotalMoles; }
        }
        
        public override double Temperature {
            get { return ThermalEnergy; }
        }

        public override FiniteAtmoVolume Split(double splitVolume) {
            // Since atmo is infinite, just create new finite volume with same pressure and compound ratios.
            Dictionary<Gas, double> newComposition = new Dictionary<Gas, double>();
            foreach(KeyValuePair<Gas, double> pair in Composition) {
                newComposition.Add(pair.Key, pair.Value * splitVolume);
            }
            
            return new FiniteAtmoVolume(splitVolume, ThermalEnergy * splitVolume, TotalMoles * splitVolume, newComposition, Signature);
        }

        public override void Merge(FiniteAtmoVolume other) {
            // Do nothing since volume is infinite.
        }

        public override void AddThermalEnergy(double thermalEnergy) { 
            // Do nothing since volume is infinite.
        }

        public override void AddTemperature(double temperature) {
            // Do nothing since volume is infinite.
        }

        public override void AddPressure(Gas gas, double moles) {
            // Do nothing since volume is infinite.
        }

        public override void AddMoles(Gas gas, double moles) {
            // Do nothing since volume is infinite.
        }

        public override double GetPartialPressure(Gas gas) {
            return Composition[gas];
        }

        public override double GetPartialPressure(double percentage) {
            return TotalMoles * percentage;
        }

        public override double GetMoles(Gas gas) {
            return Composition[gas];
        }

        
    }
}