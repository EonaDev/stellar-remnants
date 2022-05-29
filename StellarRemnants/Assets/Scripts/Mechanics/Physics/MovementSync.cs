using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSync : MonoBehaviour {
    public Transform source;

    public void LateUpdate() {
        this.transform.SetPositionAndRotation(source.position, source.rotation);
    }
}
