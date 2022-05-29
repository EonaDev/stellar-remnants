using System.Collections.Generic;
using StellarRemnants.Simulation.Atmosphere;

namespace StellarRemnants.Interact {
    public class AtmoRegulator : IntegratedInteractable {

        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public AtmoVolume mixingAtmosphere;


        
        /*----------------------------------------
        |   CACHED VARIABLES
        ----------------------------------------*/
        private AtmoVolume _structureAtmosphere; // Cached


        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            // mixingAtmosphere = new FiniteAtmoVolume(
            //     1f, 293f, 
            //     new KeyValuePair<ulong, float>(AtmoCompound.NITROGEN.Signature, 0.78f),
            //     new KeyValuePair<ulong, float>(AtmoCompound.OXYGEN.Signature, 0.21f),
            //     new KeyValuePair<ulong, float>(AtmoCompound.CARBON_DIOXIDE.Signature, 0.01f)
            // );

            // TODO: Load the mixing atmosphere instead of initializing it here.
        }

        void Start() {
            _structureAtmosphere = Room.Structure.Atmosphere; // Cached Atmosphere
        }

        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public bool DoAtmoStuff() {
            return false;
        }
    }
}