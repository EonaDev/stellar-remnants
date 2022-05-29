using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform follow;

    public Camera mainCamera;
    private SubsceneProxy proxy;
    
    public Transform originSpaceTransform; // ship exterior
    private Transform mainCamTransform;

    public Transform proxySpaceTransform; // ship interior
    
    public bool flipped = true; // If true, main cam is in subscene and proxy is in main space.

    void Start() {
        mainCamTransform = mainCamera.transform;
        proxy = CreateProxyCamera(proxySpaceTransform, 256);
        
        mainCamera.enabled = false; 
        mainCamera.enabled = true;

        proxy.camera.enabled = false;
        proxy.camera.enabled = true;

        


    }

    

    void Update() {
        // mainCamTransform.SetPositionAndRotation(follow.position, follow.rotation);

        // if(flipped) {
        //     Vector3 newForward = originSpaceTransform.TransformDirection(mainCamTransform.forward);
        //     Vector3 newUp = originSpaceTransform.TransformDirection(mainCamTransform.up);

        //     proxy.camera.transform.position = originSpaceTransform.TransformDirection(mainCamTransform.position) - proxy.transform.position + originSpaceTransform.position;
        //     proxy.camera.transform.rotation = Quaternion.LookRotation(newForward, newUp);

        // }
        // else {
        //     Vector3 newForward = originSpaceTransform.InverseTransformDirection(mainCamTransform.forward);
        //     Vector3 newUp = originSpaceTransform.InverseTransformDirection(mainCamTransform.up);

        //     proxy.camera.transform.position = originSpaceTransform.InverseTransformDirection(mainCamTransform.position - originSpaceTransform.position) + proxy.transform.position;
        //     proxy.camera.transform.rotation = Quaternion.LookRotation(newForward, newUp);
        // }
    }

    // public void flip() {
    //     Debug.Log("Flipping!");
    //     flipped = !flipped;
    //     int cullingMask = mainCamera.cullingMask;
    //     CameraClearFlags clearFlag = mainCamera.clearFlags;

    //     mainCamera.cullingMask = proxy.camera.cullingMask;
    //     mainCamera.clearFlags = proxy.camera.clearFlags;
    //     proxy.camera.clearFlags = clearFlag;
    //     proxy.camera.cullingMask = cullingMask;
        
        
    //     if(flipped) { // Why does this make it work? lol
    //         resetMain();
    //     }
    //     else {
    //         resetProxy();
    //     }
        
        
    // }

    public bool SetFlipped() {
        if(flipped) {
            return false;
        }

        flipped = true;
        int cullingMask = mainCamera.cullingMask;
        CameraClearFlags clearFlag = mainCamera.clearFlags;
        mainCamera.cullingMask = proxy.camera.cullingMask;
        mainCamera.clearFlags = proxy.camera.clearFlags;
        proxy.camera.clearFlags = clearFlag;
        proxy.camera.cullingMask = cullingMask;

        resetMain();
        return true;
    }

    public bool SetUnflipped() {
        if(!flipped) {
            return false;
        }

        flipped = false;
        int cullingMask = mainCamera.cullingMask;
        CameraClearFlags clearFlag = mainCamera.clearFlags;
        mainCamera.cullingMask = proxy.camera.cullingMask;
        mainCamera.clearFlags = proxy.camera.clearFlags;
        proxy.camera.clearFlags = clearFlag;
        proxy.camera.cullingMask = cullingMask;

        resetProxy();
        return true;
    }

    public void resetProxy() {

        proxy.camera.enabled = false;
        proxy.camera.enabled = true;
    }

    public void resetMain() {
        //mainCamera.Render
        //mainCamera.reset
        mainCamera.enabled = false;
        mainCamera.enabled = true;
    }

    private SubsceneProxy CreateProxyCamera(Transform proxySpaceTransform, int cullingMask) {
        GameObject proxyObj = new GameObject("proxy1");
        Camera newCam = proxyObj.AddComponent<Camera>();
        newCam.cullingMask = cullingMask;
        newCam.clearFlags = CameraClearFlags.Nothing;
        newCam.nearClipPlane = mainCamera.nearClipPlane;
        newCam.farClipPlane = mainCamera.farClipPlane;
        
        return new SubsceneProxy(newCam, proxySpaceTransform);
    }

    public struct SubsceneProxy {
        public Camera camera;
        public Transform transform;

        public SubsceneProxy(Camera camera, Transform space) {
            this.camera = camera;
            this.transform = space;
        }
    }
}