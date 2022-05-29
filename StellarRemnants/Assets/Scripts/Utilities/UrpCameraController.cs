using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants {
    public class UrpCameraController : MonoBehaviour {
        public Transform follow; // Usually player head
        public Transform OriginSpaceTransform;
        public Transform ProxySpaceTransform;
        public Camera OverlayCamera;
        public Camera BaseCamera;
        public Camera SkyCamera;

        private Transform overlayCamTransform;
        private Transform baseCamTransform;
        private Transform skyCamTransform;
        public bool followOverlay = false;

        void Start() {
            overlayCamTransform = OverlayCamera.transform;
            baseCamTransform = BaseCamera.transform;
            skyCamTransform = SkyCamera.transform;
            
            OverlayCamera.enabled = false; 
            OverlayCamera.enabled = true;

            BaseCamera.enabled = false;
            BaseCamera.enabled = true;

            SkyCamera.enabled = false;
            SkyCamera.enabled = true;
        }



        void Update() {
            UpdateCameraPositions();
        }

        private void UpdateCameraPositions() {
            skyCamTransform.rotation = follow.rotation;
            // Set position as a portion of follow position.
            if(followOverlay) { 
                // This is outside of ships
                overlayCamTransform.SetPositionAndRotation(follow.position, follow.rotation);

                // TODO: There is room to optimize a little here. Write custom implementation of TransformDirection/InverseTransformDirection so that any quaternions are not recalculated.
                //       Or maybe something with matrices? 
                Vector3 newForward = OriginSpaceTransform.InverseTransformDirection(overlayCamTransform.forward); 
                Vector3 newUp = OriginSpaceTransform.InverseTransformDirection(overlayCamTransform.up);

                baseCamTransform.position = OriginSpaceTransform.InverseTransformDirection(overlayCamTransform.position - OriginSpaceTransform.position) + ProxySpaceTransform.position;
                baseCamTransform.rotation = Quaternion.LookRotation(newForward, newUp);
            }
            else {
                // This is inside of ships
                baseCamTransform.SetPositionAndRotation(follow.position, follow.rotation);

                Vector3 newForward = OriginSpaceTransform.TransformDirection(baseCamTransform.forward);
                Vector3 newUp = OriginSpaceTransform.TransformDirection(baseCamTransform.up);

                overlayCamTransform.position = OriginSpaceTransform.TransformDirection(baseCamTransform.position) - ProxySpaceTransform.position + OriginSpaceTransform.position;
                overlayCamTransform.rotation = Quaternion.LookRotation(newForward, newUp);
            }
        }

        public bool FollowOverlayCamera() {
            if(followOverlay) {
                return false;
            }


            followOverlay = true;
            UpdateCameraPositions();

            return true;
        }

        public bool FollowBaseCamera() {
            if(!followOverlay) {
                return false;
            }

            followOverlay = false;
            UpdateCameraPositions();

            return true;
        }
    }
}