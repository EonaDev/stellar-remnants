namespace StellarRemnants.Interact {
    public struct InteractionField {
        InteractionFieldType type;
        public int slot; // Basically, which of the 4* interaction options it is: E, R, F, T
        public string text;
        public bool enabled;

        public InteractionField(InteractionFieldType type, int slot, string text, bool enabled) {
            this.type = type;
            this.slot = slot;
            this.text = text;
            this.enabled = enabled;
        }
    }

    public enum InteractionFieldType {
        TITLE,
        STATE,
        OPTION_PRESS,
        OPTION_FORWARD,
        OPTION_HOLD,
        OPTION_SPAM
    }
}