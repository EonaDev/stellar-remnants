using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StellarRemnants.Control;
using StellarRemnants.Interact;

namespace StellarRemnants.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class FocusPanel : MonoBehaviour {
        public CanvasGroup canvasGroup;
        public SrPlayerController controller;
        public Camera cam;

        public GameObject inputItemPrefab;
        private ObjectInteractable displayedInteractable;
        private ObjectInteractable nextInteractable;

        private List<InteractionOption> options = new List<InteractionOption>();

        // public TMP_Text titleDisplay;
        // public TMP_Text stateDisplay;
        // public TMP_Text option1Display;
        // public TMP_Text option2Display;
        // public TMP_Text option3Display;
        // public TMP_Text option4Display;

        private bool fadeOut;
        private bool fadeIn;


        public void listenToInteractable(Interactable obj) {
            
            obj.AddStateListener(UpdateMenuItems);
        }

        public void UpdateMenuItems(Interactable obj, StateChange type) {
            InteractionField[] fields = obj.BuildFieldList(controller.player.GetAccessCredentials());

            for(int i = 0; i < options.Count; i++) {
                InteractionOption option = options[i];
                GameObject.Destroy(option.gameObject);
            }
            options.Clear();


            for(int i = 0; i < fields.Length; i++) {
                InteractionField field1 = fields[i];
                GameObject prefab;
                switch(field1.slot) {
                    
                    case 1: {prefab = inputItemPrefab; break;}
                    case 2: {prefab = inputItemPrefab; break;}
                    case 3: {prefab = inputItemPrefab; break;}
                    case 4: {prefab = inputItemPrefab; break;}
                    case 5: {prefab = inputItemPrefab; break;}

                    default: {prefab = inputItemPrefab; break;}
                }




                //Debug.Log(fields[i].text); // TODO: This should add these rows to the actual display
                InteractionOption field = Instantiate(prefab, this.transform.position + (new Vector3(0, -i*30f, 0)), Quaternion.identity).GetComponent<InteractionOption>();
                options.Add(field);
                field.key.text = "" + fields[i].slot;
                field.text.text = fields[i].text;

                field.transform.SetParent(this.transform);
            }
        }

        void Start() {
            if(canvasGroup == null) {
                this.canvasGroup = GetComponent<CanvasGroup>();
            }

            controller.player.focusInteractableEvent += onNodeUpdate;

            canvasGroup.alpha = 0f;
        }

        void Update() {
            if(fadeOut) {
                canvasGroup.alpha -= 10f * Time.deltaTime;
            }
            else if(fadeIn) {
                canvasGroup.alpha += 10f * Time.deltaTime;
            }

            if(displayedInteractable != null) {
                canvasGroup.transform.position = cam.WorldToScreenPoint(displayedInteractable.GetFocalPoint() + cam.transform.right * displayedInteractable.MenuOffset);
            }
        }
        
        void FixedUpdate() {
            if(displayedInteractable != nextInteractable) {
                if(displayedInteractable == null) {
                    fadeIn = true;
                    displayedInteractable = nextInteractable;
                    displayedInteractable.AddStateListener(UpdateMenuItems);
                    Debug.Log("Adding listener");
                    UpdateMenuItems(displayedInteractable, StateChange.General);
                }
                else if(!fadeOut) { // Check fadeOut because it's only true if it's hit this spot once already
                    fadeOut = true;
                    displayedInteractable.RemoveStateListener(UpdateMenuItems);
                    Debug.Log("Removing listener");
                }
            }


            if(fadeOut && canvasGroup.alpha <= 0f) {
                fadeOut = false;
                canvasGroup.alpha = 0;
                displayedInteractable = null;
            }
            else if(fadeIn) {
                displayedInteractable = nextInteractable;
                if(canvasGroup.alpha >= 1f) {
                    fadeIn = false;
                    canvasGroup.alpha = 1f;
                }
            }
        }

        void onNodeUpdate(ObjectInteractable node) {
            nextInteractable = node;
        }

    }



    public abstract class MenuItem {
        

        public static GameObject GenerateElement(int slot, string text) {
            
            return null;
        }
    }

    public class MenuOptionItem : MenuItem {
        public new static GameObject GenerateElement(int slot, string text) {
            return null;
        }
    }

    public class MenuStateItem : MenuItem {
        public new static GameObject GenerateElement(int slot, string text) {
            return null;
        }
    }
}
