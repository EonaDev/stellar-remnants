using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Simulation.Atmosphere {
    public class FiniteAtmoVolume : AtmoVolume {

        /*----------------------------------------
        |   STATIC VARIABLES
        ----------------------------------------*/
        private static readonly float CLOSE_ENOUGH_THRESHOLD = 0.0001f;


        /*----------------------------------------
        |   MEMBER VARIABLES
        ----------------------------------------*/
        public double Volume; // Volume of AtmoVolume, in cubic meters starting at 0.0f
        
        /*----------------------------------------
        |   CONSTRUCTOR(S)
        ----------------------------------------*/
        public FiniteAtmoVolume(double volume) {
            this.Volume = volume;
            this.ThermalEnergy = 0;
            this.Composition = new Dictionary<Gas, double>();
        }

        public FiniteAtmoVolume(double volume, double thermalEnergy, double totalMoles, Dictionary<Gas, double> composition, ulong signature) {
            this.Volume = volume;
            this.ThermalEnergy = thermalEnergy;
            this.TotalMoles = totalMoles;
            this.Composition = composition;
            this.Signature = signature;
        }

        /*----------------------------------------
        |   OVERRIDES
        ----------------------------------------*/
        public override double Pressure {
            get{ return TotalMoles / Volume; }
        }

        public override double Temperature {
            get { return ThermalEnergy / Volume; }
        }

        public override FiniteAtmoVolume Split(double splitVolume) {
            double percent = splitVolume / Volume;
            double thermalEnergyDelta = ThermalEnergy * percent;
            double totalMolesDelta = TotalMoles * percent;

            ThermalEnergy -= thermalEnergyDelta;
            TotalMoles -= totalMolesDelta;
            Volume -= splitVolume;

            Dictionary<Gas, double> newComposition = new Dictionary<Gas, double>();
            foreach(KeyValuePair<Gas, double> pair in Composition) {
                double gasDelta = pair.Value * percent;
                newComposition.Add(pair.Key, gasDelta);
                Composition[pair.Key] -= gasDelta;
            }
            
            return new FiniteAtmoVolume(splitVolume, thermalEnergyDelta, totalMolesDelta, newComposition, Signature);
        }

        public override void Merge(FiniteAtmoVolume other) {
            foreach(Gas gas in other.Composition.Keys) {
                if((Signature & gas.Signature) == gas.Signature) {
                    Composition[gas] += other.Composition[gas];
                }
                else {
                    Composition.Add(gas, other.Composition[gas]);
                }
            }

            TotalMoles += other.TotalMoles;
            Volume += other.Volume;
            ThermalEnergy += other.ThermalEnergy;
            Signature |= other.Signature;
        }


        public override void AddThermalEnergy(double thermalEnergy) {
            ThermalEnergy += thermalEnergy;
        }

        // public override void AddTemperature(double temperature) {
        //     ThermalEnergy += temperature * Volume;
        // }

        // public override void AddPressure(Gas compound, double moles) {
        //     double delta = moles * Volume;
        //     Composition[compound] = delta;
        //     TotalMoles += delta;
        // }

        public override double GetPartialPressure(Gas compound) {
            return Composition[compound] / Volume;
        }

        public override double GetPartialPressure(double percentage) {
            return percentage / Volume;
        }

        public override void AddMoles(Gas compound, double moles) {
            Composition[compound] = moles;
            TotalMoles += moles;
        }

        public override double GetMoles(Gas compound) {
            return Composition[compound];
        }
    }
}