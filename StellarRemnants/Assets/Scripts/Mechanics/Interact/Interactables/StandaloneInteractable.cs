using StellarRemnants.Scriptables;

namespace StellarRemnants.Interact {
    public abstract class StandaloneInteractable : ObjectInteractable {

        /*----------------------------------------
        |   STATIC VARIABLES
        ----------------------------------------*/
        public static readonly ObjectVariant DEFAULT_VARIANT; // TODO: Apparently this doesn't get set?


        /*----------------------------------------
        |   DATA MEMBERS
        ----------------------------------------*/
        public ObjectVariant Variant = DEFAULT_VARIANT;

        public T GetScopedVariant<T> () where T : ObjectVariant{
            if(Variant != null && Variant.GetType() == typeof(T)) {
                return (T)Variant;
            }
            else {
                // Error.
            }
            return null;
        }


        /*----------------------------------------
        |   IMPLEMENTATIONS - ObjectInteractable
        ----------------------------------------*/
        public override float MaxFocusAngle {get{return Variant.MaxInteractionAngle;}}
        public override float MenuOffset {get{return Variant.MenuOffset;}}
        public override string LocalizationKey {get{return Variant.LocalizationKey;}}
    }
}