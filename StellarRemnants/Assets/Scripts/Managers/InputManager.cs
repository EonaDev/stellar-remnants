using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

[Serializable] public class MoveInputEvent : UnityEvent<Vector2> { }
[Serializable] public class LookInputEvent : UnityEvent<Vector2> { }
[Serializable] public class JumpInputEvent : UnityEvent<float> { }
[Serializable] public class SprintInputEvent : UnityEvent<float> { }
[Serializable] public class SprintCancelEvent : UnityEvent<float> { }
[Serializable] public class ToggleEvaEvent : UnityEvent<float> { }
[Serializable] public class ChangeAxisEvent : UnityEvent<float> { }
[Serializable] public class ChangeAxisCancelEvent : UnityEvent<float> { }
[Serializable] public class CrouchInputEvent : UnityEvent<float> {}
[Serializable] public class CrouchCancelEvent : UnityEvent<float> {}
[Serializable] public class UseInputEvent : UnityEvent<float> {}
[Serializable] public class AimDownSightsEvent : UnityEvent<float> {}
[Serializable] public class AimDownSightsCancelEvent : UnityEvent<float> {}
[Serializable] public class ReloadEvent : UnityEvent<float> {}
[Serializable] public class BraceInputEvent : UnityEvent<float> {}
[Serializable] public class BraceCancelEvent : UnityEvent<float> {}
[Serializable] public class FocusInputEvent : UnityEvent<float> {}
[Serializable] public class FocusCancelEvent : UnityEvent<float> {}
[Serializable] public class PutAwayInputEvent : UnityEvent<float> {}
[Serializable] public class Option1StartEvent : UnityEvent<int> {}
[Serializable] public class Option2StartEvent : UnityEvent<int> {}
[Serializable] public class Option3StartEvent : UnityEvent<int> {}
[Serializable] public class Option4StartEvent : UnityEvent<int> {}

[Serializable] public class Option1PerformEvent : UnityEvent<int> {}
[Serializable] public class Option2PerformEvent : UnityEvent<int> {}
[Serializable] public class Option3PerformEvent : UnityEvent<int> {}
[Serializable] public class Option4PerformEvent : UnityEvent<int> {}


namespace StellarRemnants.Control {
    public class InputManager : MonoBehaviour {
        public PlayerActionControls controls;
        private static InputManager _instance;

        public MoveInputEvent moveInputEvent;
        public LookInputEvent lookInputEvent;
        public JumpInputEvent jumpInputEvent;
        public SprintInputEvent sprintInputEvent;
        public SprintCancelEvent sprintCancelEvent;
        public ToggleEvaEvent toggleEvaEvent;
        public ChangeAxisEvent changeAxisEvent;
        public ChangeAxisCancelEvent changeAxisCancelEvent;
        public CrouchInputEvent crouchInputEvent;
        public CrouchCancelEvent crouchCancelEvent;
        public BraceInputEvent braceInputEvent;
        public BraceCancelEvent braceCancelEvent;
        public UseInputEvent useInputEvent;
        public AimDownSightsEvent aimDownSightsEvent;
        public AimDownSightsCancelEvent aimDownSightsCancelEvent;
        public ReloadEvent reloadEvent;
        public FocusInputEvent useOrFocusInputEvent;
        public FocusCancelEvent useOrFocusCancelEvent;
        public PutAwayInputEvent putAwayInputEvent;
        public Option1StartEvent option1PressEvent;
        public Option2StartEvent option2PressEvent;
        public Option3StartEvent option3PressEvent;
        public Option4StartEvent option4PressEvent;

        public Option1PerformEvent option1CancelEvent;
        public Option2PerformEvent option2CancelEvent;
        public Option3PerformEvent option3CancelEvent;
        public Option4PerformEvent option4CancelEvent;

        public static InputManager Instance {
            get {
                return _instance;
            }
        }

