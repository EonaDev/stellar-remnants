using UnityEngine;

namespace StellarRemnants.Interact {
    [RequireComponent(typeof(Rigidbody))]
    public class ToyBall : StandaloneInteractable {
        
        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<ToyBall> OPT_LIFT = new Lift{LocalizationKey = "Opt_Generic_Lift"};
        public static readonly Interaction<ToyBall> OPT_LAUNCH = new Launch{LocalizationKey = "Opt_Ball_Launch"};
        public static readonly Interaction<ToyBall> OPT_KICK = new Kick{LocalizationKey = "Opt_Ball_Kick"};

        public static readonly ObjectState<ToyBall> STATE_DEFAULT = new ObjectState<ToyBall>("Sta_Generic_None", opt1: OPT_LIFT, opt2: OPT_LAUNCH, opt3: OPT_KICK);


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        private Rigidbody rb;
        // Gravity source, for up direction.
        

        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Start() {
            rb = GetComponent<Rigidbody>();
            //State = STATE_DEFAULT;
        }

        /*----------------------------------------
        |   IMPLEMENTATIONS - ObjectInteractable
        ----------------------------------------*/




        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class Lift : Interaction<ToyBall> {
            public override bool Perform(ToyBall obj, Credentials credentials) { return false; }
            public override void Complete(ToyBall obj, Credentials credentials) { }
        }

        private class Launch : Interaction<ToyBall> {
            public override bool Perform(ToyBall obj, Credentials credentials) { return false; }
            public override void Complete(ToyBall obj, Credentials credentials) {
                obj.rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse); // TODO: Account for direction of gravity.
            }
        }

        private class Kick : Interaction<ToyBall> {
            public override bool Perform(ToyBall obj, Credentials credentials) { return false; }
            public override void Complete(ToyBall obj, Credentials credentials) { 
                obj.rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);  // TODO: Account for direction of gravity. Also, account for 'forward' direction.
            }
        }
    }
}