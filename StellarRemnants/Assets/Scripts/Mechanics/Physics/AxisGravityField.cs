using UnityEngine;

public class AxisGravityField : GravityField {
    // Start is called before the first frame update
    [SerializeField] public Vector3 gravityAxis;
    public float surfaceLevel; // Distance from point in which the gravity strength is 100%; anything closer to the axis will experience reduced gravity, but any further away will not.

    public AxisGravityField() {
        this.gravity = -9.81f;
        this.gravityAxis = Vector3.right; // Default to Vector3.right; East (+X) is considered standard direction of rotation, so Vector3.right would be the standard axis of rotation.
    }

    public AxisGravityField(float gravity, Vector3 gravityAxis) {
        this.gravity = gravity;
        this.gravityAxis = gravityAxis;
    }

    void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IGravitatable>(out IGravitatable obj)) {
            //obj.setGravity(GetUp(other.transform.position), gravity);
        }
    }

    // On rotating ring, gravity = rotation_rate^2 * radius

    // TODO: It might be computationally cheaper to return distance and up vector at the same time.

    public override Vector3 GetUp(Vector3 pos) {
        Vector3 up = -Vector3.ProjectOnPlane(pos-this.transform.position, gravityAxis).normalized;
        Debug.Log("Up: " + up + " @ " + pos.ToString());
        return up; // TODO: Verify this works
    }

    public override void applyGravity(IGravitatable obj, Vector3 pos) {
        obj.setGravity(GetUp(pos), gravity);

    }
}