using System.Collections.Generic;

namespace StellarRemnants.Simulation.Atmosphere {
    public abstract class AtmoVolume {

        /*----------------------------------------
        |   MEMBER VARIABLES
        ----------------------------------------*/

        // Composition of the atmosphere.
        protected Dictionary<Gas, double> Composition; // Allows up to 1 billion xmolecules per meter

        // Total moles of the volume. This is the sum of all values in Composition.
        public double TotalMoles;

        // Total thermal energy in the volume. Each unit represents a unit kelvin for each cubic meter of the volume.
        public double ThermalEnergy;

        // Signature of the composition
        public ulong Signature;


        public abstract double Pressure {get;}
        public abstract double Temperature {get;}
        
        
        /*----------------------------------------
        |   ABSTRACT METHODS
        ----------------------------------------*/
        public abstract FiniteAtmoVolume Split(double volumeDelta);
        public abstract void Merge(FiniteAtmoVolume other);
        public abstract void AddThermalEnergy(double thermalEnergyDelta);
        public abstract void AddTemperature(double temperatureDelta);
        public abstract void AddPressure(Gas compound, double moles);
        public abstract void AddMoles(Gas compound, double moles);
        public abstract double GetPartialPressure(Gas compound);
        public abstract double GetPartialPressure(double percent);
        public abstract double GetMoles(Gas compound);
        



    }
}