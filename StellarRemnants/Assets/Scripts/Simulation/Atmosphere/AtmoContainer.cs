namespace StellarRemnants.Simulation.Atmosphere {
    public interface AtmoContainer {
        public AtmoContainer GetAdjacent(Threshold threshold) {
            return threshold.GetAdjacent(this);
        }

        public AtmoVolume GetAtmosphere();

        //public void SetEqualized();
    }
}