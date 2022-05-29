using UnityEngine;

namespace StellarRemnants.Scriptables {
    [CreateAssetMenu(fileName = "DoorVariant", menuName = "Resources/Scriptable Objects/Interactables/Other Variants/Door Variant", order = 1)]
    public class DoorVariant : IntegratedVariant {
        
        [Header("Door Attributes")]

        [SerializeField]
        [Tooltip("Size of the door's threshold size, measured in square meters.")]
        public float MaxThresholdSize = 20f;

        [SerializeField] [Range(0f, 1f)]
        [Tooltip("The rate at which the door opens and closes, measured in percent per second.")]
        public float OpenRate = 0.1f;
    }
}