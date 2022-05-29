using UnityEngine;
using StellarRemnants.Interact;

/*----------------------------------------
  TODO LIST:
- High Priority - Interaction options are not working.
- Unfocus node when node is too far above or below player. Account for arbitrary player rotation.
----------------------------------------*/


/*
Focus Behavior:
1. On start focus, find object nearest to cursor. Object being pointed at always has priority.
2. When focusing, look toward node. Soft-lock camera to node. 
3. If focused node is blocked, unfocus node
4. If node is focused and player moves cursor beyond soft-lock, find new node in that direction. If there is no closer node in given direction, soft-lock is still applied.

*/

namespace StellarRemnants.Units {
    public class FocusState : BaseActivityState {

        private bool completeFocus = false;
        private bool focusEventFired = false;
        private bool glance = false;
        private bool glanceReturn = false;
        private Vector3 target;
        private float glanceUntil;

        private bool findNext = false;
        private bool noNextNode = true;
        private Vector2 nextNodeDir;

        private float nextNodeMaxAngle = 15f;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public FocusState(PlayerCharacter p) : base(p) { }
        public FocusState(BaseActivityState previous) : base(previous) { }

        /*----------------------------------------
        |   EVENTS
        ----------------------------------------*/
        public override void OnStateEnter() {
            // If player has controller, enable focus controls
            player.controller.SetFocusControlMode(true);
            base.OnStateEnter();
        }

        public override void OnStateExit() {
            // If player has controller, disable focus controls.
            player.controller.SetFocusControlMode(false);
            base.OnStateExit();
        }


        /*----------------------------------------
        |   OVERRIDE FUNCTIONS
        ----------------------------------------*/
        public override string GetStateName() {
            return "focusing";
        }

        public override void FixedUpdate() {
            if(findNext) {
                findNextNode();
            }

            if(player.focusedInteractable != null) {
                if(completeFocus) {
                    if(glance) {
                        if(!glanceReturn && glanceUntil > Time.deltaTime) {
                            glanceReturn = true;
                            target = player.focusedInteractable.GetFocalPoint();
                        }

                        if(glanceReturn) {
                            // TODO: If angle is close enough, returnFromGlance = false;
                        }
                    }
                    else {
                        // TODO: Ensure line-of-sight is not blocked.
                    }
                }
                else {
                    if(Vector3.Angle(player.focusedInteractable.GetFocalPoint() - player.lookTransform.position, player.lookTransform.forward) < 5f) {
                        completeFocus = true;
                        player.focusedInteractable.OnFocus(player);
                    }
                }
            }
            else {
                findNearestNode();
            }
            

            if(player.focusedInteractable != null && player.lookInput != Vector2.zero) { // Replace this with (lookInput is outside of deadzone)
                // Check for other nearby node that is not current node, in direction of the input.
            }

            if(player.focusedInteractable != null) {
                //player.focusedObject.OnFocusUpdate(player); // This does nothing
                unfocusDistantNode();
            }
        }

        public override void Update() {
            if(completeFocus && !focusEventFired) {
                player.focusedInteractable.OnFocus(player);
                focusEventFired = true;
            }

            if(player.focusedInteractable != null) {
                faceCurrentNode();
            }
        }

        public override void CheckStateEnd() {
            if(CheckEnd() || CheckMovementInterruption() || CheckCancelled()) {
                return;
            }
        }

        public override bool SetGlance(Vector3 position, float duration) {
            target = position;
            glanceUntil = duration + Time.deltaTime;
            glance = true;
            glanceReturn = false;
            return true;
        }

        public override bool OptionPress(int option) {
            if(player.focusedInteractable == null) {
                return false;
            }
            else {
                switch(option) {
                    // case 1: return player.focusedObject.PerformInteract1(null); // These do nothing
                    // case 2: return player.focusedObject.PerformInteract2(null);
                    // case 3: return player.focusedObject.PerformInteract3(null);
                    // case 4: return player.focusedObject.PerformInteract4(null);
                    default: return false;
                }
            }


            
        }


