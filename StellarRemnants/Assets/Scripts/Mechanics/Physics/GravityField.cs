using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class GravityField : MonoBehaviour {
    [SerializeField] public float gravity;

    public virtual Vector3 GetUp(Vector3 pos) { return Vector3.up; }
    // TODO: Impement this as different gravity types: linear (on ships), point (on planets), and axis (on rings).

    void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IGravitatable>(out IGravitatable obj)) {
            //obj.setGravityField(this);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.TryGetComponent<IGravitatable>(out IGravitatable obj)) {
            //obj.resetGravity();
        }
    }

    public virtual void applyGravity(IGravitatable obj, Vector3 pos) {
        obj.setGravity(GetUp(pos), gravity);
        //rb.AddForce(GetUp(rb.transform.position) * gravity, ForceMode.Acceleration);

    }

    
}