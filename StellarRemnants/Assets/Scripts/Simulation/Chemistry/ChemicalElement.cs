using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Simulation.Chemistry {
    public class ChemicalElement {
        private string name {get;}
        private string symbol {get;}
        private ushort atomicNumber {get;}
        private float atomicMass {get;}
        private MatterState stpState {get;}
        private bool diatomic {get;}

        public ChemicalElement(ushort atomicNumber, string name, string symbol, float atomicMass, MatterState stpState, bool diatomic) {
            this.name = name;
            this.symbol = symbol;
            this.atomicNumber = atomicNumber;
            this.atomicMass = atomicMass;
            this.stpState = stpState;
            this.diatomic = diatomic;
        }
    }
}
