public enum ItemSize {
    Tiny, // Small things like trinkets. These can be deposited in inventory.
    SideArm, // Side arms and most tools. These cannot be deposited in inventory, though the player may run while holding them.
    PrimaryWeapon, // Primary weapons. These cannot be deposited in inventory. The player may not run while holding them. If it is a weapon, the player may run if not ADS.
    HeavyWeapon, // Heavy weapons. Cannot be deposited in inventory and moves slowly while holding it unless it is a weapon and the player is not ADS.
    Deployable, // Portable items that must be placed before using. Player moves slowly while holding it. 
    Lifted // Should this be combined with deployable?
}