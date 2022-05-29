using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Units
{
    public class InteractionState : BaseActivityState {
        public InteractionState(PlayerCharacter p) : base(p) { }
        public InteractionState(BaseActivityState previous) : base(previous) { }


        // TODO: This state handles various interactions like prying open a door, cutting a lock, and any other activity involving an ObjectInteractable. Might also replace OperateState.
        // This class will signal the ObjectInteractable being interacted with once the activity is complete.
    }
}
