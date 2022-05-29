using UnityEngine;

namespace StellarRemnants.Control {
    public abstract class UnitController : MonoBehaviour {

        public virtual void OnMove(Vector2 inputDirection) { }

        public virtual void SetFocusControlMode(bool enable) { }
        public virtual void SetIdleControlMode(bool enable) { }
        public virtual void SetAdsControlMode(bool enable) { }
        public virtual void SetHoldItemControlMode(bool enable) { }
        public virtual void SetLiftItemControlMode(bool enable) { }
        public virtual void SetMenuControlMode(bool enable) { }
        public virtual void SetOperatorControlMode(bool enable) { }

    }
}
