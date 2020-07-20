using System;
using UnityEngine;


namespace Com.PI.RunTimeReferences {
    [CreateAssetMenu(fileName = "Bool RunTimeReference", menuName = "PI/RunTime References/Bool Reference Holder")]
    [Serializable]
    public class BoolRunTimeReference : RunTimeReferenceScriptableObject {
        public bool Value = false;

        public override void Reset() { Value = false; }
    }
}