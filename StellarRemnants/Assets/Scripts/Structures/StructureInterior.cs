using StellarRemnants.Interact;
using StellarRemnants.Simulation.Atmosphere;
using StellarRemnants;
using UnityEngine;

public class StructureInterior : MonoBehaviour {

    /*----------------------------------------
    |   STATIC VARAIBLES
    ----------------------------------------*/
    public static readonly float HorizontalGridScale = 2.5f;
    public static readonly float VerticalGridScale = 5f;


    /*----------------------------------------
    |   LOCAL VARIABLES
    ----------------------------------------*/
    public StructureRoom[] Rooms;
    public Threshold[] Doors;
    public IntegratedInteractable[] connectedInteractables;
    public AtmoVolume Atmosphere; // General atmosphere of whole structure
    
    public ReactorCore reactor;
    public AtmoRegulator lifeSupport;
    public float power;
    
    private float[] priorityPowerLevels = new float[5];
    private int powerIterator;

    
    /*----------------------------------------
    |   BASIC FUNCTIONS
    ----------------------------------------*/
    public void RegisterInteractable(IntegratedInteractable interactable) {
        // add to connectedInteractables
        for(int i = 0; i < interactable.Priority; i++) {
            priorityPowerLevels[i] += interactable.PowerDraw;
        }
    }

    public void UnregisterInteractable(IntegratedInteractable interactable) {
        // remove from connectedInteractables
        for(int i = 0; i < interactable.Priority; i++) {
            priorityPowerLevels[i] -= interactable.PowerDraw;
        }
    }

    // public void FixedUpdate() {
    //     PowerUpdate();




    // }

    

    // // If total power generation is less than the priority threshold of equivalent level of an interactable, shutdown interactables in that priority level until the power is no longer greater than the threshold.
    // // If total power is greater, it will reboot anything that was shutdown for lack of power.
    // // THIS WILL NOT WORK IF POWER DRAW FLUCTUATES REGULARLY (without updating the thresholds at least)


    // // NEW IDEA: Sort interactable list: Power Sources -> Highest -> High -> Moderate -> Low -> Lowest
    // // This technique would make actual power draw per interactable irrelevant.

    // public void PowerUpdate() {
    //     IntegratedInteractable interactable = connectedInteractables[powerIterator++];
    //     if(powerIterator >= connectedInteractables.Length) { powerIterator = 0; }
        
    //     if(interactable.IsPowered) {
    //         if(interactable.DrawPower(ref power)) { // Must account for priority levels
    //         }
    //         else {
    //             interactable.Shutdown(false);
    //         }
    //     }
    //     else if(interactable.AllowAutoBoot()){ // Somehow, only autoboot objects with lowest priority first.
    //         // Auto boot when relevant? What about when there isn't enough power?
    //     }
    // }




}