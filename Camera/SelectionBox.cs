using UnityEngine;


namespace Com.PI.Cursor  {
    /// <summary>
    /// Class to draw rectangular box during mouse drag. Put this on camera
    /// </summary>
    public class SelectionBox : MonoBehaviour {
        public bool isSelecting = false;
        public Vector3 mousePosition1;

        void OnGUI() {
            if (isSelecting) {
                // Create a rect from both mouse positions
                var rect = CameraUtilities.GetScreenRect(mousePosition1, Input.mousePosition);
                CameraUtilities.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
                CameraUtilities.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            }
        }
    }
}