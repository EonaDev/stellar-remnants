using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Interact {
    [RequireComponent(typeof(Light))]
    public class LightFixture : IntegratedInteractable, ControllableLight {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<LightFixture> OPT_TURN_ON = new TurnOn{LocalizationKey = "Opt_Generic_On"};
        public static readonly Interaction<LightFixture> OPT_TURN_OFF = new TurnOff{LocalizationKey = "Opt_Generic_Off"};

        public static readonly ObjectState<LightFixture> STATE_ON = new ObjectState<LightFixture>("Sta_Generic_On", opt1: OPT_TURN_OFF);
        public static readonly ObjectState<LightFixture> STATE_FLICKERING = new ObjectState<LightFixture>("Sta_LightFixture_Flicker", opt1: OPT_TURN_OFF);
        public static readonly ObjectState<LightFixture> STATE_OFF = new ObjectState<LightFixture>("Sta_Generic_Off", opt1: OPT_TURN_ON);
        

        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public Light LightSource;


        /*----------------------------------------
        |   UNITY METHODS
        ----------------------------------------*/
        void Awake() {
            LightSource = GetComponent<Light>();
        }

        void FixedUpdate() {
        }


        /*----------------------------------------
        |   OVERRIDES
        ----------------------------------------*/
        public void EnableLight(Credentials credentials) {
            if(OPT_TURN_ON.Allow(this, credentials)) {
                OPT_TURN_ON.Perform(this, credentials);
            }
        }

        public void DisableLight(Credentials credentials) {
            if(OPT_TURN_OFF.Allow(this, credentials)) {
                OPT_TURN_OFF.Perform(this, credentials);
            }
        }


        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class TurnOn : Interaction<LightFixture> {
            public override bool Allow(LightFixture obj, Credentials credentials) { return false; }
            public override bool Perform(LightFixture obj, Credentials credentials) { return false; }
            public override void Complete(LightFixture obj, Credentials credentials) { }
        }

        private class TurnOff : Interaction<LightFixture> {
            public override bool Allow(LightFixture obj, Credentials credentials) { return false; }
            public override bool Perform(LightFixture obj, Credentials credentials) { return false; }
            public override void Complete(LightFixture obj, Credentials credentials) { }
        }

    }
}