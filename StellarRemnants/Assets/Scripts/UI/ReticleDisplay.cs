using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using StellarRemnants.Units;

namespace StellarRemnants.UI {
    public class ReticleDisplay : MonoBehaviour {
        public Image image;
        public PlayerCharacter player;
        
        void Start() {
            
        }

        void Update() { // TODO: Update reticle only when it is changed; not every frame.
            if(player.doFocus && player.activityState.GetType() == typeof(FocusState) && player.focusedInteractable != null && !player.cancelFocus) {
                // Don't display
                image.enabled = false;
            }
            else {
                // Display
                image.enabled = true;
            }
        }
    }
}