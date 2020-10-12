using System.Collections.Generic;
using System.Linq;

//using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;


namespace Com.PI.RunTimeReferences {
    [InitializeOnLoad]
    public static class RunTimeReferenceResetter {
        public static bool Debugging = true;
        private static string ResourcePath = "RunTime References";
        private static readonly List<RunTimeReferenceScriptableObject> ObjectsToReset;

        static RunTimeReferenceResetter() {
            if (ResourcePath == null) {
                Debug.LogErrorFormat("RunTime Reference Resetter's Resource Path is not set! Edit this to path of folder holding RunTime References, relative (inside of) a Resource folder!" + "RunTime References will NOT be reset without this!");
            }
            ObjectsToReset = Resources.LoadAll<RunTimeReferenceScriptableObject>(ResourcePath).ToList();
            if (AssetDatabase.IsValidFolder(ResourcePath)) {
                
                if (Debugging) {
                    Debug.LogFormat("{0} RunTime References loaded.", ObjectsToReset.Count);
                }
            } else {
                Debug.LogErrorFormat("RunTime Reference Resetter's Resource Path ({0}) is not valid! Edit this to path of folder holding RunTime References, relative (inside of) a Resource folder!" + "RunTime References will NOT be reset without this!", ResourcePath);
            }

            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state) {
            if (state == PlayModeStateChange.ExitingPlayMode) {
                for (int i = 0; i < ObjectsToReset.Count; i++) {
                    ObjectsToReset[i].Reset();
                    if (Debugging) {
                        Debug.LogFormat("{0} Reset!", ObjectsToReset[i].name);
                    }
                }
            }
        }
    }
}