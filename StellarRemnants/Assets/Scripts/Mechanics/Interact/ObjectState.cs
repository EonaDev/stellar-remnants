namespace StellarRemnants.Interact {

    public interface ObjectState {
        public string LocalizationKey {get;}
        public bool Focusable {get;}
        public Interaction GetInteraction1();
        public Interaction GetInteraction2();
        public Interaction GetInteraction3();
        public Interaction GetInteraction4();
        public Interaction GetInteraction5();

        public Interaction GetAltInteraction1();
        public Interaction GetAltInteraction2();
        public Interaction GetAltInteraction3();
        public Interaction GetAltInteraction4();
    }

    public class ObjectState<T> : ObjectState where T : ObjectInteractable {
        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        public string LocalizationKey {get;}
        public bool Focusable {get;}
        public Interaction Interaction1; //public Interaction<T> Interaction1;
        public Interaction Interaction2;
        public Interaction Interaction3;
        public Interaction Interaction4;
        public Interaction Interaction5;

        public Interaction AltInteraction1; // public Interaction<T> AltInteraction1;
        public Interaction AltInteraction2;
        public Interaction AltInteraction3;
        public Interaction AltInteraction4;



        public ObjectState(string localizationKey) {
            LocalizationKey = localizationKey;
            Focusable = false;
        }

        public ObjectState(string localizationKey, 
        Interaction opt1 = null, Interaction opt2 = null, Interaction opt3 = null, Interaction opt4 = null, Interaction opt5 = null,
        Interaction alt1 = null, Interaction alt2 = null, Interaction alt3 = null, Interaction alt4 = null) { // Interaction<T> opt1 = null,
            LocalizationKey = localizationKey;
            Focusable = true;
            Interaction1 = opt1;
            Interaction2 = opt2;
            Interaction3 = opt3;
            Interaction4 = opt4;
            Interaction5 = opt5;
            AltInteraction1 = alt1;
            AltInteraction2 = alt2;
            AltInteraction3 = alt3;
            AltInteraction4 = alt4;
        }


        public Interaction GetInteraction1() { return Interaction1; }
        public Interaction GetInteraction2() { return Interaction2; }
        public Interaction GetInteraction3() { return Interaction3; }
        public Interaction GetInteraction4() { return Interaction4; }
        public Interaction GetInteraction5() { return Interaction5; }

        public Interaction GetAltInteraction1() { return AltInteraction1; }
        public Interaction GetAltInteraction2() { return AltInteraction2; }
        public Interaction GetAltInteraction3() { return AltInteraction3; }
        public Interaction GetAltInteraction4() { return AltInteraction4; }
    }



















    // public interface ObjectState2 {
    //     public string LocalizationKey {get;}
    //     public bool Focusable {get;}
    //     public Interaction2 GetInteraction1();
    //     public Interaction2 GetInteraction2();
    //     public Interaction2 GetInteraction3();
    //     public Interaction2 GetInteraction4();
    //     public Interaction2 GetInteraction5();

    //     public Interaction2 GetAltInteraction1();
    //     public Interaction2 GetAltInteraction2();
    //     public Interaction2 GetAltInteraction3();
    //     public Interaction2 GetAltInteraction4();
    // }

    // /*============================================================================================*/
    
    // public class ObjectState2<T> : ObjectState2 where T : ObjectInteractable {
        
    //     /*----------------------------------------
    //     |   LOCAL VARIABLES
    //     ----------------------------------------*/
    //     public string LocalizationKey {get;}
    //     public bool Focusable {get;}
    //     public Interaction2<T> Interaction1;
    //     public Interaction2<T> Interaction2;
    //     public Interaction2<T> Interaction3;
    //     public Interaction2<T> Interaction4;
    //     public Interaction2<T> Interaction5;

    //     public Interaction2<T> AltInteraction1;
    //     public Interaction2<T> AltInteraction2;
    //     public Interaction2<T> AltInteraction3;
    //     public Interaction2<T> AltInteraction4;


    //     /*----------------------------------------
    //     |   CONSTRUCTOR(S)
    //     ----------------------------------------*/
    //     public ObjectState2(string localizationKey) {
    //         LocalizationKey = localizationKey;
    //         Focusable = false;
    //     }

    //     public ObjectState2(string stateName, 
    //     Interaction2<T> opt1 = null, Interaction2<T> opt2 = null, Interaction2<T> opt3 = null, Interaction2<T> opt4 = null, Interaction2<T> opt5 = null,
    //     Interaction2<T> alt1 = null, Interaction2<T> alt2 = null, Interaction2<T> alt3 = null, Interaction2<T> alt4 = null) {
    //         LocalizationKey = stateName;
    //         Focusable = true;
    //         Interaction1 = opt1;
    //         Interaction2 = opt2;
    //         Interaction3 = opt3;
    //         Interaction4 = opt4;
    //         Interaction5 = opt5;
    //         AltInteraction1 = alt1;
    //         AltInteraction2 = alt2;
    //         AltInteraction3 = alt3;
    //         AltInteraction4 = alt4;
    //     }
        

    //     /*----------------------------------------
    //     |   INTERFACE FUNCTION IMPLEMENTATIONS
    //     ----------------------------------------*/
    //     public Interaction2 GetInteraction1() { return Interaction1; }
    //     public Interaction2 GetInteraction2() { return Interaction2; }
    //     public Interaction2 GetInteraction3() { return Interaction3; }
    //     public Interaction2 GetInteraction4() { return Interaction4; }
    //     public Interaction2 GetInteraction5() { return Interaction5; }

    //     public Interaction2 GetAltInteraction1() { return AltInteraction1; }
    //     public Interaction2 GetAltInteraction2() { return AltInteraction2; }
    //     public Interaction2 GetAltInteraction3() { return AltInteraction3; }
    //     public Interaction2 GetAltInteraction4() { return AltInteraction4; }


    //     /*----------------------------------------
    //     |   BASIC FUNCTIONS
    //     ----------------------------------------*/
    //     public Interaction2<T> GetTypedInteraction1() { return Interaction1; }
    //     public Interaction2<T> GetTypedInteraction2() { return Interaction2; }
    //     public Interaction2<T> GetTypedInteraction3() { return Interaction3; }
    //     public Interaction2<T> GetTypedInteraction4() { return Interaction4; }
    //     public Interaction2<T> GetTypedInteraction5() { return Interaction5; }

    //     public Interaction2<T> GetTypedInteraction1Alt() { return AltInteraction1; }
    //     public Interaction2<T> GetTypedInteraction2Alt() { return AltInteraction2; }
    //     public Interaction2<T> GetTypedInteraction3Alt() { return AltInteraction3; }
    //     public Interaction2<T> GetTypedInteraction4Alt() { return AltInteraction4; }
    // }
}