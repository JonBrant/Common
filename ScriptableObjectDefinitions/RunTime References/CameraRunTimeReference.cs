using System;
using UnityEngine;


namespace Com.PI.RunTimeReferences {
    [CreateAssetMenu(fileName = "Camera RunTimeReference", menuName = "PI/RunTime References/Camera Reference Holder")]
    [Serializable]
    public class CameraRunTimeReference : RunTimeReferenceScriptableObject {
        public Camera Camera;
        public override void Reset() { Camera = null; }
    }
}