        private void Awake() {
            if(_instance != null && _instance != this) {
                Destroy(this.gameObject);
            }
            else {
                _instance = this;
                
            }
            #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
            #endif
            controls = new PlayerActionControls();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //controls.bindingMask = new InputBinding {groups = "Switch Pro"};
        }

        private void OnEnable() {
            controls.Enable();
            controls.PlayerOnFoot.Disable();
            controls.OnFootFocus.Disable();
            //controls.OnFootFocus.Enable();
            //InputControlScheme[] controlOptions = controls.controlSchemes;
            controls.OnFootBase.Move.performed += OnMovePerform;
            controls.OnFootBase.Move.canceled += OnMovePerform;
            controls.OnFootBase.Look.performed += OnLookPerform;
            controls.OnFootBase.Jump.performed += OnJumpPerform;
            controls.OnFootBase.Sprint.started += OnSprintStart;
            controls.OnFootBase.Sprint.canceled += OnSprintCancel;
            //controls.PlayerOnFoot.ToggleEVA.performed += OnToggleEva;
            //controls.PlayerOnFoot.ChangeAxis.performed += OnChangeAxis;
            //controls.PlayerOnFoot.ChangeAxis.canceled += OnChangeAxisCancel;
            controls.OnFootBase.Crouch.started += OnCrouchStart;
            controls.OnFootBase.Crouch.canceled += OnCrouchCancel;
            controls.OnFootBase.AimWeapon.performed += OnAimDownSightsPerform;
            controls.OnFootBase.AimWeapon.canceled += OnAimDownSightsCancel;
            controls.OnFootBase.UseFocus.performed += OnUseOrFocusPerform;
            controls.OnFootBase.UseFocus.canceled += OnUseOrFocusCancel;

            //controls.OnFootADS.UseFire.performed += OnUsePerform;
            controls.OnFootADS.ItemInteraction2Reload.performed += OnReloadPerform;

            //controls.OnFootNotADS.Focus.performed += OnFocusPerform;
            //controls.OnFootNotADS.Focus.canceled += OnFocusCancel;
            controls.OnFootNotADS.ItemInteraction2Reload.performed += OnReloadPerform;
            controls.OnFootNotADS.Melee.performed += OnBracePerform; // TODO: Replace with melee
            controls.OnFootNotADS.Melee.canceled += OnBraceCancel; // TODO: Replace with melee
            controls.OnFootNotADS.PutAway.performed += OnPutAwayPerform;

            controls.OnFootFocus.ObjectInteraction1Basic.started += OnSelectOption1Start;
            controls.OnFootFocus.ObjectInteraction2Toggle.started += OnSelectOption2Start;
            controls.OnFootFocus.ObjectInteraction3Strike.started += OnSelectOption3Start;
            controls.OnFootFocus.ObjectInteraction4Terminal.started += OnSelectOption4Start;

            controls.OnFootFocus.ObjectInteraction1Basic.performed += OnSelectOption1Perform;
            controls.OnFootFocus.ObjectInteraction2Toggle.performed += OnSelectOption2Perform;
            controls.OnFootFocus.ObjectInteraction3Strike.performed += OnSelectOption3Perform;
            controls.OnFootFocus.ObjectInteraction4Terminal.performed += OnSelectOption4Perform;
            //controls.OnFootFocus.Focus.canceled += OnFocusCancel;
            
            controls.OnFootADS.Disable();
        }

        public void SetFocusMode() {
            controls.OnFootNotADS.Disable();
            controls.OnFootFocus.Enable();
        }

        private void OnDisable() {
            controls.Disable();
        }
        

        private void OnMovePerform(InputAction.CallbackContext context) {
            moveInputEvent.Invoke(context.ReadValue<Vector2>());
        }

        private void OnLookPerform(InputAction.CallbackContext context) {
            lookInputEvent.Invoke(context.ReadValue<Vector2>());
        }

