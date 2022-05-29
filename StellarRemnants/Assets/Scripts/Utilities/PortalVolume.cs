using UnityEngine;
using StellarRemnants;

public class PortalVolume : MonoBehaviour {
    public Transform from;
    public Transform to;
    private int layer;
    public bool enter;
    public CameraController cameraController;
    public UrpCameraController cameraController2;
    public Rigidbody volumeBody;

    void Start() {
        layer = to.gameObject.layer;
    }

    void OnTriggerEnter(Collider other) {
        
        if(enter && other.gameObject != this.gameObject && other.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
            if(other.name.Equals("Player")) {
                //bool result = cameraController.SetFlipped(); // TODO: For some reason, the camera tries to flip anyway. Or perhaps isn't updating to the new location immediately.
                bool result = cameraController2.FollowBaseCamera();
                if(result) {
                    return;
                }
            }
            
            SetLayerRecursively(other.gameObject, layer);
            Vector3 newForward = to.TransformDirection(from.InverseTransformDirection(other.transform.forward));
            Vector3 newUp = to.TransformDirection(from.InverseTransformDirection(other.transform.up));
            Vector3 pos = to.TransformPoint(from.InverseTransformPoint(other.transform.position));
            other.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(newForward, newUp));
            rb.velocity = to.TransformDirection(from.InverseTransformDirection(rb.velocity-volumeBody.velocity));
            
        }
    }

    void OnTriggerExit(Collider other) {
        if(!enter && other.gameObject != this.gameObject && other.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
            if(other.name.Equals("Player")) {
                //cameraController.flip();
                //bool result = cameraController.SetUnflipped();
                bool result = cameraController2.FollowOverlayCamera();
                if(result) {
                    return;
                }
            }

            SetLayerRecursively(other.gameObject, layer);
            Vector3 newForward = to.TransformDirection(from.InverseTransformDirection(other.transform.forward));
            Vector3 newUp = to.TransformDirection(from.InverseTransformDirection(other.transform.up));
            Vector3 pos = to.TransformPoint(from.InverseTransformPoint(other.transform.position));
            other.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(newForward, newUp));
            rb.velocity = to.TransformDirection(from.InverseTransformDirection(rb.velocity+volumeBody.velocity));
        }
    }



    void SetLayerRecursively(GameObject obj, int newLayer) {
        if (null == obj) {
            return;
        }
        
        obj.layer = newLayer;
        
        foreach (Transform child in obj.transform) {
            if (null == child) {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}