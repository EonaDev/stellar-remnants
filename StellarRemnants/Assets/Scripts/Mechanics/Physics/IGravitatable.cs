using UnityEngine;

public interface IGravitatable {
    void setGravityField(GravityField gravityField);
    void setGravity(Vector3 gravityNormal, float gravityStrength);
    void resetGravity();
    //void addGravity(Vector3 gravity);
}