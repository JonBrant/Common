using System;
using System.Collections.Generic;
using UnityEngine;
using Com.PI.RunTimeReferences;
using Com.PI.StateMachine;


namespace Com.PI.Cursor {
    /// <summary>
    /// Cursor manager with StateMachine. Contains a list of Cursor Scriptable Objects with Textures
    /// and Left/Right click prefabs.
    /// </summary>
    [RequireComponent(typeof(SelectionBox))]
    public partial class CursorManager : MonoBehaviour {
        [Header("References")]
        public CameraRunTimeReference CameraReference;
        public BoolRunTimeReference MouseOverUI;

        public Action<Vector3> OnLeftClick;
        public Action<Vector3> OnRightClick;
        public Action<IState> StateChange;

        public FiniteStateMachine StateMachine { get; set; }
        public List<CursorScriptableObject> Cursors = new List<CursorScriptableObject>();

        private void Awake() {
            CameraReference.Camera = GetComponent<Camera>();
            InitStateMachine();
        }

        Tuple<Texture2D, Vector2> GetCursorValues(CursorScriptableObject inputCursorScriptableObject) { return new Tuple<Texture2D, Vector2>(inputCursorScriptableObject.CursorTexture, inputCursorScriptableObject.HotSpot); }

        private void Update() { StateMachine.Tick(); }

        CursorDefaultState defaultState;

        public void InitStateMachine() {
            StateMachine = new FiniteStateMachine();
            StateMachine.SetState(new CursorDefaultState(this));
        }
    }
}