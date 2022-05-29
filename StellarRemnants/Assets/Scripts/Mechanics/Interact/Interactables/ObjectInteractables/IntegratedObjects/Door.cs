using StellarRemnants.Simulation.Atmosphere;
using StellarRemnants.Scriptables;
using UnityEngine;

namespace StellarRemnants.Interact {
    public class Door : IntegratedInteractable, Threshold {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<Door> OPT_OPEN = new Open{LocalizationKey = "Opt_Generic_Open"};
        public static readonly Interaction<Door> OPT_CLOSE = new Close{LocalizationKey = "Opt_Generic_Close"};
        public static readonly Interaction<Door> OPT_LOCK = new Lock{LocalizationKey = "Opt_Generic_Lock"};
        public static readonly Interaction<Door> OPT_UNLOCK = new Unlock{LocalizationKey = "Opt_Generic_Unlock"};
        public static readonly Interaction<Door> OPT_KNOCK = new Knock{LocalizationKey = "Opt_Door_Knock"};
        public static readonly Interaction<Door> OPT_PRY = new Pry{LocalizationKey = "Opt_Generic_Pry"};
        public static readonly Interaction<Door> OPT_CUT = new Cut{LocalizationKey = "Opt_Generic_LaserCut"};
        public static readonly Interaction<Door> OPT_AUTO_ON = new AutoOn{LocalizationKey = "Opt_Generic_AutoActivateOn"};
        public static readonly Interaction<Door> OPT_AUTO_OFF = new AutoOff{LocalizationKey = "Opt_Generic_AutoActivateOff"};

        public static readonly ObjectState<Door> STATE_OPEN = new ObjectState<Door>("Sta_Generic_Open");
        public static readonly ObjectState<Door> STATE_CLOSED = new ObjectState<Door>("Sta_Generic_Closed", opt1: null, opt3: OPT_KNOCK); // TODO: Set conditional interaction for opt1.
        public static readonly ObjectState<Door> STATE_LOCKED = new ObjectState<Door>("Sta_Generic_Locked", opt1: null, opt3: OPT_KNOCK); // TODO: Set conditional interaction for opt1.

        public static readonly TerminalConnector FIELDS = new TerminalConnector(
            new ToggleField("Ter_Generic_OpenClose", OPT_CLOSE, OPT_OPEN),
            new ToggleField("Ter_Generic_LockUnlock", OPT_LOCK, OPT_UNLOCK),
            new ToggleField("Ter_Generic_AutoActivate", OPT_AUTO_ON, OPT_AUTO_OFF)
        );

        /*----------------------------------------
        |   DATA MEMBERS
        ----------------------------------------*/
        private DoorVariant doorVariant;

        //public Threshold Threshold;
        public bool AutoActivate;
        public bool IsOpen;
        public bool IsLocked;
        public bool IsMoving;
        public bool IsJammed;
        public float Size;
        public float PercentOpen;


        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        public void Awake() {
            doorVariant = GetScopedVariant<DoorVariant>();
            State = STATE_CLOSED;
            IsOpen = false;
            IsLocked = false;
            IsMoving = false;
            IsJammed = false;
        }

        public void Start() { }
        public void Update() { }
        public void FixedUpdate() { }

        public override void OnTriggerEnter(Collider other) {
            if(AutoActivate && Online) {
                
            }
        }


        /*----------------------------------------
        |   IMPLEMENTATIONS - Threshold
        ----------------------------------------*/
        public float GetThresholdSize() {
            return Size * PercentOpen;
        }







        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class Open : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return !obj.IsOpen && !obj.IsLocked && obj.Online; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Close : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return obj.IsOpen && !obj.IsLocked && obj.Online; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Lock : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return !obj.IsLocked && obj.Online; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Unlock : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return obj.IsLocked && obj.Online; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Knock : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return !obj.IsOpen; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Pry : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return false; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class Cut : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return false; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class AutoOn : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return false; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }

        private class AutoOff : Interaction<Door> {
            public override bool Allow(Door obj, Credentials credentials) { return false; }
            public override bool Perform(Door obj, Credentials credentials) { return false; }
            public override void Complete(Door obj, Credentials credentials) { }
        }
    }


    
}