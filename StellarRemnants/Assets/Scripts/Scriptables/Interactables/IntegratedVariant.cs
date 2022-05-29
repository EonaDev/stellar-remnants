using UnityEngine;
using StellarRemnants.Interact;

namespace StellarRemnants.Scriptables {
    [CreateAssetMenu(fileName = "IntegratedObjectVariant", menuName = "Resources/Scriptable Objects/Interactables/Integrated Variant", order = 1)]
    public class IntegratedVariant : ObjectVariant {
        [Header("Integration Attributes")]
        public TechType Technology = TechType.HumanTech;
        public float IdlePowerDraw;
        public float ActivePowerDraw;
        public bool LocalTerminalAccess;
    }
}