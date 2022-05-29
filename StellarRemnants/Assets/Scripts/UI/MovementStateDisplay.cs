using UnityEngine;
using UnityEngine.UI;
using StellarRemnants.Control;

namespace StellarRemnants.UI {
    public class MovementStateDisplay : MonoBehaviour {

        public Text text;

        public SrPlayerController controller;

        void Start() {
            
        }

        void Update() {
            //text.text = controller.state.ToString();
            text.text = controller.player.movementState.GetStateName() + " / " + controller.player.activityState.GetStateName() + " [" + controller.player.HorizontalSpeed.ToString("F1") + " : " + controller.player.TrueSpeed.ToString("F1") + "]";
        }
    }
}