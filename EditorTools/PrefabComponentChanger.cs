using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


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
                /*
                Type componentType = Type.GetType(ComponentsToAdd[i].name);
                if (componentType != null) {
                    Prefabs[j].AddComponent(componentType);
                    PrefabUtility.ApplyPrefabInstance(Prefab, InteractionMode.UserAction);
                } else {
                    Debug.LogFormat("{0} did not have valid type");
                }
                */
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
                /*
                GameObject Prefab = Prefabs[j];
                List<Component> components = Prefab.GetComponents<Component>().ToList();
                for (int i = 0; i < components.Count; i++) {
                    Type component = components[i].GetType();
                    if (Prefab.GetComponent(component).GetType().ToString() == ComponentsToRemove[k].name) {
                        DestroyImmediate(Prefab.GetComponent(component));
                        PrefabUtility.ApplyPrefabInstance(Prefab, InteractionMode.UserAction);
                        return;
                    }
                }
                */
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