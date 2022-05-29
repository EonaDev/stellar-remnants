namespace StellarRemnants.Interact {
    public class ToggleField : TerminalField {
        public Interaction OptionA;
        public Interaction OptionB;

        public ToggleField(string headingLocalizationKey, Interaction a, Interaction b) {
            this.HeadingLocalizationKey = headingLocalizationKey;
            this.OptionA = a;
            this.OptionB = b;
        }
    }
}