using System;

namespace StellarRemnants.Interact {
    public class AirVent : IntegratedInteractable {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<AirVent> OPT_OPEN = new OpenGrate{LocalizationKey = "Opt_AirVent_Open"};
        public static readonly Interaction<AirVent> OPT_CLOSE = new CloseGrate{LocalizationKey = "Opt_AirVent_Close"};
        public static readonly Interaction<AirVent> OPT_UNBLOCK = new Unblock{LocalizationKey = "Opt_AirVent_Unblock"};

        public static readonly ObjectState<AirVent> STATE_OPEN = new ObjectState<AirVent>("Sta_Generic_Open");
        public static readonly ObjectState<AirVent> STATE_CLOSED = new ObjectState<AirVent>("Sta_Generic_Closed");
        public static readonly ObjectState<AirVent> STATE_BLOCKED = new ObjectState<AirVent>("Sta_AirVent_Blocked");


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public AtmoRegulator Regulator;
        public bool IsGrateOpen = true;
        public bool IsBlocked = false;

        
        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            // TODO: Set all variables here.
        }

        void Start() {
            // TODO: Set all references here, including those used in IntegratedInteractables.
            Regulator = Room.Structure.lifeSupport;
            AddStateListener(Room.OnVentBlockedUpdate);
        }


        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public bool DoAtmoStuff() {
            return !IsBlocked && Online && Regulator.DoAtmoStuff();
        }

        public bool AllowAirflow() {
            return !IsBlocked && Online;
        }


        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class OpenGrate : Interaction<AirVent> {
            public override bool Allow(AirVent obj, Credentials credentials) { return !obj.IsGrateOpen; }
            public override bool Perform(AirVent obj, Credentials credentials) { return false; }
            public override void Complete(AirVent obj, Credentials credentials) { }
        }

        private class CloseGrate : Interaction<AirVent> {
            public override bool Allow(AirVent obj, Credentials credentials) { return obj.IsGrateOpen; }
            public override bool Perform(AirVent obj, Credentials credentials) { return false; }
            public override void Complete(AirVent obj, Credentials credentials) { }
        }

        private class Unblock : Interaction<AirVent> {
            public override bool Allow(AirVent obj, Credentials credentials) { return obj.IsGrateOpen && obj.IsBlocked; }
            public override bool Perform(AirVent obj, Credentials credentials) { return false; }
            public override void Complete(AirVent obj, Credentials credentials) { }
        }
    }
}