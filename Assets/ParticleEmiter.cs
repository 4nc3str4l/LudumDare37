using UnityEngine;
using System.Collections;

public class ParticleEmiter : MonoBehaviour {


    public GameObject ParticlePrefab;
    private float _nextParticleTrow = 0;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_nextParticleTrow < Time.time)
            CreateParticle();
    }

    void CreateParticle()
    {
        GameObject particle = GameObject.Instantiate(ParticlePrefab,
            new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                        transform.position.y + Random.Range(-0.1f, 0.1f),
                        transform.position.z), transform.rotation) as GameObject;
        particle.GetComponent<ProjectileParticle>().Initialize(Color.white);
        _nextParticleTrow = Time.time + Random.Range(0.01f, 0.03f);
    }

}
