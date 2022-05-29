using System.Collections.Generic;
using StellarRemnants.Units;

namespace StellarRemnants.Interact {
    public class Seat : StandaloneInteractable {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        // public static readonly Interaction2<Seat> OPT_SIT = new Interaction2<Seat>("Opt_Generic_Sit", AllowSit, PerformSit);
        // public static readonly ObjectState2<Seat> STATE_BASE = new ObjectState2<Seat>("Sta_Generic_None", opt5: OPT_SIT);


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public List<CharacterUnit> SeatedUnits = new List<CharacterUnit>();
        public int MaxSeated = 1;
        public float Width = 1f; // What about curved sofas?


        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            // TODO: Set all variables here.
        }

        void Start() {
            // TODO: Set all references here
        }


        /*----------------------------------------
        |   INTERACTION ENABLED CHECKS
        ----------------------------------------*/
        public static bool AllowSit(Seat obj, Credentials credentials) { return true; }


        /*----------------------------------------
        |   INTERACTION PERFORMED LOGIC
        ----------------------------------------*/
        public static bool PerformSit(Seat obj, Credentials credentials) { return false; }

    }
}