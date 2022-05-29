using System;
using UnityEngine;
using StellarRemnants.Core;
using StellarRemnants.Inventory;



/*----------------------------------------
  TODO LIST:
- In LookRaycast functions, account for edge case where player is looking across the boundary of a portal volume.
----------------------------------------*/
namespace StellarRemnants.Units {
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerCharacter : CharacterUnit {

        /* Component References */
        [HideInInspector] public CapsuleCollider capsuleCollider;
        [HideInInspector] public SphereCollider sphereCollider;
        [HideInInspector] public Transform lookTransform;
        
        /* External References */
        public PlayerBase common;
        public PlayerConfig config; // Should this be moved to controller? // If it's moved to the controller, how do we get player-specific values?

        /* State Variables */
        public BaseMovementState movementState;
        public BaseActivityState activityState;

        //private Gun[] equippedWeapons;
        //private Tool[] equippedTools;

        /* Input Control Variables */
        public bool doCrouch;
        public bool doSprint;
        public bool doBrace;
        public bool doFocus;
        public bool doJumpThisFrame;
        public bool doUse;
        public bool doADS;
        public bool cancelFocus;

        /* Performance Variables - Calculate once per tick rather than calling a function each time */
        public bool OnSurface;
        public bool OnTrueGround;

        [HideInInspector] public float HorizontalSpeed;
        //[HideInInspector] public float TrueSpeed;
        [HideInInspector] public bool IsSwimming = false; // Temporary value. Eventually will be calculated based on whether player is in a water volume.
        [HideInInspector] public bool HasHeadClearance = true; // TODO: Implement calculation. If false, prevents jumping and uncrouching.
        [HideInInspector] public float frictionCoefficient;
        [HideInInspector] public Vector3 groundNormal;
        [HideInInspector] public float GravityJumpMultiplier = 1f;

        
        // ANIMATION TUTORIAL:
        // https://www.youtube.com/watch?v=vApG8aYD5aI
        // https://www.youtube.com/watch?v=_J8RPIaO2Lc




        // TODO: Sort these variables.
        //[HideInInspector] private Item swapTo; // Move to some sort of swap activity state?
        //[HideInInspector] private bool swapHeldItem;
        [HideInInspector] private bool animationLocked; // TODO: Handle animation in movement & activity states?
        [HideInInspector] private float lockDuration;

        [Header("Details")]
        public float health;
        //public Gun gun; // TODO: Move to activity state.
        public int armorClass;

        [Header("General Movement")]
        
        [SerializeField] [Range(0f, 1f)] [Tooltip("Speed multiplier based on the effects of gravity. Calculated when gravity is changed. Gravity levels further from OptimalGravity result in lower values.")]
        public float OptimalGravitySpeedMultiplier = 1f;

        [Header("Equipment")]
        [Range(-1,1)] [SerializeField] private int selectedWeapon;
        [Range(-1,3)] [SerializeField] private int selectedTool;

        //public Item heldItem; // TODO: MOve to activity state?
        public float zoomMultiplier = 1;




        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public PlayerCharacter() {
            //equippedWeapons = new Gun[2];
            //equippedWeapons[0] = new Gun("Test Pistol");
            //equippedWeapons[1] = new Gun("Test SMG", false);

            movementState = new StandState(this);
            activityState = new IdleState(this);
            
            //gun = equippedWeapons[0];
            //heldItem = equippedWeapons[0];
            health = 100f;
            armorClass = 1;
            animationLocked = false;
            OnTrueGround = false;
            config = new PlayerConfig();
        }


        /*----------------------------------------
        |   UNITY FUNCTIONS
        ----------------------------------------*/
        void Start() {
            init();
            capsuleCollider = GetComponent<CapsuleCollider>();
            sphereCollider = GetComponent<SphereCollider>();
            lookTransform = GameObject.Find("PlayerHead").transform; // TODO: Maybe create PlayerHead here?
        }

        public void Update() {
            movementState.Update();
            activityState.Update();
        }

        public new void FixedUpdate() {
            base.FixedUpdate();
            
            HorizontalSpeed = Vector3.ProjectOnPlane(rb.velocity, gravityNormal).magnitude;
            
            movementState.CheckStateEnd();
            movementState.FixedUpdate();
            activityState.CheckStateEnd();
            activityState.FixedUpdate();
            
            //updateHeldItem();

            if(animationLocked) { // TODO: Move animation to movement/activity states?
                lockDuration -= Time.deltaTime;

                if(lockDuration < 0) {
                    animationLocked = false;
                }
            }
            
            OnTrueGround = false;
        }

        public void OnCollisionEnter(Collision collision) {
            CheckTrueGround(collision);
            movementState.OnCollisionEnter(collision);
        }    



        /*----------------------------------------
        |   CONTROL FUNCTIONS
        ----------------------------------------*/
        // TODO: Many of these control functions (like SetCrouch) appear to not be used in SrPlayerController. Instead, that class is setting values directly. Something should be changed.
        public void SetMovement(Vector3 movement) {
            movementInput = movement;
        }

        public void SetLookDelta(Vector2 delta) {
            lookInput = delta;
        }

        public void SetSprint(bool s) {
            doSprint = s && movementState.CanSprint() && activityState.CanSprint();
        }

        public void SetCrouch(bool c) {
            doCrouch = c && movementState.CanCrouch() && activityState.CanCrouch();
        }

        public void SetJump() {
            if(movementState.CanJump() && activityState.CanJump()) {
                doJumpThisFrame = true;
            }
        }

        public void SetBrace(bool b) {
            doBrace = b && movementState.CanBrace() && activityState.CanBrace();
        }

        public bool SetGlance(Vector3 position, float duration) {
            return activityState.SetGlance(position, duration);
        }

        public void SetUseOrFocus(bool use) {
            if(!use) {
                doUse = false;
                doFocus = false;
                // TODO: Should cancelFocus be set here too?
                return;
            }

            if(activityState.CanUse()) { // TODO: Also account for movement state.
                doUse = true;
            }
            else if(activityState.CanFocus() && movementState.CanFocus()) {
                doFocus = true;
                cancelFocus = false;
            }
        }

        public void SetAimWeapon(bool w) {

        }

        public void SetAimTool(bool t) {

        }

        public void OptionPress(int selection) {
            activityState.OptionRelease(selection);
        }

        public void OptionRelease(int selection) {
            activityState.OptionRelease(selection);
        }

        public void PerformInteraction1() { activityState.PerformInteraction1(); }
        public void PerformInteraction2() { activityState.PerformInteraction2(); }
        public void PerformInteraction3() { activityState.PerformInteraction3(); }
        public void PerformInteraction4() { activityState.PerformInteraction4(); }

        public void PerformInteraction1Alt() { activityState.PerformInteraction1Alt(); }
        public void PerformInteraction2Alt() { activityState.PerformInteraction2Alt(); }
        public void PerformInteraction3Alt() { activityState.PerformInteraction3Alt(); }
        public void PerformInteraction4Alt() { activityState.PerformInteraction4Alt(); }


        /*----------------------------------------
        |   PRIVATE FUNCTIONS
        ----------------------------------------*/
        private void doAnimation(float duration) {
            // What about multiple animations performed sequentially, like swapping one weapon for another?
            this.animationLocked = true;
            this.lockDuration = duration;
        }

        

        private void updateBodyOrientation() {
            // If player is looking too far to the side, update body orientation to match.

        }

        private void orientBodyToForward() {
            // Called in updateBodyOrientation() and when player starts moving.
        }


        /*----------------------------------------
        |   STATE FUNCTIONS
        ----------------------------------------*/
        public void SetMovementState(BaseMovementState newMovementState) {
            movementState.OnStateExit();
            movementState = newMovementState;
            movementState.OnStateEnter();
        }

        public void SetMovementState(BaseMovementState newMovementState, string reason) {
            if(debugMode) {
                Debug.Log("Changing State: " + movementState.GetStateName() + " -> " + newMovementState.GetStateName() + " (" + reason + ")");
            }
            
            SetMovementState(newMovementState);
        }

        public void SetActivityState(BaseActivityState newActivityState) {
            activityState.OnStateExit();
            activityState = newActivityState;
            activityState.OnStateEnter();
        }

        public void SetActivityState(BaseActivityState newActivityState, string reason) {
            if(debugMode) {
                Debug.Log("Changing State: " + activityState.GetStateName() + " -> " + newActivityState.GetStateName() + " (" + reason + ")");
            }
            
            SetActivityState(newActivityState);
        }


        /*----------------------------------------
        |   MISC FUNCTIONS
        ----------------------------------------*/

        public void DoDamage(float amount) {
            Debug.Log("Damaged! " + amount);
        }

        /**
            This function should be called exclusively by the current movement state.
        **/
        public void ApplyGravity() {
            
            if(!OnTrueGround) {
                gravityField.applyGravity(this, transform.position);
                rb.AddForce(gravityNormal * gravity, ForceMode.Acceleration);
            }
        }

        public RaycastHit UpdateGroundedStatus() {
            RaycastHit hit;
            //OnSurface = Physics.SphereCast(transform.position + new Vector3(0, sphereCollider.radius+0.1f, 0), sphereCollider.radius - 0.1f, -gravityNormal, out hit, 1f);
            OnSurface = Physics.SphereCast(transform.position + gravityNormal * (sphereCollider.radius+0.1f), sphereCollider.radius - 0.1f, -gravityNormal, out hit, 1f);
            if(OnSurface) {
                frictionCoefficient = hit.collider.material.staticFriction;
                groundNormal = hit.normal;
            }
            else {
                frictionCoefficient = 0f;
                groundNormal = Vector3.zero;
            }
            
            return hit;
        }

        public void CheckTrueGround(Collision collision) {
            if(!OnTrueGround & !doJumpThisFrame) {
                ContactPoint contact = collision.contacts[0];
                if(contact.thisCollider == sphereCollider) { // TODO: Correct true ground check
                    OnTrueGround = true;
                }
                if(Vector3.Angle(gravityNormal, contact.point - contact.thisCollider.transform.position) > 135f) {
                    OnTrueGround = true;
                }
            }
        }

        public void ChangeGravityStrength(float gravity) {
            this.gravity = gravity;
            OptimalGravitySpeedMultiplier = Mathf.Clamp((Mathf.Abs(this.gravity) > Mathf.Abs(common.OptimalGravity)) ? common.OptimalGravity / this.gravity : this.gravity / common.OptimalGravity, common.OptimalGravitySpeedMultiplierMin, 1f);
            float ratio = Mathf.Clamp01(this.gravity / common.OptimalGravity);
            GravityJumpMultiplier = Mathf.Sqrt((-ratio+2) * ratio); // This field weakens jump strength in low gravity so that the player doesn't jump too high.
        }

        public bool MovementDirectionAllowsSprint() {
            return movementInput.z >= common.SprintMinimumAngle;
        }

        public bool LookRaycast(float range, out RaycastHit hit) {
            // TODO: Make this account being on the boundary of a portal volume.
            return Physics.Raycast(new Ray(lookTransform.position, lookTransform.forward), out hit, range, 1 << gameObject.layer, QueryTriggerInteraction.Ignore);
        }

        public bool LookRaycastToward(Vector3 direction, float range, out RaycastHit hit) {
            // TODO: Make this account being on the boundary of a portal volume.
            return Physics.Raycast(new Ray(lookTransform.position, direction), out hit, range, 1 << gameObject.layer, QueryTriggerInteraction.Ignore);
        }
    }
}