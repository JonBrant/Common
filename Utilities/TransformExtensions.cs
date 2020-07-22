using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Com.PI.Utilities {
    public static class TransformExtensions {
        public static Vector3 RandomGroundPosition(this Transform target, float range, string inputLayerMask = "Ground", int maxAttempts = 100) {
            Vector3 returnValue = Vector3.zero;

            for (int i = 0; i < maxAttempts; i++) {
                Vector3 randomPosition = target.position+UnityEngine.Random.onUnitSphere * range;
                randomPosition.y = 50;
                int layerMask = LayerMask.GetMask(inputLayerMask);
                if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask)) {
                    return hit.point;
                }
            }

            return returnValue;
        }
    }
}