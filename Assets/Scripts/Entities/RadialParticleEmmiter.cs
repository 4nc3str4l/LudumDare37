using UnityEngine;
using System.Collections;

public class RadialParticleEmmiter : MonoBehaviour {


    public GameObject ParticlePrefab;
    private float _nextParticleTrow = 0;
    public bool isActive = false;
    public Color c = Color.white;
    public bool MakeParticlesMove = true;
    

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (_nextParticleTrow < Time.time)
        {
            int NumParticles = Random.Range(0, 30);
            for (int i = 0; i < NumParticles; i++)
            {
                CreateParticle(c);
            }
        }   
    }

    public void CreateSomeParticles(Vector3 destination, int numberOfParticles, Color color)
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            CreateParticle(color, true);
        }
    }

    void CreateParticle(Color color, bool canMove = false)
    {
        GameObject particle = GameObject.Instantiate(ParticlePrefab,
            new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                        transform.position.y + Random.Range(-0.1f, 0.1f),
                        transform.position.z), transform.rotation) as GameObject;
        ProjectileParticle part = particle.GetComponent<ProjectileParticle>();
        part.Initialize(color, 0.3f, canMove);
        _nextParticleTrow = Time.time + Random.Range(0.01f, 0.03f);
    }

}
