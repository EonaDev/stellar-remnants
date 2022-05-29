using UnityEngine;

public class ReversePointGravityField : GravityField {
    [SerializeField] public Vector3 gravityPoint;

    public ReversePointGravityField() {
        this.gravity = -9.81f;
        this.gravityPoint = Vector3.zero; // Default to Vector3.right; East (+X) is considered standard direction of rotation, so Vector3.right would be the standard axis of rotation.
    }

    public ReversePointGravityField(float gravity, Vector3 gravityPoint) {
        this.gravity = gravity;
        this.gravityPoint = gravityPoint;
    }

    void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IGravitatable>(out IGravitatable obj)) {
            obj.setGravity(GetUp(other.transform.position), gravity);
        }
    }

    // TODO: It might be computationally cheaper to return distance and up vector at the same time.
    public override Vector3 GetUp(Vector3 pos) {
        return (gravityPoint - pos).normalized;
    }
}
