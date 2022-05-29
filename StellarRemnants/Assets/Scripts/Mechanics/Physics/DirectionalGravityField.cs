using UnityEngine;

public class DirectionalGravityField : GravityField {
    [SerializeField] public Vector3 gravityNormal;

    public DirectionalGravityField() {
        this.gravity = -9.81f;
        this.gravityNormal = Vector3.up;
    }

    public DirectionalGravityField(float gravity, Vector3 gravityNormal) {
        this.gravity = gravity;
        this.gravityNormal = gravityNormal;
    }

    void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IGravitatable>(out IGravitatable obj)) {
            obj.setGravity(gravityNormal, gravity);
        }
    }

    public override Vector3 GetUp(Vector3 pos) {
        return gravityNormal;
    }
}