using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace StellarRemnants.Core {
    public class OriginManager : MonoBehaviour {
        public float Threshold = 1000f*1000f;
        public Vector3d OriginOffset = Vector3d.zero;
        
        // https://forum.unity.com/threads/multiplayer-floating-origin.1094215/

        void LateUpdate() {
            Vector3 cameraPosition = Vector3.zero;

            if(cameraPosition.magnitude > Threshold) {
                foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects()) {
                    g.transform.position -= cameraPosition;
                }
                OriginOffset -= cameraPosition;
            }
        }
    }
}