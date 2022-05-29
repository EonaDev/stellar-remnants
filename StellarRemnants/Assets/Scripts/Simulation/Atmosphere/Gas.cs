namespace StellarRemnants.Simulation.Atmosphere {
    public class Gas {
        
        /*----------------------------------------
        |   STATIC VARAIBLES
        ----------------------------------------*/
        public static readonly Gas TraceInertGases   = new Gas( 0, GasType.Element,  "trace inert gases");
        public static readonly Gas Hydrogen          = new Gas( 1, GasType.Element,  "hydrogen");
        public static readonly Gas Helium            = new Gas( 2, GasType.Element,  "helium");
        public static readonly Gas HydrogenDeuteride = new Gas( 3, GasType.Compound, "hydrogen deuteride");
        public static readonly Gas WaterVapor        = new Gas( 4, GasType.Compound, "water vapor");
        public static readonly Gas Ammonia           = new Gas( 5, GasType.Compound, "ammonia");
        public static readonly Gas Ozone             = new Gas( 6, GasType.Compound, "ozone");
        public static readonly Gas Nitrogen          = new Gas( 7, GasType.Element,  "nitrogen");
        public static readonly Gas Oxygen            = new Gas( 8, GasType.Element,  "oxygen");
        public static readonly Gas Floruine          = new Gas( 9, GasType.Element,  "flourine");
        public static readonly Gas Neon              = new Gas(10, GasType.Element,  "neon");
        public static readonly Gas SodiumVapor       = new Gas(11, GasType.Element,  "sodium vapor");
        public static readonly Gas MagnesiumVapor    = new Gas(12, GasType.Element,  "magnesium vapor");
        public static readonly Gas AluminumVapor     = new Gas(13, GasType.Element,  "aluminum vapor");
        public static readonly Gas CarbonMonoxide    = new Gas(14, GasType.Compound, "carbon monoxide");
        public static readonly Gas CarbonDioxide     = new Gas(15, GasType.Compound, "carbon dioxide");

        public static readonly Gas Chlorine          = new Gas(17, GasType.Element,  "chlorine");
        public static readonly Gas Argon             = new Gas(18, GasType.Element,  "argon");
        public static readonly Gas PotassiumVapor    = new Gas(19, GasType.Element,  "potassium vapor");
        public static readonly Gas CalciumVapor      = new Gas(20, GasType.Element,  "calcium vapor");
        public static readonly Gas Methane           = new Gas(21, GasType.Compound, "methane");
        public static readonly Gas Ethane            = new Gas(22, GasType.Compound, "ethane");
        public static readonly Gas SulfurDioxide     = new Gas(23, GasType.Compound, "sulfur dioxide");
        public static readonly Gas HydrogenSulfide   = new Gas(24, GasType.Compound, "hydrogen sulfide");
        public static readonly Gas TraceHydrocarbons = new Gas(25, GasType.Compound, "trace hydrocarbons");
        public static readonly Gas IronVapor         = new Gas(26, GasType.Element,  "iron vapor");
        public static readonly Gas Xenon             = new Gas(27, GasType.Element,  "xenon");
        
        public static readonly Gas Krypton           = new Gas(36, GasType.Element,  "krypton");

        public static readonly Gas Radon             = new Gas(43, GasType.Element,  "radon");
        public static readonly Gas Smoke             = new Gas(44, GasType.Particulate, "smoke");
        public static readonly Gas Dust              = new Gas(45, GasType.Particulate, "dust");
        public static readonly Gas RadioactiveDust   = new Gas(46, GasType.Particulate, "radioactive dust");


        /*----------------------------------------
        |   LOCAL VARAIBLES
        ----------------------------------------*/
        //public readonly int Id;
        public readonly string Name;
        public readonly ulong Signature;
        public readonly GasType Type;
        // Pressure/weight?
        // Reactive? Toxic? Should these be handled by other processes?


        /*----------------------------------------
        |   CONSTRUCTOR(S)
        ----------------------------------------*/
        public Gas(int id, GasType type, string name) {
            this.Name = name;
            this.Signature = 1ul << id;
            this.Type = type;
        }


    }

    public enum GasType {
        Element, Compound, Particulate, Biological
    }
}