using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Interact {
    public class IntegratedSystem : MonoBehaviour {

        private static readonly int PRIORITY_LEVELS = 5;


        private List<IntegratedInteractable> powerSources;
        
        private float power;
        private List<IntegratedInteractable>[] interactables = new List<IntegratedInteractable>[PRIORITY_LEVELS];
        private int iterator;
        private int priorityIterator = PRIORITY_LEVELS;
        private float lastIteration;
        private float timeDelta;

        void Start() {
            lastIteration = Time.time;
        }

        
        void Update() {
        
        }

        void FixedUpdate() {
            // With the way this function is set up, there will be an unused 1 tick gap between each priority level.
            if(priorityIterator < PRIORITY_LEVELS) {
                List<IntegratedInteractable> list = interactables[priorityIterator];
                if(iterator < list.Count) {
                    power = list[iterator].DrawPower(power, timeDelta);
                    iterator++;
                }
                else {
                    iterator = 0;
                    priorityIterator++;
                }
            }
            else {
                if(iterator < powerSources.Count) {
                    //AddPower(powerSources[iterator], timeDelta);
                    iterator++;
                }
                else {
                    iterator = 0;
                    priorityIterator = 0;
                    timeDelta = Time.time - lastIteration; // What is the timeDelta for the first iteration?
                    lastIteration = Time.time;
                }
            }
        }




        private void AddPower(IntegratedInteractable interactable) {

        }


        // void FixedUpdate() {
        //     if(iterator < interactables.Length) {
        //         DrawPower(interactables[iterator++]);
        //     }
        //     else {
        //         int i = iterator - interactables.Length;
        //         if(i < powerSources.Length) {
        //             AddPower(powerSources[i]);
        //             iterator++;
        //         }
        //         else {
        //             iterator = 0;
        //         }
        //     }
        // }


        // void AltFixedUpdate() {
        //     if(iterator > -1) {
        //         DrawPower(interactables[iterator--]);
        //     }
        //     else {
        //         int i = -(iterator+1);
        //         if(i < powerSources.Length) {
        //             AddPower(powerSources[i]);
        //             iterator--;
        //         }
        //         else {
        //             iterator = interactables.Length;
        //         }
        //     }

        // }
    }

    public enum InteractablePriorityLevel {
        HIGHEST = 0, 
        HIGH = 1, 
        MODERATE = 2, 
        LOW = 3, 
        LOWEST = 4
    }
}
