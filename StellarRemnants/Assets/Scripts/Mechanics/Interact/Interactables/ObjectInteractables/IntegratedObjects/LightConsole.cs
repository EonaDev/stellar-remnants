
using UnityEngine;

namespace StellarRemnants.Interact {
    public class LightConsole : IntegratedInteractable {
        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<LightConsole> OPT_ALL_ON = new TurnOnAll{LocalizationKey = "Opt_Generic_On"};
        public static readonly Interaction<LightConsole> OPT_ALL_OFF = new TurnOffAll{LocalizationKey = "Opt_Generic_Off"};
        public static readonly Interaction<LightConsole> OPT_SYNC = new SyncLights{LocalizationKey = "Opt_LightConsole_SyncLights"};

        public static readonly ObjectState<LightConsole> STATE_LIGHTS_ON = new ObjectState<LightConsole>("Sta_LightConsole_LightsOn", opt1: OPT_ALL_OFF, opt2: OPT_SYNC, opt4: OPT_TERMINAL);
        public static readonly ObjectState<LightConsole> STATE_LIGHTS_OFF = new ObjectState<LightConsole>("Sta_LightConsole_LightsOff", opt1: OPT_ALL_ON, opt2: OPT_SYNC, opt4: OPT_TERMINAL, opt5: AutoActivatable.OPT_TRIGGER_AUTOACTIVATE);


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public ControllableLight[] Lights;
        public bool AutoActivate;
        public bool AllOn;
        

        /*----------------------------------------
        |   UNITY METHODS
        ----------------------------------------*/
        public void Start() {}
        public void Update() {}
        public void FixedUpdate() {}

        public override void OnTriggerEnter(Collider other) {
            if(AutoActivate && Online) {
                SetLights(true);
            }
        }


        /*----------------------------------------
        |   PRIVATE METHODS
        ----------------------------------------*/
        private void SetLights(bool turnAllOn) {
            if(AllOn != turnAllOn) {
                if(turnAllOn) {
                    SetState(STATE_LIGHTS_ON, StateChange.General);
                }
                else {
                    SetState(STATE_LIGHTS_OFF, StateChange.General);
                }
                AllOn = turnAllOn;
            }

            if(turnAllOn) {
                foreach(ControllableLight light in Lights) {
                    light.EnableLight(null); // TODO: Pass some sort of credential.
                }
            }
            else {
                foreach(ControllableLight light in Lights) {
                    light.DisableLight(null); // TODO: Pass some sort of credential.
                }
            }
        }


        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class TurnOnAll : Interaction<LightConsole> {
            public override bool Allow(LightConsole obj, Credentials credentials) { return !obj.AllOn && obj.Online; }
            public override bool Perform(LightConsole obj, Credentials credentials) { return true; }
            public override void Complete(LightConsole obj, Credentials credentials) { obj.SetLights(true); }
        }

        private class TurnOffAll : Interaction<LightConsole> {
            public override bool Allow(LightConsole obj, Credentials credentials) { return obj.AllOn && obj.Online; }
            public override bool Perform(LightConsole obj, Credentials credentials) { return true; }
            public override void Complete(LightConsole obj, Credentials credentials) { obj.SetLights(false); }
        }

        private class SyncLights : Interaction<LightConsole> {
            public override bool Perform(LightConsole obj, Credentials credentials) { return obj.Online; }
            public override void Complete(LightConsole obj, Credentials credentials) { obj.SetLights(obj.AllOn); }
        }

        
    }
}