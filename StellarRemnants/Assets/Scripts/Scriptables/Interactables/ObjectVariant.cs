using UnityEngine;
using StellarRemnants.Combat;

namespace StellarRemnants.Scriptables {
    [CreateAssetMenu(fileName = "StandaloneVariant", menuName = "Resources/Scriptable Objects/Interactables/Standalone Variant", order = 1)]
    public class ObjectVariant : ScriptableObject {

        [Header("General Attributes")]

        [SerializeField]
        [Tooltip("Localization key for this object. Defaults to Int_Unnamed.")]
        public string LocalizationKey = "Int_Unnamed";

        [Header("Health Attributes")]

        [SerializeField]
        [Tooltip("Max health of this object.")]
        public float MaxHealth = 100f;
        
        [SerializeField]
        [Tooltip("Durability of the object. Determines what type of damage it takes from different damage sources.")]
        public DurabilityType DurabilityType = DurabilityType.Armor;

        [Header("Interaction Attributes")]

        [SerializeField] [Range(45f, 180f)]
        [Tooltip("The maximum angle the InteractableObject can be focused on, centered on the object's forward vector.")]
        public float MaxInteractionAngle = 90f;

        [SerializeField] [Range(0f, 2f)]
        [Tooltip("The distance from the object's focus node in which the object's focus menu will appear.")]
        public float MenuOffset = 0.4f;
    }
}