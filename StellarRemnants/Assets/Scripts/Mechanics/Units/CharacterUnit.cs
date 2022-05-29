using System;
using UnityEngine;
using StellarRemnants.Inventory;
using StellarRemnants.Interact;
using StellarRemnants.Control;

namespace StellarRemnants.Units {
    public abstract class CharacterUnit : MonoBehaviour, IInventory, IGravitatable {
        /* Component References */
        [HideInInspector] public Rigidbody rb;

        /* External References */
        public UnitController controller;

        /* Input Control Variables */
        public Vector3 movementInput;
        public Vector2 lookInput;
        public bool doMovement;

        
        //public event Action<InteractableObject> focusNodeEvent = delegate { }; // TODO: Remove
        public event Action<ObjectInteractable> focusInteractableEvent = delegate { };
        //public InteractableObject focusedObject; // TODO: Remove
        public ObjectInteractable focusedInteractable;

        /* Performance Variables - Calculate once per tick rather than calling a function each time */
        [HideInInspector] public float TrueSpeed;
        [HideInInspector] public bool HasVelocity;





        
        public Vector3Int ChunkPosition {get;set;}

        public GravityField gravityField;

        [SerializeField] [Tooltip("The normal vector of gravity; points away from the source of gravity.")]
        public Vector3 gravityNormal = Vector3.up;

        [SerializeField] [Range(-100f, 0f)] [Tooltip("[m/s^2] Strength of gravity being applied to player.")]
        public float gravity = -9.81f;


        [Header("Debug")]
        [SerializeField] public Vector3 impulse;
        [SerializeField] public bool debugMode = false;


                
        public void SetFocusedInteractable(ObjectInteractable obj) {
            focusedInteractable = obj;
            focusInteractableEvent.Invoke(obj);
        }


        public void init() {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.useGravity = false;
            //controller = GetComponent<UnitController>();
        }

        public void FixedUpdate() {
            if(!impulse.Equals(Vector3.zero)) {
                rb.AddForce(impulse, ForceMode.VelocityChange);
                impulse = Vector3.zero;
            }
        
            CorrectVerticalOrientation();

            TrueSpeed = rb.velocity.magnitude;
            doMovement = !movementInput.Equals(Vector3.zero);
            HasVelocity = rb.velocity.sqrMagnitude > 0.01f;


        }


        /*----------------------------------------
        |   MISC FUNCTIONS
        ----------------------------------------*/

        private void CorrectVerticalOrientation() {
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward, gravityNormal), 0.1f);
            Quaternion targetOrientation = Quaternion.LookRotation(Vector3.Cross(transform.right, gravityNormal), gravityNormal);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, Time.deltaTime *4);
        }

        public void MoveOrigin(Vector3Int offset) {
            ChunkPosition += offset;
        }

        public void setGravity(Vector3 gravityNormal, float gravityStrength) {
            this.gravityNormal = gravityNormal;
            this.gravity = gravityStrength;
        }

        public void resetGravity() {
            this.gravity = 0f;
        }

        public void setGravityField(GravityField gravityField) {
            this.gravityField = gravityField;
        }

        public Credentials GetAccessCredentials() {
            return null; // TODO: Implement
        }
    }
}