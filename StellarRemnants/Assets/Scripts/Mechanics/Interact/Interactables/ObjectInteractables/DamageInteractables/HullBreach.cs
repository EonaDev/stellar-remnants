using StellarRemnants.Simulation.Atmosphere;

namespace StellarRemnants.Interact {
    public class HullBreach : DamageInteractable, Threshold {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        // public static readonly Interaction2<HullBreach> OPT_PATCH = new Interaction2<HullBreach>("Opt_HullBreach_Patch", AllowFix, PerformPatch);
        // public static readonly Interaction2<HullBreach> OPT_COVER = new Interaction2<HullBreach>("Opt_HullBreach_Cover", AllowFix, PerformCover);

        // public static readonly ObjectState2<HullBreach> STATE_OPEN = new ObjectState2<HullBreach>("Sta_HullBreach_Open", opt1: OPT_PATCH, opt2: OPT_COVER);
        // public static readonly ObjectState2<HullBreach> STATE_PATCHED = new ObjectState2<HullBreach>("Sta_HullBreach_Patched");

        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        //public Threshold threshold;
        public AtmoContainer SideA;
        public AtmoContainer SideB;
        public bool isPatched = true;
        public float Size;


        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            // TODO: Set all variables here.
            
        }

        void Start() {
            // TODO: Set all references here, including those used in IntegratedInteractables.
            
        }



        /*----------------------------------------
        |   IMPLEMENTATIONS - Threshold
        ----------------------------------------*/
        public float GetThresholdSize() {
            return isPatched ? 0f : Size;
        }

        public AtmoContainer GetConnectionA() { return SideA; }
        public AtmoContainer GetConnectionB() { return SideB; }





        // /*----------------------------------------
        // |   INTERACTION ENABLED CHECKS
        // ----------------------------------------*/
        // public static bool AllowFix(HullBreach obj, Credentials credentials) { return !obj.isPatched; }

        // /*----------------------------------------
        // |   INTERACTION PERFORMED LOGIC
        // ----------------------------------------*/
        // public static bool PerformPatch(HullBreach obj, Credentials credentials) { return false; }
        // public static bool PerformCover(HullBreach obj, Credentials credentials) { return false; }
    }
}