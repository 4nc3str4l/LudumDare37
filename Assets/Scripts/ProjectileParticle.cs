using UnityEngine;
using System.Collections;
using System;

public class ProjectileParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Initialize(Color color)
    {
        //GetComponent<SpriteRenderer>().color = color;
        Destroy(gameObject, 0.1f);   
    }
}
