namespace StellarRemnants.Interact {
    public class ReactorCore : IntegratedInteractable {

        // public static readonly ObjectState2<ReactorCore> STATE_ONLINE = new ObjectState2<ReactorCore>("Sta_Generic_Online");
        // public static readonly ObjectState2<ReactorCore> STATE_OPEN = new ObjectState2<ReactorCore>("Sta_ReactorCore_Open"); // Can only be opened when offline. Exposes sub-components

        
                
        // This should store a reference to the items rather than the actual SystemModule interactables. The SystemModules are then created when the ReactorCore is opened.

        private IntegratedInteractable[] connectedInteractables; // Should this iterate over the connected interactables or should that be handled by the ship?


    }
}
