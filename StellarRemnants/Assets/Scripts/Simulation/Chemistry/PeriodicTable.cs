using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace StellarRemnants.Simulation.Chemistry {
    public class PeriodicTable {
        private static readonly Dictionary<ushort, ChemicalElement> table = new Dictionary<ushort, ChemicalElement>() {
            {1, new ChemicalElement(1, "hydrogen", "H", 1.008f, MatterState.GAS, true)},
            {2, new ChemicalElement(2, "helium", "He", 4.002f, MatterState.GAS, false)},
            {3, new ChemicalElement(3, "lithium", "Li", 6.94f, MatterState.SOLID, false)},
            {4, new ChemicalElement(4, "beryllium", "Be", 9.0122f, MatterState.SOLID, false)},
            {5, new ChemicalElement(5, "boron", "B", 10.81f, MatterState.SOLID, false)},
            {6, new ChemicalElement(6, "carbon", "C", 12.011f, MatterState.SOLID, false)},
            {7, new ChemicalElement(7, "nitrogen", "N", 14.007f, MatterState.GAS, true)},
            {8, new ChemicalElement(8, "oxygen", "O", 15.999f, MatterState.GAS, true)},
            {9, new ChemicalElement(9, "fluorine", "F", 18.998f, MatterState.GAS, true)},
            {10, new ChemicalElement(10, "neon", "Ne", 20.180f, MatterState.GAS, false)},
            {11, new ChemicalElement(11, "sodium", "Na", 22.990f, MatterState.SOLID, false)},
            {12, new ChemicalElement(12, "magnesium", "Mg", 24.305f, MatterState.SOLID, false)},
            {13, new ChemicalElement(13, "aluminium", "Al", 26.982f, MatterState.SOLID, false)},
            {14, new ChemicalElement(14, "silicon", "Si", 28.085f, MatterState.SOLID, false)},
            {15, new ChemicalElement(15, "phosphorus", "P", 30.974f, MatterState.SOLID, false)},
            {16, new ChemicalElement(16, "sulfur", "S", 32.06f, MatterState.SOLID, false)},
            {17, new ChemicalElement(17, "chlorine", "Cl", 35.45f, MatterState.GAS, true)},
            {18, new ChemicalElement(18, "argon", "Ar", 39.95f, MatterState.GAS, false)},
        };
    }
}