        private void OnJumpPerform(InputAction.CallbackContext context) {
            jumpInputEvent.Invoke(context.ReadValue<float>());
        }

        private void OnCrouchStart(InputAction.CallbackContext context) {
            crouchInputEvent.Invoke(context.ReadValue<float>());
        }

        private void OnCrouchCancel(InputAction.CallbackContext context) {
            crouchCancelEvent.Invoke(context.ReadValue<float>());
        }

        private void OnSprintStart(InputAction.CallbackContext context) {
            sprintInputEvent.Invoke(context.ReadValue<float>());
        }

        private void OnSprintCancel(InputAction.CallbackContext context) {
            sprintCancelEvent.Invoke(context.ReadValue<float>());
        }


        private void OnToggleEva(InputAction.CallbackContext context) {
            toggleEvaEvent.Invoke(context.ReadValue<float>());
        }


        private void OnChangeAxis(InputAction.CallbackContext context) {
            changeAxisEvent.Invoke(context.ReadValue<float>());
        }

        private void OnChangeAxisCancel(InputAction.CallbackContext context) {
            changeAxisCancelEvent.Invoke(context.ReadValue<float>());
        }

        private void OnUsePerform(InputAction.CallbackContext context) {
            useInputEvent.Invoke(context.ReadValue<float>());
        }

        private void OnAimDownSightsPerform(InputAction.CallbackContext context) {
            aimDownSightsEvent.Invoke(context.ReadValue<float>());
        }

        private void OnAimDownSightsCancel(InputAction.CallbackContext context) {
            aimDownSightsCancelEvent.Invoke(context.ReadValue<float>());
        }

        public void OnReloadPerform(InputAction.CallbackContext context) {
            reloadEvent.Invoke(context.ReadValue<float>());
        }

        public void OnBracePerform(InputAction.CallbackContext context) {
            braceInputEvent.Invoke(context.ReadValue<float>());
        }

        public void OnBraceCancel(InputAction.CallbackContext context) {
            braceCancelEvent.Invoke(context.ReadValue<float>());
        }

        public void OnUseOrFocusPerform(InputAction.CallbackContext context) {
            useOrFocusInputEvent.Invoke(context.ReadValue<float>());
            //controls.OnFootNotADS.Disable();
            //controls.OnFootFocus.Enable();
        }

        public void OnUseOrFocusCancel(InputAction.CallbackContext context) {
            if(context.canceled) {
                useOrFocusCancelEvent.Invoke(context.ReadValue<float>());
            }
        }

        public void OnPutAwayPerform(InputAction.CallbackContext context) {
            putAwayInputEvent.Invoke(context.ReadValue<float>());
        }

        public void OnSelectOption1Start(InputAction.CallbackContext context) {
            Debug.Log("Start 1");
            option1PressEvent.Invoke(1);
        }

        public void OnSelectOption2Start(InputAction.CallbackContext context) {
            Debug.Log("Start 2");
            option2PressEvent.Invoke(2);
        }

        public void OnSelectOption3Start(InputAction.CallbackContext context) {
            Debug.Log("Start 3");
            option3PressEvent.Invoke(3);
        }

        public void OnSelectOption4Start(InputAction.CallbackContext context) {
            Debug.Log("Start 3");
            option4PressEvent.Invoke(4);
        }

        

        public void OnSelectOption1Perform(InputAction.CallbackContext context) {
            Debug.Log("Perform 1");
            option1CancelEvent.Invoke(1);
        }

        public void OnSelectOption2Perform(InputAction.CallbackContext context) {
            Debug.Log("Perform 2");
            option2CancelEvent.Invoke(2);
        }

        public void OnSelectOption3Perform(InputAction.CallbackContext context) {
            Debug.Log("Perform 3");
            option3CancelEvent.Invoke(3);
        }

        public void OnSelectOption4Perform(InputAction.CallbackContext context) {
            Debug.Log("Perform 4");
            option4CancelEvent.Invoke(4);
        }
    }
}