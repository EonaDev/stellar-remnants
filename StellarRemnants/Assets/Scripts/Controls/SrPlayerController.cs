using UnityEngine;
using StellarRemnants.Units;

/*----------------------------------------
  TODO LIST:
- 
----------------------------------------*/

//https://www.reddit.com/r/gamedev/comments/qxg3dr/ive_made_a_ton_of_modular_scifi_guns_you_can_use/

namespace StellarRemnants.Control {


    [RequireComponent(typeof(PlayerCharacter))]
    public class SrPlayerController : UnitController {
        [HideInInspector] public PlayerCharacter player;

        PlayerActionControls controls;


        // TODO: Variables below should be considered "game config"
        float softMaxVerticalAngle = 80f;
        float hardMaxVerticalAngle = 85f;
        float correctionRate = 200f;
        
        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Awake() {
            player = GetComponent<PlayerCharacter>();
        }

        void Start() {
            controls = GameObject.Find("GameManager").GetComponent<InputManager>().controls;
        }

        void Update() {
            UpdateCamera();
        }

        public void OnStateChange(BaseActivityState startingState, BaseActivityState endState) {
            // TODO: Enable/disable specific controls here.
            //this.



        }


        /*----------------------------------------
        |   INPUT EVENT FUNCTIONS
        ----------------------------------------*/
        public override void OnMove(Vector2 inputDirection) {
            player.SetMovement(new Vector3(inputDirection.x, 0, inputDirection.y));
        }

        public void OnLook(Vector2 delta) {
            player.SetLookDelta(delta);
        }

        public void OnJump(float value) {
            player.SetJump();
        }

        public void OnSprintPress(float value) {
            player.SetSprint(true);
        }

        public void OnSprintRelease(float value) {
            player.SetSprint(false);
        }

        public void OnCrouchPress(float value) {
            if(player.config.toggleCrouch) { 
                player.SetCrouch(!player.doCrouch);
            }
            else { 
                player.SetCrouch(true);
            }
        }

        public void OnCrouchRelease(float value) {
            if(!player.config.toggleCrouch) { player.SetCrouch(false); }
        }

        public void OnBracePress(float value) {
            player.SetBrace(true);
        }

        public void OnBraceRelease(float value) {
            player.SetBrace(false);
        }

        public void OnUseOrFocusPress(float value) {
            player.SetUseOrFocus(true);
        }

        public void OnUseOrFocusRelease(float value) {
            player.SetUseOrFocus(false);
            // player.doUse = false;
            // player.doFocus = false;
        }

        public void OnOptionPress(int selection) {
            player.OptionPress(selection);
        }

        public void OnOptionCancel(int selection) {
            player.OptionRelease(selection);
        }

        public void OnInteraction1Start() { player.PerformInteraction1(); }
        public void OnInteraction2Start() { player.PerformInteraction2(); }
        public void OnInteraction3Start() { player.PerformInteraction3(); }
        public void OnInteraction4Start() { player.PerformInteraction4(); }

        public void OnInteraction1Perform() { player.PerformInteraction1Alt(); }
        public void OnInteraction2Perform() { player.PerformInteraction2Alt(); }
        public void OnInteraction3Perform() { player.PerformInteraction3Alt(); }
        public void OnInteraction4Perform() { player.PerformInteraction1Alt(); }


        public void OnOptionHold(int selection, float duration) {
            // TODO: Implement this in Interactions. Might need to add a new type that can account for duration of hold.
            //player.
        }


        /*----------------------------------------
        |   CONTROL MODE FUNCTIONS
        ----------------------------------------*/
        public override void SetFocusControlMode(bool enable) {
            Debug.Log("setting focus controls: " + enable);
            if(enable) {
                controls.OnFootFocus.Enable();
            }
            else {
                controls.OnFootFocus.Disable();
            }
        }

        public override void SetIdleControlMode(bool enable) {
            if(enable) {
                controls.OnFootNotADS.Enable();
                controls.OnFootNotHolding.Enable();
            }
            else {
                controls.OnFootNotADS.Disable();
                controls.OnFootNotHolding.Disable();
            }
        }

        public override void SetOperatorControlMode(bool enable) {
            if(enable) {
                controls.OnFootBase.Disable();
                controls.VehicleBase.Enable();
            }
            else {
                controls.VehicleBase.Disable();
                controls.OnFootBase.Enable();
            }
        }

        /*----------------------------------------
        |   PRIVATE FUNCTIONS
        ----------------------------------------*/
        private void UpdateCamera() {

            float hInput;
            float vInput;

            if(player.activityState.CanLookAround()) {
                hInput = player.lookInput.x * player.config.horizontalLookSensitivity * Time.deltaTime * player.zoomMultiplier;
                vInput = player.lookInput.y * player.config.verticalLookSensitivity * Time.deltaTime * player.zoomMultiplier;
            }
            else {
                hInput = 0;
                vInput = 0;
            }

            Vector3 eulerAngles = player.lookTransform.localRotation.eulerAngles;

            float vAngle = eulerAngles.x - vInput;//SoftClamp(eulerAngles.x - vInput, softMaxVerticalAngle, hardMaxVerticalAngle); // TODO: Fix soft clamp
            float hAngle = eulerAngles.y + hInput;

            vAngle = SoftClamp(vAngle, softMaxVerticalAngle, hardMaxVerticalAngle);

            player.lookTransform.localRotation = Quaternion.Euler(vAngle, hAngle, 0); // TODO: Put look rotation somewhere? Here or on player?
            //Debug.Log("Rotation: " + player.lookTransform.forward.ToString("0.0000"));
            //player.transform.localRotation *= Quaternion.Euler(0f, hInput, 0f);

            // player.lookTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0, 0); // TODO: Put look rotation somewhere? Here or on player?
            // player.transform.localRotation *= Quaternion.Euler(0f, hInput, 0f);

        //     if(!zoomCompleted) {
        //         if(zoomDuration > 0) {
        //             vcam.m_Lens.FieldOfView = Mathf.Lerp(zoomStartFOV, zoomEndFOV, zoomTimeElapsed / zoomDuration);
        //             zoomTimeElapsed += Time.deltaTime;

        //             if(zoomTimeElapsed > zoomDuration) {
        //                 vcam.m_Lens.FieldOfView = zoomEndFOV;
        //                 zoomCompleted = true;
        //             }
        //         }
        //         else {
        //             vcam.m_Lens.FieldOfView = zoomEndFOV;
        //             zoomCompleted = true;
        //         }
        //     }
        }


        private float SoftClamp(float value, float soft, float hard) {
            bool invert = false;
            if(value > 180) {
                invert = true;
                value = 360f - value;
            }

            if(value > soft) {
                if(value > hard) {
                    value = hard;
                }
                float percentIn = (value - soft) / (hard - soft);
                
                value -= correctionRate * percentIn * Time.deltaTime;
            }

            if(invert) {
                value = 360f-value;
            }
            return value;
        }
    }
}