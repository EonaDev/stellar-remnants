namespace StellarRemnants.Interact {
    public class DamagedWire : DamageInteractable {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        // public static readonly Interaction2<DamagedWire> OPT_REPAIR = new Interaction2<DamagedWire>("Opt_Interactable_Repair", AllowFix, PerformFix);

        // public static readonly ObjectState2<DamagedWire> STATE_DAMAGED = new ObjectState2<DamagedWire>("Sta_Wire_Damaged", opt1: OPT_REPAIR);
        // public static readonly ObjectState2<DamagedWire> STATE_REPAIRED = new ObjectState2<DamagedWire>("Sta_Wire_Repaired");


        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public IntegratedInteractable AffectedInteractable;
        public bool Repaired = true;
        
        void Awake() {
            // TODO: Set all variables here.
            
        }

        void Start() {
            // TODO: Set all references here, including those used in ObjectInteractables.
            
        }


        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public bool IsPowered() {
            return AffectedInteractable.Room.IsPowered;
        }
        

        /*----------------------------------------
        |   INTERACTION ENABLED CHECKS
        ----------------------------------------*/
        public static bool AllowFix(DamagedWire obj, Credentials credentials) { return !obj.Repaired; }


        /*----------------------------------------
        |   INTERACTION PERFORMED LOGIC
        ----------------------------------------*/
        public static bool PerformFix(DamagedWire obj, Credentials credentials) { 
            if(obj.IsPowered()) {
                // Shock the unit attempting the repair.
            }
            else {
                // Safely repair.
            }
            return false; 
        }
    }
}