using System.Collections.Generic;

namespace StellarRemnants.Interact {
    public interface Interaction {
        public bool ViewInteraction(Interactable obj, Credentials credentials);
        public bool AllowInteraction(Interactable obj, Credentials credentials);
        public bool PerformInteraction(Interactable obj, Credentials credentials);
        public void CompleteInteraction(Interactable obj, Credentials credentials);
        //public void AppendInteractionFields(List<InteractionField> list, InteractionFieldType type, Interactable obj, int slot, Credentials credentials);
    }
    
    /*============================================================================================*/

    public abstract class Interaction<T> : Interaction where T : Interactable {

        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public string LocalizationKey = "Opt_Generic_Unnamed";


        /*----------------------------------------
        |   VIRTUAL METHODS
        ----------------------------------------*/
        public virtual bool View(T obj, Credentials credentials) { return true; }
        public virtual bool Allow(T obj, Credentials credentials) { return true; }
        public virtual bool Perform(T obj, Credentials credentials) { return true; }
        public virtual void Complete(T obj, Credentials credentials) { }
        
        
        /*----------------------------------------
        |   LOCAL METHODS
        ----------------------------------------*/
        public bool ViewInteraction(Interactable obj, Credentials credentials) {
            return View((T)obj, credentials);
        }

        public bool AllowInteraction(Interactable obj, Credentials credentials) {
            return Allow((T)obj, credentials);
        }

        public bool PerformInteraction(Interactable obj, Credentials credentials) {
            return Perform((T)obj, credentials);
        }

        public void CompleteInteraction(Interactable obj, Credentials credentials) {
            Complete((T)obj, credentials);
        }
    }













    // [Obsolete("Replaced with Interaction System v4.")]
    // public interface Interaction2 {
    //     public string LocalizationKey {get; set;}
    //     public bool InteractionEnabled(Interactable obj, Credentials credentials);
    //     public bool InteractionPerform(Interactable obj, Credentials credentials);
    //     public bool TryPerform(Interactable obj, Credentials credentials);
    //     public void AppendInteractionFields(List<InteractionField> list, InteractionFieldType type, Interactable obj, int slot, Credentials credentials);
    // }

    // /*============================================================================================*/

    // [Obsolete("Replaced with Interaction System v4.")]
    // public class Interaction2<T> : Interaction2 where T : Interactable {

    //     /*----------------------------------------
    //     |   DELEGATE FUNCTION DEFINITIONS
    //     ----------------------------------------*/
    //     public delegate bool InteractionMethod(T obj, Credentials credentials);

    //     /*----------------------------------------
    //     |   LOCAL VARIABLES
    //     ----------------------------------------*/
    //     public string LocalizationKey {get; set;}
    //     public bool Visible = true;
    //     protected InteractionMethod EnabledMethod;
    //     protected InteractionMethod PerformMethod;
    //     protected InteractionMethod CompleteMethod;
    //     //protected InteractionMethod ViewMethod;

    //     /*----------------------------------------
    //     |   CONSTRUCTOR(S)
    //     ----------------------------------------*/
    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public Interaction2() {}

    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public Interaction2(string localizationKey, InteractionMethod enableMethod, InteractionMethod performMethod) {
    //         this.Visible = true;
    //         this.LocalizationKey = localizationKey;
    //         EnabledMethod = enableMethod;
    //         PerformMethod = performMethod;
    //     }

    //     // public Interaction(string localizationKey, InteractionEnabledCheck enableFunction, InteractionBehavior performfunction, InteractionVisible visibilityFunction) {
    //     //    this.LocalizationKey = localizationKey;
    //     //    interactionEnabledFunction = enableFunction;
    //     //    interactionPerformFunction = performFunction;
    //     //    InteractionVisible = visibilityFunction;
    //     // }


    //     /*----------------------------------------
    //     |   ENABLED-LOGIC FUNCTIONS
    //     ----------------------------------------*/
    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool IsVisible(Interactable obj, Credentials credentials) {
    //         return Visible || false; // TODO: Use visibility method.
    //     }

    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool InteractionEnabled(Interactable obj, Credentials credentials) {
    //         return TypedInteractionEnabled((T)obj, credentials);
    //     }

    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool TypedInteractionEnabled(T obj, Credentials credentials) {
    //         return EnabledMethod.Invoke(obj, credentials);
    //     }


    //     /*----------------------------------------
    //     |   PERFORM-LOGIC FUNCTIONS
    //     ----------------------------------------*/
    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool InteractionPerform(Interactable obj, Credentials credentials) {
    //         return TypedInteractionPerform((T)obj, credentials);
    //     }

    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool TypedInteractionPerform(T obj, Credentials credentials) {
    //         bool changeMade = PerformMethod.Invoke(obj, credentials);
    //         if(changeMade) {
    //             obj.InvokeStateChange(StateChange.GENERAL);
    //         }
            
    //         return changeMade;
    //     }


    //     /*----------------------------------------
    //     |   TRY-PERFORM FUNCTIONS
    //     ----------------------------------------*/
    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool TryPerform(Interactable obj, Credentials credentials) {
    //         return TypedTryPerform((T)obj, credentials);
    //     }

    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public bool TypedTryPerform(T obj, Credentials credentials) {
    //         if(TypedInteractionEnabled(obj, credentials)) {
    //             return TypedInteractionPerform(obj, credentials); // TODO: Return from that function call is whether or change was made where as TypedTryPerform returns success status. Fix it?
    //         }
    //         return false;
    //     }


    //     /*----------------------------------------
    //     |   MISC FUNCTIONS
    //     ----------------------------------------*/
    //     [Obsolete("Replaced with Interaction System v4.")]
    //     public void AppendInteractionFields(List<InteractionField> list, InteractionFieldType type, Interactable obj, int slot, Credentials credentials) {
    //         list.Add(new InteractionField(type, slot, LocalizationKey, InteractionEnabled(obj, credentials)));
    //     }
    // }
}