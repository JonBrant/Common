using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


//using Sirenix.OdinInspector;

namespace Com.PI.EditorTools {
    [CreateAssetMenu(fileName = "Prefab Component Changer", menuName = "PI/Tools/Prefab Component Changer")]
    public class PrefabComponentChanger : ScriptableObject {
        public List<GameObject> Prefabs = new List<GameObject>();

#if ODIN_INSPECTOR
        [FoldoutGroup("Add Components")]
#endif
        public List<MonoScript> ComponentsToAdd = new List<MonoScript>();
#if ODIN_INSPECTOR
        [FoldoutGroup("Add Components")] [Button]
#endif
        public void AddComponents() {
            for (int j = 0; j < Prefabs.Count; j++) {
                GameObject Prefab = Prefabs[j];
                for (int i = 0; i < ComponentsToAdd.Count; i++) {
                    AddComponent(Prefabs[j], ComponentsToAdd[i]);
                }
            }
        }

        private Component AddComponent(GameObject inputPrefab, MonoScript inputComponent) {
            Type componentType = Type.GetType(inputComponent.name);
            Component returnValue = null;
            if (componentType != null) {
                GameObject tempPrefab = PrefabUtility.InstantiatePrefab(inputPrefab) as GameObject;
                if (tempPrefab != null) {
                    returnValue = tempPrefab.AddComponent(componentType);
                    PrefabUtility.ApplyPrefabInstance(tempPrefab, InteractionMode.UserAction);
                    DestroyImmediate(tempPrefab);
                } else {
                    Debug.LogFormat("Failed to instantiate prefab.");
                }
            } else {
                Debug.LogFormat("{0} did not have valid type", inputComponent);
                return null;
            }

            return returnValue;
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Remove Components")]
#endif
        public List<MonoScript> ComponentsToRemove = new List<MonoScript>();
#if ODIN_INSPECTOR
        [FoldoutGroup("Remove Components")] [Button]
#endif
        public void RemoveComponents() {
            for (int k = 0; k < ComponentsToRemove.Count; k++) {
                for (int j = 0; j < Prefabs.Count; j++) {
                    RemoveComponent(Prefabs[j], ComponentsToRemove[k]);
                }
            }
        }

        private void RemoveComponent(GameObject inputPrefab, MonoScript inputComponent) {
            List<Component> components = inputPrefab.GetComponents<Component>().ToList();
            for (int i = 0; i < components.Count; i++) {
                Type component = components[i].GetType();
                if (inputPrefab.GetComponent(component).GetType().ToString() == inputComponent.name) {
                    GameObject tempPrefab = PrefabUtility.InstantiatePrefab(inputPrefab) as GameObject;
                    if (tempPrefab != null) {
                        DestroyImmediate(tempPrefab.GetComponent(component));
                        PrefabUtility.ApplyPrefabInstance(tempPrefab, InteractionMode.UserAction);
                    } else {
                        Debug.LogFormat("{0} did not have valid type", inputComponent);
                    }

                    break;
                }
            }
        }
    }
}

public class PrefabComponentChanger : MonoBehaviour {
    public List<GameObject> Prefabs = new List<GameObject>();

#if ODIN_INSPECTOR
    [FoldoutGroup("Add Components")]
#endif
    public List<MonoScript> ComponentsToAdd = new List<MonoScript>();
#if ODIN_INSPECTOR
    [FoldoutGroup("Add Components")] [Button]
#endif
    public void AddComponents() {
        for (int j = 0; j < Prefabs.Count; j++) {
            GameObject Prefab = Prefabs[j];
            for (int i = 0; i < ComponentsToAdd.Count; i++) {
                AddComponent(Prefabs[i], ComponentsToAdd[j]);
            }
        }
    }

    private void AddComponent(GameObject inputPrefab, MonoScript inputComponent) {
        Type componentType = Type.GetType(inputComponent.name);
        if (componentType != null) {
            inputPrefab.AddComponent(componentType);
            PrefabUtility.ApplyPrefabInstance(inputPrefab, InteractionMode.UserAction);
        } else {
            Debug.LogFormat("{0} did not have valid type");
        }
    }

#if ODIN_INSPECTOR
    [FoldoutGroup("Remove Components")]
#endif
    public List<MonoScript> ComponentsToRemove = new List<MonoScript>();
#if ODIN_INSPECTOR
    [FoldoutGroup("Remove Components")] [Button]
#endif
    public void RemoveComponents() {
        for (int k = 0; k < ComponentsToRemove.Count; k++) {
            for (int j = 0; j < Prefabs.Count; j++) {
                RemoveComponent(Prefabs[j], ComponentsToRemove[k]);
            }
        }
    }

    private void RemoveComponent(GameObject inputPrefab, MonoScript inputComponent) {
        List<Component> components = inputPrefab.GetComponents<Component>().ToList();
        for (int i = 0; i < components.Count; i++) {
            Type component = components[i].GetType();
            if (inputPrefab.GetComponent(component).GetType().ToString() == inputComponent.name) {
                DestroyImmediate(inputPrefab.GetComponent(component));
                PrefabUtility.ApplyPrefabInstance(inputPrefab, InteractionMode.UserAction);
                return;
            }
        }
    }
}