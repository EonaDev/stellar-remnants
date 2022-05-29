namespace StellarRemnants.Interact {
    public interface AutoActivatable : Interactable {
        public bool AutoActivate {get; set;}
        public void OnAutoActivation();

        public static readonly Interaction<AutoActivatable> OPT_AUTOACTIVATE_ON = new AutoActivateOn{LocalizationKey = "Opt_Generic_AutoActivateOn"};
        public static readonly Interaction<AutoActivatable> OPT_AUTOACTIVATE_OFF = new AutoActivateOff{LocalizationKey = "Opt_Generic_AutoActivateOff"};
        public static readonly Interaction<AutoActivatable> OPT_TRIGGER_AUTOACTIVATE = new TriggerAutoActivate{LocalizationKey = "Opt_Generic_AutoActivateTrigger"};

        public void OnAutoActivationChange(bool enabled) {
            AutoActivate = enabled;
        }

        public bool OnAutoActivationPerform(bool enabled, Credentials credentials) {
            return true;
        }

        private class AutoActivateOn : Interaction<AutoActivatable> {
            public override bool Allow(AutoActivatable obj, Credentials credentials) { return !obj.AutoActivate; }
            public override bool Perform(AutoActivatable obj, Credentials credentials) { return true; } // TODO: Perform 
            public override void Complete(AutoActivatable obj, Credentials credentials) { obj.OnAutoActivationChange(true); }
        }

        private class AutoActivateOff : Interaction<AutoActivatable> {
            public override bool Allow(AutoActivatable obj, Credentials credentials) { return obj.AutoActivate; }
            public override bool Perform(AutoActivatable obj, Credentials credentials) { return true; }
            public override void Complete(AutoActivatable obj, Credentials credentials) { obj.OnAutoActivationChange(false); }
        }

        private class TriggerAutoActivate : Interaction<AutoActivatable> {
            public override bool Allow(AutoActivatable obj, Credentials credentials) { return obj.AutoActivate; }
            public override void Complete(AutoActivatable obj, Credentials credentials) { obj.OnAutoActivation(); }
        }
    }
}