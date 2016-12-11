using UnityEngine;
using System.Collections;
using System;

public class ProjectileParticle : MonoBehaviour {

    public bool CanMove = false;
    private Vector3 _destination;
	
	// Update is called once per frame
	void Update () {
        if (!CanMove) return;
        transform.Translate((_destination - transform.position) * 0.2f * Time.deltaTime);

	}

    internal void Initialize(Color color, float destroyTime = 0.1f, bool move = false)
    {
        GetComponent<SpriteRenderer>().color = color;
        CanMove = move;
        Destroy(gameObject, move ? destroyTime : destroyTime);
        if (CanMove)
            _destination = MapController.GenerateRandomPointInsideMap();
    }
}
