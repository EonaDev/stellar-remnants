using System;

namespace StellarRemnants.Interact {
    public interface Interactable {
        public Interaction GetInteraction1();
        public Interaction GetInteraction2();
        public Interaction GetInteraction3();
        public Interaction GetInteraction4();
        public float MenuOffset {get;}
        public void InvokeStateChange(StateChange type);
        public void AddStateListener(Action<Interactable, StateChange> function);
        public void RemoveStateListener(Action<Interactable, StateChange> function);
        public string LocalizationKey {get;}
        public InteractionField[] BuildFieldList(Credentials credential);

    }
}