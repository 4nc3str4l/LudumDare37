using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {


    public static UIController Instance;
    public GameObject PlayerHealthPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateHealthController(Participant player)
    {
        GameObject playerHealth = GameObject.Instantiate(PlayerHealthPrefab, transform) as GameObject;
        playerHealth.GetComponent<PlayerHealth>().Player = player;
    }
}
