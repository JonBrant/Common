using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.PI.RunTimeReferences {
    public abstract class RunTimeReferenceScriptableObject : ScriptableObject {
        public Action ListUpdated;
        public abstract void Reset();
    }
}