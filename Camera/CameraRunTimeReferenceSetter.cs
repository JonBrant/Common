using UnityEngine;


namespace Com.PI.RunTimeReferences {
    public class CameraRunTimeReferenceSetter : MonoBehaviour {
        public CameraRunTimeReference CameraReference;

        private void Awake() {
            Camera camera = GetComponent<Camera>();
            if (camera != null) {
                CameraReference.Camera = camera;
            } else {
                Debug.LogErrorFormat("{0}'s CameraRunTimeReferenceSetter couldn't find a Camera attached!", gameObject.name);
            }
        }
    }
}