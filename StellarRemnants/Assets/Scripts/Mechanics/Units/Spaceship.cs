using UnityEngine;

// TODO: Max dimensions of ships should be approximately 20x15x10, in any orientation. Maybe 25x15x10.
// Smallest ships should have a minimum of 50 cubes of usable space. 
// Larger ships could have upwards of 300 usable cubes on a single floor.

// FLOATING ORIGIN TUTORIAL: https://www.reddit.com/r/Unity3D/comments/t8syrk/vfx_graph_floating_origin_for_infinite_distance/

public class Spaceship : MonoBehaviour {
    public string ShipName;
    public string CallSign;
    public bool InteriorGenerated; // TODO: Remove this variable; just check Interior == null instead. 

    public Vector3Int ChunkPosition {get;set;}

    public ShipInterior Interior;

    bool appliedForce = false; // Temp for testing movement
    void Start() {
        this.name = "Ship #"+CallSign;

        //Rigidbody ring = GameObject.Find("Ring").GetComponent<Rigidbody>();
        //ring.AddTorque(new Vector3(0.1f, 0, 0), ForceMode.VelocityChange);
        /* WORK AROUND: Main camera will not rendered until turned off and back on again. */
        //Camera mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //mainCam.enabled = false; 
        //mainCam.enabled = true;
        /* END OF WORK AROUND */

        if(Interior == null || !InteriorGenerated) {
            // Generate
        }

    }

    void Update() {
        //Debug.Log(mainCam.cullingMask);
        
        // if(!appliedForce) {
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.AddRelativeTorque(new Vector3(0.2f, 0.5f, 0), ForceMode.VelocityChange);
        //     rb.AddForce(new Vector3(0.1f, 0, 0.1f), ForceMode.VelocityChange);
        //     appliedForce = true;
            
        // }
    }



    // public void ConvertTransform(Transform obj) {
    //     // Why don't I just use 2 cameras and 2 world spaces: 1 camera for outerspace and 1 camera for the player in ship-space
    //     // YES: Layered cameras are a much better option. Performance should be easier and won't be as janky.
    //     // Player can be teleported into/out of ship 
    //     // Remember to not render anything in ship-space outside of the ship's bounds.
    //     // Ship exterior and ship interiors can be different layers so one camera does not pick up things visible in the other
    //     // https://www.youtube.com/watch?v=HR-Tf_6uzwc

    //     // Assume object is already in physics ship
    //     Vector3 newPos = visualShip.TransformDirection(physicsShip.InverseTransformDirection(obj.position)) - physicsShip.position + visualShip.position;
    //     Vector3 newForward = visualShip.TransformDirection(obj.forward);
    //     Vector3 newUp = visualShip.TransformDirection(obj.up);


    //     //Debug.Log("Source Input: " + obj.position);
    //     //Debug.Log("Relative Output: " + newPos);

    //     //Debug.Log("Forward: " + obj.forward + " -> " + newForward);
    //     //Debug.Log("Up: " + obj.up + " -> " + newUp);
    //     cam.transform.position = newPos;
    //     //cube.transform.forward = newForward;
    //     //cube.transform.up = newUp;
    //     //cube.transform.SetPositionAndRotation
    //     cam.transform.rotation = Quaternion.LookRotation(newForward, newUp);
        
    // }

    // Called when a game object enters the ship. 
    // public static void OnObjectEnter(GameObject obj) {
    //     if(obj.TryGetComponent<Rigidbody>(out Rigidbody rb) && obj.TryGetComponent<CapsuleCollider>(out CapsuleCollider cc)) {
    //         GameObject physicsProxy = new GameObject("Proxy_" + obj.name);
    //         Vector3 entryPosition = rb.position;
    //         Vector3 entryVelocity = rb.velocity;
    //         Quaternion entryRotation = rb.rotation;
    //         Vector3 entrySpin = rb.angularVelocity;

    //         // Perform transformation of entry velocity (rotation only) and entry position (position only).
    //         Vector3 proxyPosition = entryPosition;
    //         Vector3 proxyVelocity = entryVelocity;
    //         Quaternion proxyRotation = entryRotation;
    //         Vector3 proxySpin = entrySpin;


    //         physicsProxy.transform.position = proxyPosition;
    //         physicsProxy.transform.rotation = proxyRotation;

    //         Rigidbody rb2 = physicsProxy.AddComponent<Rigidbody>();
    //         rb2.velocity = proxyVelocity;
    //         rb2.angularVelocity = proxySpin;
    //         rb2.position = proxyPosition;
    //         rb.rotation = proxyRotation;
    //         // TODO: Not all values are copied over currently.

    //         Destroy(rb);

    //         CapsuleCollider cc2 = CopyComponent<CapsuleCollider>(cc, physicsProxy);

    //         PhysicsSplit visualComponent = obj.AddComponent<PhysicsSplit>();
    //         visualComponent.isPhysicsProxy = false;
    //         visualComponent.linkedObject = physicsProxy;


    //         PhysicsSplit physicsComponent = physicsProxy.AddComponent<PhysicsSplit>();
    //         physicsComponent.isPhysicsProxy = true;
    //         physicsComponent.linkedObject = obj;

    //         //obj.AddComponent(visualComp);
    //     }
    // }

    // public static T CopyComponent<T>(T original, GameObject destination) where T : Component {
    //     System.Type type = original.GetType();
    //     Component copy = destination.AddComponent(type);
    //     System.Reflection.FieldInfo[] fields = type.GetFields();
    //     foreach (System.Reflection.FieldInfo field in fields) {
    //         field.SetValue(copy, field.GetValue(original));
    //     }
    //     return copy as T;
    // }

    public void MoveOrigin(Vector3Int offset) {
        ChunkPosition += offset;
    }
}
