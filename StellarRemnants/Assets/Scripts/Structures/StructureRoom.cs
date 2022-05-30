using System;
using StellarRemnants.Simulation.Atmosphere;
using StellarRemnants.Interact;

namespace StellarRemnants {
    public class StructureRoom : AtmoContainer {

        // If a single vent is open, do not disconnect local atmo from global.
        // Conversely, do not reconnect local to global no matter how many vents or air pathways are open.
        // Instead, only reconnect once the two have equalized.


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public IntegratedInteractable[] Interactables;
        public AirVent[] Vents;
        public Threshold[] Doors;
        public StructureInterior Structure;
        private FiniteAtmoVolume LocalAtmosphere; // This will be null if room is connected to general atmosphere through airvents, regardless of whether doors are open.
        public string roomName = "room";
        public bool IsPowered = true;
        public double AtmosphereVolume;

        private bool lifeSupportConnection;
        private int blockedVents;

        private int airPathways;

        public StructureRoom() {
            airPathways = Vents.Length + Doors.Length;
        }
        

        /*----------------------------------------
        |   EVENT LISTENER FUNCTIONS
        ----------------------------------------*/
        public void OnVentBlockedUpdate(Interactable obj, StateChange type) {
            if(type == StateChange.VentBlockage && obj is AirVent vent) {
                if(vent.IsBlocked) {
                    if(blockedVents == 0) {
                        //DisconnectAtmosphere();
                    }
                    blockedVents++;
                }
                else {
                    blockedVents--;
                    if(blockedVents == 0) {
                        //JoinAtmosphere();
                    }
                }
            }
        }


        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public bool DoAtmoStuff() {
            foreach(AirVent vent in Vents) { // Find enabled airvent.
                if(vent.AllowAirflow()) {
                    return vent.DoAtmoStuff();
                }
            }

            return false;
        }

        

        
        /*----------------------------------------
        |   IMPLEMENTATIONS - AtmoContainer
        ----------------------------------------*/
        public AtmoVolume GetAtmosphere() { 
            return LocalAtmosphere == null ? Structure.Atmosphere : LocalAtmosphere;
        }

        public void SetEqualized() {
            
        }


        public void DoAtmoStuff2() { // Executes on each atmo tick
            


        }


        /*----------------------------------------
        |   PRIVATE FUNCTIONS
        ----------------------------------------*/
        private void JoinAtmosphere() {
            // TODO: Two atmo containers are connected and exchange between them occurs.
            
        }

        private void MergeAtmosphere() {
            // Only executes once pressure and composition between local and structure atmospheres have equilized.
            // Add all local atmo contents to structure atmo, then delete local atmo.
            Structure.Atmosphere.Merge(LocalAtmosphere);
            LocalAtmosphere = null;
        }

        private void DisconnectAtmosphere() {
            // Takes a chunk of the structure atmosphere and uses it as the local atmo, based on the volume of the room and of the structure overall.
            LocalAtmosphere = Structure.Atmosphere.Split(AtmosphereVolume);
        }

        

        

        
    }
}