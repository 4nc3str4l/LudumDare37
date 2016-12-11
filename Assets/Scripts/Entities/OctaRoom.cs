using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OctaRoom : MonoBehaviour {

    public const float DROP_RATE = 1f;
    public GameObject PowerUpPrefab;
    public static OctaRoom Instance;
    private Image _image;
    private float _nextDropTime;
    public List<BasePowerUP> ActivePowerUps;
    
    void Awake()
    {
        ActivePowerUps = new List<BasePowerUP>();
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
        BasePowerUP.OnPowerUpTaken += OnPowerUpTaken;    
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
        BasePowerUP pu = powerUp.GetComponent<BasePowerUP>();
        pu.Initialize();
        ActivePowerUps.Add(pu);
        _nextDropTime = Time.time + DROP_RATE + Random.Range(0, 10);
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    void OnPowerUpTaken(BasePowerUP powerUp)
    {
        ActivePowerUps.Remove(powerUp);
    }
}
