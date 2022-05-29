using System.Collections.Generic;
using StellarRemnants.Interact;

namespace StellarRemnants {
    public class Damage {
        
        /*----------------------------------------
        |   LOCAL VARIABLES
        ----------------------------------------*/
        Interactable source;
        List<(DamageType type, float damage)> damageComponents = new List<(DamageType, float)>();


        /*----------------------------------------
        |   CONSTRUCTOR(S)
        ----------------------------------------*/
        public Damage(Interactable source) {
            this.source = source;
        }

        public Damage(Interactable source, DamageType type, float damage) {
            this.source = source;
            Add(type, damage);
        }


        /*----------------------------------------
        |   BASIC FUNCTIONS
        ----------------------------------------*/
        public void Add(DamageType type, float damage) {
            damageComponents.Add((type, damage));
        }
    }

    /*============================================================================================*/

    public enum DamageType {
        KINETIC,
        PLASMA,
        CRYSTAL,
        HARDLIGHT,
        EXPLOSIVE,
        EMP,
        LASER,
        SONIC,
        THERMAL,
        CORROSIVE,
        ELECTRICAL,
        IONIC,
        NUCLEAR,
        COLLISION,
        FALL
    }
}