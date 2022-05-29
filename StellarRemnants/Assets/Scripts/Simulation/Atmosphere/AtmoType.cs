using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not sure this enum will actually be used.
namespace StellarRemnants.Simulation.Atmosphere {
    public enum AtmoType {
        TENUOUS, // Basically none
        ROCK_VAPOR,// Like Mercury
        RUNAWAY_GREENHOUSE, // Like Venus
        SULFERIC, // Like Io
        MARTIAN,
        EARTHLIKE,
        JOVIAN,
        ICE_GIANT, // Like Neptune and Uranus
        HOT_GIANT,
        BROWN_DWARF,
    }
}
