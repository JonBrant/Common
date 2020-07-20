using Debug = UnityEngine.Debug;
using System;
using System.Collections.Generic;
using UnityEngine;



//Usage transform.FirstChildOrDefault(x => x.name == "GameObjectName");
public static class TransformEx {
    public static Transform FirstChildOrDefault(this Transform parent, Func<Transform, bool> query) {
        if (parent.childCount == 0) {
            return null;
        }

        Transform result = null;
        for (int i = 0; i < parent.childCount; i++) {
            var child = parent.GetChild(i);
            if (query(child)) {
                UnityEngine.Debug.LogFormat("Found '{0}'!",child.name);
                return child;
            }

            result = FirstChildOrDefault(child, query);
        }

        return result;
    }
    
    public static Transform FindDeepChildBreadthFirst(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach(Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }    
 
    
    //Depth-first search
    public static Transform FindDeepChildDepthFirst(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChildDepthFirst(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    
}