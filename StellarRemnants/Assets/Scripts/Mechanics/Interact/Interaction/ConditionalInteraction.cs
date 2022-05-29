using System.Collections.Generic;

namespace StellarRemnants.Interact {
    public class ConditionalInteraction<T> : Interaction<T> where T : Interactable {

        // public delegate bool InteractionCondition(T obj, Credentials credentials);

        // public ConditionalInteraction(params (string condition, Interaction2<T> interaction)[] interactions) {

        //     // TODO: Store all conditions and interactions. 
        //     // TODO: Find a better way to handle conditionals.
        // }


    }

    public enum InteractionCondition {
        NONE,
    }
}