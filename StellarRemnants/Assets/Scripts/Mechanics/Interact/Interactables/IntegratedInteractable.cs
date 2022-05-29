using UnityEngine;
using StellarRemnants.Scriptables;

namespace StellarRemnants.Interact {
    public abstract class IntegratedInteractable : ObjectInteractable {

        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Interaction<IntegratedInteractable> OPT_TERMINAL = new Terminal{LocalizationKey = "Opt_Generic_Terminal"};
        public static readonly Interaction<IntegratedInteractable> OPT_BOOT = new Boot{LocalizationKey = "Opt_Generic_Boot"};
        public static readonly Interaction<IntegratedInteractable> OPT_SHUTDOWN = new Shutdown{LocalizationKey = "Opt_Generic_Shutdown"};
        
        public static readonly ObjectState STATE_OFFLINE = new ObjectState<IntegratedInteractable>("Sta_Generic_Offline", opt1: OPT_BOOT); // What about interactables that shouldn't have boot option? Custom offline state.
        public static readonly ObjectState STATE_UNPOWERED = new ObjectState<IntegratedInteractable>("Sta_Generic_Unpowered");

        public static readonly IntegratedVariant DEFAULT_VARIANT;


        /*----------------------------------------
        |   DATA MEMBERS
        ----------------------------------------*/
        public StructureRoom Room;
        public IntegratedVariant Variant = DEFAULT_VARIANT;
        public TechType Technology {get => Variant.Technology;}
        public float PowerDraw;
        public bool Online; // Whether or not the object is functioning and powered. Separate into powered vs enabled?
        public bool IsSafeshutdown;
        public bool LocalTerminalAccess {get => Variant.LocalTerminalAccess;}
        
        [Range(1, 5)]public int Priority; // Is this neccesary? This data also exists in the form of which List the object is included with in IntegratedSystem


        /*----------------------------------------
        |   IMPLEMENTATIONS - ObjectInteractable
        ----------------------------------------*/
        public override float MaxFocusAngle {get{return Variant.MaxInteractionAngle;}}
        public override float MenuOffset {get{return Variant.MenuOffset;}}
        public override string LocalizationKey {get{return Variant.LocalizationKey;}}


        /*----------------------------------------
        |   BASIC METHODS
        ----------------------------------------*/
        public bool PerformBoot(bool autoboot) {
            Online = true;
            OnBootComplete();
            InvokeStateChange(StateChange.Startup);
            return false; // Return false only when it fails to boot for some internal reason.
        }

        public bool PerformShutdown(bool safeShutdown) {
            if(Online) {
                if(!safeShutdown) {
                    Online = false;
                    IsSafeshutdown = safeShutdown;
                    // Call some function to change state?
                    OnShutdownComplete();
                    InvokeStateChange(StateChange.PowerOutage); // TODO: Is this necessary? It will probably call some interaction.
                    return true;
                }
                else {
                    // TODO: if(can shutdown)
                    Online = false;
                    IsSafeshutdown = safeShutdown;
                    OnShutdownComplete();
                    InvokeStateChange(StateChange.Shutdown); // TODO: Is this necessary? It will probably call some interaction.
                    return true;
                }
            }
            return false;
        }

        public float DrawPower(float source, float deltaTime) { // Returns remaining power.
            if(Online) {
                float draw = PowerDraw * deltaTime;
                if(draw <= source) {
                    source -= draw;
                }
                else {
                    PerformShutdown(false);
                }
            }
            else if(!IsSafeshutdown) {
                float draw = PowerDraw * deltaTime;
                if(draw <= source && PerformBoot(true)) {
                    source -= draw;
                }
            }
            return source;
        }

        public T GetScopedVariant<T> () where T : IntegratedVariant{
            if(Variant != null && Variant.GetType() == typeof(T)) {
                return (T)Variant;
            }
            else {
                // Error.
            }
            return null;
        }


        /*----------------------------------------
        |   VIRTUAL METHODS
        ----------------------------------------*/
        public virtual bool OnBootCheck(Credentials credentials) { return true; }
        public virtual bool OnShutdownCheck(Credentials credentials) { return true; }
        public virtual bool OnShutdownPerform(Credentials credentials) { return true; }
        public virtual bool OnBootPerform(Credentials credentials) { return true; }
        public virtual void OnBootComplete() {}
        public virtual void OnShutdownComplete() {}


        // - Visibile on terminals 
        // - Can be toggled off


        // TODO: Implement compute cycle usage
        // TODO: Implement terminal option
        // TODO: Register object on ship/building list?

        
        


        /*----------------------------------------
        |   INTERACTIONS
        ----------------------------------------*/
        private class Terminal : Interaction<IntegratedInteractable> {
            public override bool Allow(IntegratedInteractable obj, Credentials credentials) { return obj.LocalTerminalAccess; }
            public override bool Perform(IntegratedInteractable obj, Credentials credentials) { return obj.LocalTerminalAccess; } // TODO: Check credentials.
            public override void Complete(IntegratedInteractable obj, Credentials credentials) {  }
        }

        private class Boot : Interaction<IntegratedInteractable> {
            public override bool Allow(IntegratedInteractable obj, Credentials credentials) { return !obj.Online && obj.OnBootCheck(credentials); }
            public override bool Perform(IntegratedInteractable obj, Credentials credentials) { return obj.OnBootPerform(credentials); }
            public override void Complete(IntegratedInteractable obj, Credentials credentials) { obj.OnBootComplete(); }
        }

        private class Shutdown : Interaction<IntegratedInteractable> {
            public override bool Allow(IntegratedInteractable obj, Credentials credentials) { return obj.Online && obj.OnShutdownCheck(credentials); }
            public override bool Perform(IntegratedInteractable obj, Credentials credentials) { return obj.OnShutdownPerform(credentials);; }
            public override void Complete(IntegratedInteractable obj, Credentials credentials) { obj.OnShutdownComplete(); }
        }


    }
}