using UnityEngine;


namespace Com.PI.Cursor {
    [CreateAssetMenu(fileName = "New Cursor", menuName = "PI/Cursor")]
    public class CursorScriptableObject : ScriptableObject {
        public Texture2D CursorTexture;
        public Vector2 HotSpot = new Vector2();
        public float LeftClickPrefabScale = 1.0f;
        public float RightClickPrefabScale = 1.0f;
        public GameObject LeftClickPrefab;
        public GameObject RightClickPrefab;
    }
}