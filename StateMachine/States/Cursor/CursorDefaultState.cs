using System.Linq;
using UnityEngine;
using Com.PI.StateMachine;


namespace Com.PI.Cursor {
    public partial class CursorManager {
        public class CursorDefaultState : IState {
            private CursorScriptableObject currentCursor;
            private readonly CursorManager cursorManager;
            private readonly SelectionBox selectionBox;

            public CursorDefaultState(CursorManager inputCursorManager) {
                cursorManager = inputCursorManager;
                selectionBox = cursorManager.GetComponent<SelectionBox>();
            }

            public void Tick() {
                if (cursorManager.MouseOverUI.Value == true) {
                    return;
                }

                if (Input.GetMouseButtonDown(0) && cursorManager.MouseOverUI.Value == false) {
                    selectionBox.isSelecting = true;
                    selectionBox.mousePosition1 = Input.mousePosition;

                    //For clicking single targets
                    if (selectionBox.mousePosition1 == Input.mousePosition) {
                        LayerMask layerMask = LayerMask.GetMask("Units");
                        RaycastHit hit;
                        Ray ray = cursorManager.CameraReference.Camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                            //Handle hits here.
                        }
                    }
                }

                //Process items selected by SelectionBox
                if (Input.GetMouseButtonUp(0)) {
                    foreach (var selectableObject in FindObjectsOfType<MonoBehaviour>()) {
                        if (IsWithinSelectionBounds(selectableObject.gameObject)) {
                            //Handle selection here.
                        }
                    }

                    selectionBox.isSelecting = false;
                }

                if (Input.GetMouseButtonDown(1) && cursorManager.MouseOverUI.Value == false) {
                    int layerMask = LayerMask.GetMask("Ground");
                    RaycastHit hit;
                    Ray ray = cursorManager.CameraReference.Camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                        if (currentCursor.RightClickPrefab != null) {
                            GameObject clickPrefab = Instantiate(currentCursor.RightClickPrefab);
                            clickPrefab.transform.localScale *= currentCursor.RightClickPrefabScale;
                            clickPrefab.transform.position = hit.point;
                            cursorManager.OnRightClick?.Invoke(hit.point);
                        }
                    }
                }
            }

            public void OnEnter() {
                currentCursor = cursorManager.Cursors.FirstOrDefault(x => x.name == "Default");
                UnityEngine.Cursor.SetCursor(cursorManager.GetCursorValues(currentCursor).Item1, cursorManager.GetCursorValues(currentCursor).Item2, CursorMode.Auto);
                cursorManager.OnRightClick += OnRightClickHandler;
            }

            public void OnExit() { cursorManager.OnRightClick -= OnRightClickHandler; }

            private void OnRightClickHandler(Vector3 clickPosition) { }

            public bool IsWithinSelectionBounds(GameObject inputGameobject) {
                if (!selectionBox.isSelecting)
                    return false;

                var viewportBounds = CameraUtilities.GetViewportBounds(cursorManager.CameraReference.Camera, selectionBox.mousePosition1, Input.mousePosition);
                return viewportBounds.Contains(cursorManager.CameraReference.Camera.WorldToViewportPoint(inputGameobject.transform.position));
            }
        }
    }
}