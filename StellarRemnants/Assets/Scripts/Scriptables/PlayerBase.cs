using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Resources/Scriptable Objects/Player Data", order = 1)]
public class PlayerBase : ScriptableObject {


    [Header("General Movement")]

    [SerializeField] [Range(0f, 1f)] [Tooltip("Movement speed multiplier when moving sideways.")]
    public float StrafeSpeedMultiplier = 0.875f;

    [SerializeField] [Range(0f, 1f)] [Tooltip("Movement speed multiplier when moving backwards.")]
    public float ReverseSpeedMultiplier = 0.75f;


    [Header("Air & Falling Movement")]

    [SerializeField] [Range(0f, 20f)] [Tooltip("[m/s] Upward velocity applied when jumping.")]
    public float JumpStrength = 4.5f;

    [SerializeField] [Range(0f, 1f)] [Tooltip("Drag applied to rigidbody when airborn.")]
    public float AirbornDrag = 0.1f; // TODO: Implement; it's not currently being used.

    [SerializeField] [Range(0f, 1f)] [Tooltip("[s] Duration in which player can jump after falling off ground.")]
    public float AirbornGraceJumpWindow = 0.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Impact velocity at which player begins to take fall damage.")]
    public float FallDamageStartVelocity = 15f;

    [SerializeField] [Range(0, 100)] [Tooltip("[d/(m/s)] Damage multiplier for each m/s of impact velocity after FallDamageStartVelocity.")]
    public float fallDamageScaling = 1f; // Damage = scaling * (velocity-fallDamageStartVelocity); Might reevaluate calculation.
    
    [SerializeField] [Range(-100f, 0f)] [Tooltip("[m/s^2] Optimal gravity for player to move in. Gravity levels further from this number will cause movement penalties.")]
    public float OptimalGravity = -9.81f; // TODO: Should high gravity levels also reduce healing?

    [SerializeField] [Range(0f, 1f)] [Tooltip("Minimum speed multiplier based on the effects of gravity.")]
    public float OptimalGravitySpeedMultiplierMin = 0.5f;

    [SerializeField] [Range(-100f, 0f)] [Tooltip("[m/s^2] Gravity level at which gravity is considered \"low\".")]
    public float LowGravityThreshold = -3f;

    [SerializeField] [Range(-100f, 0f)] [Tooltip("[m/s^2] Gravity level at which gravity is considered \"micro gravity\" and micro gravity movement is enabled.")]
    public float MicroGravityThreshold = -0.05f; // 0.0636g is the gravity of Mimas, the smallest body in the solar system to achieve hydrostatic equilibrium


    [Header("Walk Movement")]

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Base walking speed.")]
    public float WalkSpeed = 3.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s^2] Acceleration while walking.")]
    public float WalkAcceleration = 0.5f;
    
    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while walking, will begin sliding.")]
    public float WalkStartSlideThreshold = 3.6f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while sliding, will resume walking.")]
    public float WalkEndSlideThreshold = 3.0f;

    [SerializeField] [Range(0, 1)] [Tooltip("[s] Window of time the player can decelerate from walking before switching to sliding state.")]
    public float WalkDecelerationWindow = 0.5f;

    [Header("Sprint Movement")]

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Base sprinting speed.")]
    public float SprintSpeed = 5.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s^2] Acceleration while sprinting.")]
    public float SprintAcceleration = 0.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while sprinting, will begin sliding.")]
    public float SprintStartSlideThreshold = 5.6f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while sliding, will resume sprinting.")]
    public float SprintEndSlideThreshold = 5.0f;

    [SerializeField] [Range(-1f, 1f)] [Tooltip("Minimum forward vector in which player can sprint.")]
    public float SprintMinimumAngle = 0.707f; // Roughly the square root of 1/2.


    [Header("Crouch & Sneak Movement")]

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Base sneaking speed.")]
    public float SneakSpeed = 2.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s^2] Acceleration while sneaking.")]
    public float SneakAcceleration = 0.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while sneaking, will begin sliding.")]
    public float SneakStartSlideThreshold = 5.1f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while sliding, will resume sneaking.")]
    public float SneakEndSlideThreshold = 2.0f;

    
    [Header("Misc Movement")]
    
    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Base swimming speed.")]
    public float SwimSpeed = 3.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Base climbing speed.")]
    public float ClimbSpeed = 2.5f;

    [SerializeField] [Range(0, 100)] [Tooltip("[m/s] Speed at which the player, while motionless, will begin sliding.")]
    public float MotionlessStartSlideThreshold = 0.1f;
    
    [Header("Slide Movement")]
    [Range(0, 90)]   [SerializeField] public float SlideStartAngle = 45f;
    [Range(0, 90)]   [SerializeField] public float SlideEndAngle = 35f;
    [Range(0, 90)]   [SerializeField] public float SlideEndAngleBracing = 40f;
    [Range(0, 100)]  [SerializeField] public float SlideFriction = 0.1f;
    [Range(0, 100)]  [SerializeField] public float SlideTumbleThreshold = 25f;
    [Range(0, 100)]  [SerializeField] public float SlideBraceDecelerationMultiplier = 3f;
    [Range(0f, 1f)]  [SerializeField] public float SlideDownSlopeVelocityMultiplier = 0.001f;
    [Range(0f, 1f)]  [SerializeField] public float SlideMinDuration = 0.25f;
    
    [Header("ADS Movement")]
    [Range(0f, 1f)] [SerializeField] public float lightAdsMovementSpeedMultiplier = 1f;
    [Range(0f, 1f)] [SerializeField] public float primeAdsMovementSpeedMultiplier = 0.9f;
    [Range(0f, 1f)] [SerializeField] public float heavyAdsMovementSpeedMultiplier = 0.5f;

    [Header("Interaction")]
    [Range(0f, 10f)] [SerializeField] public float FocusOnNodeRange = 3f;

    [Header("Equipment")]
    public readonly int maxEquippedGuns = 2;
    public readonly int maxEquippedTools = 4;
    public readonly int NO_TOOL_EQUIPPED = -1;
    

}
