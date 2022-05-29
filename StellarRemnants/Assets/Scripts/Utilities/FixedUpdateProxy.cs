using UnityEngine;

public class FixedUpdateProxy : MonoBehaviour {
    public Transform SourceTransform;
    public Transform OriginTransform;
    public Transform DestinationTransform;

    void FixedUpdate() {
        Vector3 newForward = DestinationTransform.TransformDirection(SourceTransform.forward);
        Vector3 newUp = DestinationTransform.TransformDirection(SourceTransform.up);

        transform.position = DestinationTransform.TransformDirection(OriginTransform.InverseTransformDirection(SourceTransform.position)) - OriginTransform.position + DestinationTransform.position;
        transform.rotation = Quaternion.LookRotation(newForward, newUp);
    }
    
    // TODO: Transfer collisions from mimc to source somehow. This component will be used for collision while the player is in "ship-space", but is still outside of the ship.
}