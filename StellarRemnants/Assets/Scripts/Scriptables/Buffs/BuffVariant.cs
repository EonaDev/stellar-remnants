using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StellarRemnants.Scriptables {
    public class BuffVariant : ScriptableObject {
        public string NameLocalizationKey = "Buf_Unnamed";
        public string DescLocalizationKey = "Buf_Unnamed_Desc";
        public bool Visible = true;
        public float MaxQuantity = 1f;
        public float DecayRate = 0.1f;
    }
}