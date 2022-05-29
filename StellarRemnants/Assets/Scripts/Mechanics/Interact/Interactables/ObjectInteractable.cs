using System;
using System.Collections.Generic;
using UnityEngine;
using StellarRemnants.Units;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StellarRemnants.Interact {
    public abstract class ObjectInteractable : MonoBehaviour, Interactable, Damageable {

        /*----------------------------------------
        |   STATIC VARIABLES
        ----------------------------------------*/
        private static readonly ObjectState<ObjectInteractable> STATE_NONE = new ObjectState<ObjectInteractable>("Sta_Generic_None");
        private static readonly ObjectState<ObjectInteractable> STATE_DESTROYED = new ObjectState<ObjectInteractable>("Sta_Generic_Destroyed");


        /*----------------------------------------
        |   DATA MEMBERS
        ----------------------------------------*/
        public ObjectState State = STATE_NONE;
        public float Health{get; set;}


        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        private event Action<Interactable, StateChange> internalStateChangeEvent = delegate { };

        
        /*----------------------------------------
        |   EVENT LISTENER METHODS
        ----------------------------------------*/
        public virtual void OnTriggerEnter(Collider other) {
            Interaction interaction = State.GetInteraction5();
            if(interaction != null && other.TryGetComponent<CharacterUnit>(out CharacterUnit unit)) {
                interaction.PerformInteraction(this, unit.GetAccessCredentials());
            }
        }

        
        /*----------------------------------------
        |   PUBLIC MEMBER METHODS
        ----------------------------------------*/
        public void SetState(ObjectState newState, StateChange changeType) {
            this.State = newState;
            InvokeStateChange(changeType);
        }


        /*----------------------------------------
        |   IMPLEMENTATIONS - Interactable
        ----------------------------------------*/
        public Interaction GetInteraction1() { return State.GetInteraction1(); }
        public Interaction GetInteraction2() { return State.GetInteraction2(); }
        public Interaction GetInteraction3() { return State.GetInteraction3(); }
        public Interaction GetInteraction4() { return State.GetInteraction4(); }
        public Interaction GetInteraction5() { return State.GetInteraction5(); }

        public Interaction GetAltInteraction1() { return State.GetAltInteraction1(); }
        public Interaction GetAltInteraction2() { return State.GetAltInteraction2(); }
        public Interaction GetAltInteraction3() { return State.GetAltInteraction3(); }
        public Interaction GetAltInteraction4() { return State.GetAltInteraction4(); }
        
        public void AddStateListener(Action<Interactable, StateChange> function) { internalStateChangeEvent += function; }
        public void RemoveStateListener(Action<Interactable, StateChange> function) { internalStateChangeEvent -= function; }
        public void InvokeStateChange(StateChange type) { internalStateChangeEvent.Invoke(this, type); }

        public virtual Vector3 GetFocalPoint() { return transform.position; } // TODO: Remove
        public virtual bool IsFocusable() { return true; }
        public virtual void OnFocus(CharacterUnit unit) {}
        public virtual Transform FocalPoint {get{return transform;}}

        public abstract float MaxFocusAngle {get;}
        public abstract float MenuOffset {get;}
        public abstract string LocalizationKey {get;}


        public InteractionField[] BuildFieldList(Credentials credential) {
            List<InteractionField> list = new List<InteractionField>();
            if(!LocalizationKey.Equals("")) {list.Add(new InteractionField(InteractionFieldType.TITLE, 0, LocalizationKey, true));}
            if(!State.LocalizationKey.Equals("")) {list.Add(new InteractionField(InteractionFieldType.STATE, 0, State.LocalizationKey, true));}

            // TODO: Update to interactions v3
            // GetInteraction1()?.AppendInteractionFields(list, InteractionFieldType.OPTION_PRESS, this, 1, credential);
            // GetAltInteraction1()?.AppendInteractionFields(list, InteractionFieldType.OPTION_HOLD, this, 1, credential);
            // GetInteraction2()?.AppendInteractionFields(list, InteractionFieldType.OPTION_PRESS, this, 2, credential);
            // GetAltInteraction2()?.AppendInteractionFields(list, InteractionFieldType.OPTION_HOLD, this, 2, credential);
            // GetInteraction3()?.AppendInteractionFields(list, InteractionFieldType.OPTION_PRESS, this, 3, credential);
            // GetAltInteraction3()?.AppendInteractionFields(list, InteractionFieldType.OPTION_HOLD, this, 3, credential);
            // GetInteraction4()?.AppendInteractionFields(list, InteractionFieldType.OPTION_PRESS, this, 4, credential);
            // GetAltInteraction4()?.AppendInteractionFields(list, InteractionFieldType.OPTION_HOLD, this, 4, credential);
            // GetInteraction5()?.AppendInteractionFields(list, InteractionFieldType.OPTION_FORWARD, this, 5, credential);

            return list.ToArray();
        }


        /*----------------------------------------
        |   IMPLEMENTATIONS - Damageable
        ----------------------------------------*/
        public virtual float GetMaxHealth() {return 1f;}

        public virtual void ReceiveDamage(float amount, Damage type) {
            // float realAmount = amount * type.InorganicMultiplier;
            // if(Health > realAmount) {
            //     Health -= realAmount;
            //     OnDamage(realAmount / GetMaxHealth());
            // }
            // else {
            //     Health = 0f;
            //     OnDeath();
            // }
        }

        public virtual void OnDamage(float percentOfTotal) {
            // Can be overidden so modules can be damaged.
        }

        public virtual void OnDeath() {
            Destroy(this);
        }



        /*----------------------------------------
        |   EDITOR FUNCTIONS
        ----------------------------------------*/
        #if UNITY_EDITOR
        private GUIStyle style = new GUIStyle();
        public void OnDrawGizmosSelected() {

            style.normal.textColor = Color.cyan;
            style.fontSize = 15;
            style.alignment = TextAnchor.MiddleLeft;
            

            Quaternion arcRotation = Quaternion.Euler(0, -MaxFocusAngle, 0);
            
            

            if(State != null) {
                Handles.color = Color.green;
                Handles.Label(transform.position, State.LocalizationKey, style);
                Handles.DrawWireArc(transform.position, transform.up, arcRotation * transform.forward, MaxFocusAngle*2, MenuOffset);
            }
            

            Handles.color = Color.red;
            Handles.DrawWireArc(transform.position, transform.up, arcRotation * transform.forward, -(360f - MaxFocusAngle*2f), MenuOffset);
            
            Gizmos.color = Color.green;
            if(FocalPoint != null && FocalPoint != this.transform) {
                Gizmos.DrawWireSphere(FocalPoint.position, 0.25f);
            }
            
            
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * MenuOffset);

            
        }
        #endif
    }
}