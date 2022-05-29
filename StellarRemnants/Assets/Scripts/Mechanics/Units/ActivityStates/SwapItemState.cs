using UnityEngine;
using StellarRemnants.Inventory;

namespace StellarRemnants.Units {
    public class SwapItemState : BaseActivityState {
        //public Item swapTo;

        /*----------------------------------------
        |   CONSTRUCTORS
        ----------------------------------------*/
        public SwapItemState(PlayerCharacter p) : base(p) { }
        public SwapItemState(BaseActivityState previous) : base(previous) { }








    // public bool EquipItem() { // Equips the currently held item, switching to Held Equipped mode. 
    //     // if(!holdState.IsItemEquipped && !animationLocked && heldItem.GetEquipmentType() != EquipmentType.NONE) {
    //     //     switch(heldItem.GetEquipmentType()) {
    //     //         case EquipmentType.GUN: {
    //     //             // If all weapon slots are used, swap out the currently selected weapon.
    //     //             // Side-arms will go into Tool slots if weapon slots are full and there is at least one open tool slot
    //     //             // D-pad/1234 to equip to specific Tool slot
    //     //             break;
    //     //         }
    //     //         case EquipmentType.TOOL:{
    //     //             // If all tool slots are used, swap out the currently selected tool.
    //     //             break;
    //     //         }
    //     //         case EquipmentType.SUIT: {
    //     //             break;
    //     //         }
    //     //     }
    //     // }

    //     return false;
    // }

    // // Unquips the currently held item, switching to Held Unequipped modes.
    // public bool UnequipItem() {
    //     // if(holdState.IsItemEquipped && !animationLocked) {
    //     //     holdState = HoldState.HoldUnequipped;

    //     //     switch(heldItem.GetEquipmentType()) {
    //     //         case EquipmentType.GUN: {
    //     //             equippedWeapons[selectedWeapon] = null;
    //     //             selectedWeapon = common.NO_TOOL_EQUIPPED;
    //     //             break;
    //     //         }
    //     //         case EquipmentType.TOOL: {
    //     //             equippedTools[selectedTool] = null;
    //     //             selectedTool = common.NO_TOOL_EQUIPPED;
    //     //             break;
    //     //         }
    //     //     }
    //     //     return true;
    //     // }
    //     return false;
    // }


    // public bool CycleWeapons(int dir) {
    //     // int increment = dir < 0 ? -1 : 1;
    //     // int startIndex = selectedWeapon < 0 ? common.maxEquippedGuns : selectedWeapon + common.maxEquippedGuns;

    //     // for(int i = 0; i < common.maxEquippedGuns; i++) {
    //     //     int nextWeapon = (startIndex + (i * increment)) % common.maxEquippedGuns;
    //     //     if(equippedWeapons[nextWeapon] != null) {
    //     //         return SwapToWeapon(nextWeapon);
    //     //     }
    //     // }

    //     return false;
    // }

    // public bool CycleTools(int dir) {
    //     // int increment = dir < 0 ? -1 : 1;
    //     // int startIndex = selectedTool < 0 ? common.maxEquippedTools : selectedTool + common.maxEquippedTools;

    //     // for(int i = 0; i < common.maxEquippedTools; i++) {
    //     //     int nextTool = (startIndex + (i * increment)) % common.maxEquippedTools;
    //     //     if(equippedTools[nextTool] != null) {
    //     //         return SwapToTool(nextTool);
    //     //     }
    //     // }

    //     return false;
    // }

    // public bool SwapToTool(int toolSlot) {
    //     // if(toolSlot == selectedTool || equippedTools[toolSlot] == null) {
    //     //     if(holdState.IsItemEquipped && heldItem.GetEquipmentType() == EquipmentType.TOOL && !animationLocked) {
    //     //         swapTo = null;
    //     //         swapHeldItem = true;
    //     //         return true;
    //     //     }
    //     //     return false;
    //     // }
    //     // else {
    //     //     selectedTool = toolSlot;
    //     //     if(holdState.IsItemEquipped && heldItem.GetEquipmentType() == EquipmentType.TOOL && !animationLocked) {
    //     //         swapTo = equippedTools[toolSlot]; // Only swap to tool if previous tool was equipped and held
    //     //         swapHeldItem = true;
    //     //     }
    //     //     return true;
    //     // }
    //     return false;
    // }

    // public bool SwapToWeapon(int weaponSlot) {
    //     // if(weaponSlot == selectedWeapon || equippedWeapons[weaponSlot] == null) {
    //     //     if(holdState.IsItemEquipped && heldItem.GetEquipmentType() == EquipmentType.GUN && !animationLocked) {
    //     //         swapTo = null;
    //     //         swapHeldItem = true;
    //     //         return true;
    //     //     }
    //     //     return false;
    //     // }
    //     // else {
    //     //     selectedWeapon = weaponSlot;
    //     //     if(holdState.IsItemEquipped && !animationLocked) {
    //     //         swapTo = equippedWeapons[weaponSlot];
    //     //         swapHeldItem = true;
    //     //     }
    //     //     return true;
    //     // }
    //     return false;
    // }





    // private void updateHeldItem() {
    //     if(swapHeldItem) {
    //         if(swapTo == heldItem) {
    //             // If current item and swap-to are the same object, set swap-to to null and swapHeldItem = false and reverse the animation.
    //             swapTo = null;
    //             swapHeldItem = false;
    //         }
    //         else {
    //             if(heldItem != null) {
    //                 // If current item is not null, perform put-away animation
    //                 // When animation is complete, set current item to null
    //                 //heldItem = null;
    //             }
    //             else {
    //                 // If current item is null and swap-to is not null, perform pull-out animation
    //                 // When animation is completed: 1. Set current item to swap-to; 2. Set swap-to to null; 3. Set swapHeldItem = false
    //                 //heldItem = swapTo;
    //                 //swapTo = null;
    //                 //swapHeldItem = false;
    //             }
    //         }
    //     }
    // }







    }
}
