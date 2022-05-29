using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StellarRemnants.Interact {
    public class DoorConsole : IntegratedInteractable {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<DoorConsole> OPT_OPEN = new Open{LocalizationKey = "Opt_DoorConsole_Open"};
        public static readonly Interaction<DoorConsole> OPT_CLOSE = new Close{LocalizationKey = "Opt_Generic_Close"};
        public static readonly Interaction<DoorConsole> OPT_LOCK = new Lock{LocalizationKey = "Opt_Generic_Lock"};
        public static readonly Interaction<DoorConsole> OPT_UNLOCK = new Unlock{LocalizationKey = "Opt_Generic_Unlock"};

        public static readonly ObjectState<DoorConsole> STATE_OPEN = new ObjectState<DoorConsole>("Sta_DoorConsole_Open", opt1: OPT_CLOSE, opt2: OPT_LOCK);
        public static readonly ObjectState<DoorConsole> STATE_CLOSED = new ObjectState<DoorConsole>("Sta_DoorControl_Closed", opt1: OPT_OPEN, opt2: OPT_LOCK);//, opt4: OPT_TERMINAL, opt5: OPT_PROXIMITY);
        public static readonly ObjectState<DoorConsole> STATE_LOCKED = new ObjectState<DoorConsole>("Sta_DoorControl_Locked", opt2: OPT_UNLOCK, opt4: OPT_TERMINAL); // , opt4: OPT_TERMINAL
        public static readonly ObjectState<DoorConsole> STATE_ERROR = new ObjectState<DoorConsole>("Sta_DoorControl_Error"); // , opt4: OPT_TERMINAL


  

        public static readonly TerminalConnector FIELDS = new TerminalConnector(
            new ToggleField("Ter_Generic_OpenClose", OPT_CLOSE, OPT_OPEN),
            new ToggleField("Ter_Generic_LockUnlock", OPT_LOCK, OPT_UNLOCK)
        );


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public Door ConnectedDoor;

        private Transform DoorwayFocalPoint;

        
        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            State = STATE_OFFLINE;
        }

        void Start() {
            if(ConnectedDoor != null) {
                ConnectedDoor.AddStateListener(OnDoorStateUpdate);
                DoorwayFocalPoint = (new GameObject("DoorwayFocalPoint")).transform;
                DoorwayFocalPoint.position = ConnectedDoor.transform.position;
                DoorwayFocalPoint.parent = transform;
                UpdateDoorControlState();
            }
            else {
                State = STATE_ERROR;
                InvokeStateChange(StateChange.Initialized);
            }
        }
        

        /*----------------------------------------
        |   IMPLEMENTATIONS - ObjectInteractable
        ----------------------------------------*/
        public override float MaxFocusAngle {
            get{return 90f;} // TODO: Get value from common data instead.
        }

        public override Transform FocalPoint {get{return DoorwayFocalPoint;}}


        /*----------------------------------------
        |   LOCAL FUNCTIONS
        ----------------------------------------*/
        public void UpdateDoorControlState() {
            ObjectState initialState = State;

            if(!ConnectedDoor.Online || ConnectedDoor.IsJammed) {
                State = STATE_ERROR;
            }
            else if(ConnectedDoor.IsLocked) {
                State = STATE_LOCKED;
            }
            else if(ConnectedDoor.IsOpen) {
                State = STATE_OPEN;
            }
            else {
                State = STATE_CLOSED;
            }

            if(initialState != State) {
                InvokeStateChange(StateChange.Refresh);
            }
        }

        /*----------------------------------------
        |   EVENT LISTENER FUNCTIONS
        ----------------------------------------*/
        public void OnDoorStateUpdate(Interactable interactable, StateChange type) {
            UpdateDoorControlState();
        }





        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class Open : Interaction<DoorConsole> {
            public override bool Allow(DoorConsole obj, Credentials credentials) { return !obj.ConnectedDoor.IsOpen; }
            public override bool Perform(DoorConsole obj, Credentials credentials) { return false; }
            public override void Complete(DoorConsole obj, Credentials credentials) { }
        }

        private class Close : Interaction<DoorConsole> {
            public override bool Allow(DoorConsole obj, Credentials credentials) { return obj.ConnectedDoor.IsOpen; }
            public override bool Perform(DoorConsole obj, Credentials credentials) { return false; }
            public override void Complete(DoorConsole obj, Credentials credentials) { }
        }

        private class Lock : Interaction<DoorConsole> {
            public override bool Allow(DoorConsole obj, Credentials credentials) { return !obj.ConnectedDoor.IsLocked; }
            public override bool Perform(DoorConsole obj, Credentials credentials) { return false; }
            public override void Complete(DoorConsole obj, Credentials credentials) { }
        }

        private class Unlock : Interaction<DoorConsole> {
            public override bool Allow(DoorConsole obj, Credentials credentials) { return obj.ConnectedDoor.IsLocked; }
            public override bool Perform(DoorConsole obj, Credentials credentials) { return false; }
            public override void Complete(DoorConsole obj, Credentials credentials) { }
        }

        




        /*----------------------------------------
        |   EDITOR FUNCTIONS
        ----------------------------------------*/
        #if UNITY_EDITOR
        public new void OnDrawGizmosSelected() {
            if(ConnectedDoor != null) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, ConnectedDoor.transform.position);
            }
            else {
                Gizmos.DrawIcon(transform.position, "cancel", true);
            }

            base.OnDrawGizmosSelected();
        }
        #endif
    }
}