namespace StellarRemnants {
    // TODO: REDO ALL DAMAGE STUFF.
    // Damage objects should handle what type of damage they do. They should be able to do multiple types in different ratios.
    // Damageable objects should handle the resistances/weaknesses to certain types.


    public interface Damageable {
        public float GetMaxHealth();
        public void ReceiveDamage(float amount, Damage damageType);
        public void OnDamage(float percentOfTotal);
        public void OnDeath();
        public float Health{get;set;}

        
    }

    public abstract class HealthType {
        
    }

    public class Health {
        public float Amount;
        public HealthType type;
        
        public void ReceiveDamage(float amount, Damage damageType) {

            //damageable.Get
        }

    }


}