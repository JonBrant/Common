using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterialByScale : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTextureScale = new Vector2(transform.localScale.x,transform.localScale.z);
        
    }

    // Update is called once per frame
    void Update() {
    }
}