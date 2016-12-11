using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OctaRoom : MonoBehaviour {

    public const float DROP_RATE = 1f;

    public GameObject PowerUpPrefab;

    public static OctaRoom Instance;

    private Image _image;
    private float _nextDropTime;


    void Awake()
    {
        Instance = this;
        Debug.Log(gameObject.name);
    }

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();    
	}
	
	// Update is called once per frame
	void Update () {
        if (_nextDropTime < Time.time)
            CreateRandomDrop();
	}

    public void CreateRandomDrop()
    {
        GameObject powerUp = GameObject.Instantiate(PowerUpPrefab);
        powerUp.transform.position = MapController.GenerateRandomPointInsideMap();
        powerUp.GetComponent<BasePowerUP>().Initialize();
        _nextDropTime = Time.time + DROP_RATE + Random.Range(0, 10);
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