        public override bool PerformInteraction1() { 
            if(player.focusedInteractable == null) { return false; }
            //player.focusedInteractable.GetInteraction1().
            return false;
        }

        public override bool PerformInteraction2() { 
            if(player.focusedInteractable == null) { return false; }
            return false; 
        }

        public override bool PerformInteraction3() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }

        public override bool PerformInteraction4() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }



        public override bool PerformInteraction1Alt() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }

        public override bool PerformInteraction2Alt() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }

        public override bool PerformInteraction3Alt() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }

        public override bool PerformInteraction4Alt() { 
            if(player.focusedInteractable == null) { return false; }
            return false;
        }
        
        
        /*----------------------------------------
        |   END-STATE FUNCTIONS
        ----------------------------------------*/
        private bool CheckEnd() {
            if(!player.doFocus) {
                unfocusNode();
                player.SetActivityState(new IdleState(player), "Player stopped focusing");
                return true;
            }

            return false;
        }

        private bool CheckMovementInterruption() {
            if(!player.movementState.CanFocus()) {
                player.SetActivityState(new IdleState(player), "Player focus interrupted by movement state");
                player.cancelFocus = true;
                return true;
            }
            return false;
        }

        private bool CheckCancelled() {
            if(player.cancelFocus) {
                player.SetActivityState(new IdleState(player), "Player focus was cancelled");
                return true;
            }
            return false;
        }

        /*----------------------------------------
        |   ACTION GOVERNOR FUNCTIONS
        ----------------------------------------*/
        public override bool CanFocus() {
            return true; // Maybe, though I'm not sure it's needed at all since this is only checked before entering the Focus state, which can't happen.
        }

        public override bool CanLookAround() {
            return player.focusedInteractable == null;
        }

        public override bool CanSprint() {
            return player.focusedInteractable == null;
        }

        public override bool CanJump() {
            return true;
        }

        

        /*----------------------------------------
        |   FRAME UPDATE PRIVATE FUNCTIONS
        ----------------------------------------*/
        /* Update logic to make player face focused node and set "next node" state when applicable. */
        private void faceCurrentNode() {
            if(glance) {
                setLookRotation(player.config.focusChangeLookSpeed);
                // TODO: Do more sophisticated glance animation curve here (instead of the slerp above).
            }
            else if(completeFocus && !findNext && player.lookInput.sqrMagnitude > player.config.focusNextNodeInputThreshold) {
                findNext = true;
                nextNodeDir = player.lookInput;
                noNextNode = false;
            }
            else {
                setLookRotation(player.config.focusStartLookSpeed);
            }
        }

        /* Update logic to make player turn to face focused node at specified speed. */
        private void setLookRotation(float lookSpeed) {
            Vector3 directionToTarget = player.focusedInteractable.GetFocalPoint() - player.lookTransform.position; // Not normalized, but doesn't need to be.
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget, player.transform.up);
            player.lookTransform.rotation = Quaternion.Slerp(player.lookTransform.rotation, rotationToTarget, Time.deltaTime * lookSpeed);
        }
        

        /*----------------------------------------
        |   FIXED UPDATE PRIVATE FUNCTIONS
        ----------------------------------------*/

        private void findNearestNode() {
            ObjectInteractable node;
            node = getLookedAtNode();
            if(node == null) {
                node = getClosestNode();
            }
            
            setCurrentNode(node);
            
        }

        private void findNextNode() { // TODO: When selecting next node, prioritize those that are closer to the initial node (in 2D space).
            Collider[] colliders = getNearbyColliders();
            ObjectInteractable selectedNode = null;
            float lowestProximityFactor = 1000;

            Vector3 nextObjectDir = player.lookTransform.rotation * new Vector3(nextNodeDir.x, nextNodeDir.y, 0);

            foreach(Collider collider in colliders) {
                if(collider.TryGetComponent<ObjectInteractable>(out ObjectInteractable node) && node.IsFocusable() && node != player.focusedInteractable) {
                    Vector3 nodeDifference = node.transform.position - player.lookTransform.position;
                    float angleToNode = Vector3.Angle(nodeDifference, player.lookTransform.forward);
                    if(angleToNode > 45) {
                        continue;
                    }
                    
                    Vector3 flattenedPos = Vector3.ProjectOnPlane(nodeDifference, player.lookTransform.forward);
                    float angleToNode2d = Vector3.Angle(nextObjectDir, flattenedPos);

                    if(angleToNode2d < 30f && angleToNode2d < lowestProximityFactor) {
                        lowestProximityFactor = angleToNode2d;
                        selectedNode = node;
                    }
                }
            }
            
            if(selectedNode != null) {
                setCurrentNode(selectedNode);
            }
            else {
                noNextNode = true;
                findNext = false;
            }
        }

        private void unfocusDistantNode() {
            ObjectInteractable node = player.focusedInteractable;
            Vector3 nodeDifference = node.transform.position - player.lookTransform.position;

            if(nodeDifference.sqrMagnitude > 3.5f*3.5f) {
                player.cancelFocus = true;
                unfocusNode();
            }
            else if(Vector3.Angle(-nodeDifference, node.transform.forward) > node.MaxFocusAngle) {
                unfocusNode();
                player.cancelFocus = true;
            }
        }

        private ObjectInteractable getLookedAtNode() {
            if(player.LookRaycast(player.common.FocusOnNodeRange, out RaycastHit hit) && hit.collider.TryGetComponent<ObjectInteractable>(out ObjectInteractable interactable)) {
                return interactable;
            }

            return null;
        }


        private Collider[] getNearbyColliders() {
            // TODO: Why did I use player.rb.position instead of player.lookTransform.position?
            // TODO: Why was a capsule overlap used instead of sphere?
            return Physics.OverlapCapsule(player.rb.position, new Vector3(player.rb.position.x, player.rb.position.y+3, player.rb.position.z), player.common.FocusOnNodeRange, 1<<player.gameObject.layer);
        }

        private ObjectInteractable getClosestNode() {
            Collider[] colliders = getNearbyColliders();
                
            Vector3 lookDir = player.lookTransform.forward;
            ObjectInteractable selectedNode = null;
            float lowestProximityFactor = 100;

            foreach(Collider collider in colliders) {
                if(collider.TryGetComponent<ObjectInteractable>(out ObjectInteractable node) && node.IsFocusable()) {
                    Vector3 nodeDifference = node.transform.position - player.lookTransform.position;
                    float angleToNode = Vector3.Angle(nodeDifference, lookDir);
                    if(angleToNode > 45) {
                        continue;
                    }

                    float angleToPlayer = Vector3.Angle(-nodeDifference, node.transform.forward);
                    if(angleToPlayer > node.MaxFocusAngle) {
                        continue;
                    }

                    float proximityFactor = (angleToNode * nodeDifference.sqrMagnitude / 405); // 45 * 3^2 = 405

                    if(proximityFactor < lowestProximityFactor) {
                        selectedNode = node;
                        lowestProximityFactor = proximityFactor;
                    }
                }
            }

            return selectedNode;
        }

        private void setCurrentNode(ObjectInteractable node) {
            player.SetFocusedInteractable(node);
            if(node != null) {
                target = node.GetFocalPoint();
            }
            
            findNext = false;
            nextNodeDir = player.lookInput;
            completeFocus = false;
            // TODO: Trigger glow effect on object.
        }

        private void unfocusNode() {
            player.SetFocusedInteractable(null);
            findNext = false;
            completeFocus = false;
        }

        /*----------------------------------------
        |   INACTIVE CODE
        ----------------------------------------*/
        
        // private void EachFocusableNode(Action<InteractableObject> action) {
        //     Collider[] colliders  = Physics.OverlapCapsule(player.rb.position, new Vector3(player.rb.position.x, player.rb.position.y+3, player.rb.position.z), player.common.FocusOnNodeRange, 1<<player.gameObject.layer);
        
        //     foreach(Collider collider in colliders) {
        //         if(collider.TryGetComponent<InteractableObject>(out InteractableObject node) && node.IsFocusable()) {
        //             action(node);
        //         }
        //     }
        // }
    }
}