using UnityEngine;

public class FactionReputation {
    public int loyalty; // Increases when player performs task for the faction, or in some way benefits the faction, but decreases when actions are to the detriment of the faction.
    public int respect; // Increases when the action is liked and decreases when the action is disliked.
    public int credibility; // Increases when agreements are kept, but decreases when they are not. Also increases when truth is told, but decreases when lies are found.
    public int boldness; // Increases when requesting dangerous tasks, but decreases when accepting safe tasks.
    public int discretion; // Increases when tasks are performed in a discrete manner, but decreases when done in non-discrete manners.
}