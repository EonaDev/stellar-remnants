using UnityEngine;

public class LateUpdateProxy : MonoBehaviour {
    public Transform SourceTransform; // Player
    public Transform OriginTransform; // Interior
    public Transform DestinationTransform; // Exterior

    void LateUpdate() {
        Vector3 newForward = DestinationTransform.TransformDirection(SourceTransform.forward);
        Vector3 newUp = DestinationTransform.TransformDirection(SourceTransform.up);

        transform.position = DestinationTransform.TransformDirection(OriginTransform.InverseTransformDirection(SourceTransform.position)) - OriginTransform.position + DestinationTransform.position;
        transform.rotation = Quaternion.LookRotation(newForward, newUp);
    }

    public void Flip(bool moveSource) {
        Transform t = OriginTransform;
        OriginTransform = DestinationTransform;
        DestinationTransform = t;

        if(moveSource) {
            // TODO: Move source to new position.
        }
        
    }
}