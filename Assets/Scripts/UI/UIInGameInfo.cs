using UnityEngine;
using System.Collections.Generic;

public class UIInGameInfo : MonoBehaviour {

    public static int deadCounter = 0;

    public GameObject PlayerInfoPrefab;

    public PlayerInfo PinkyInfo, Lemonidas, Cyani, Bloody;
    public Participant pPinky, pLemin, pCyani, pBloody;

    public List<PlayerInfo>_deadPlayers;

	// Use this for initialization
	void Start () {
        _deadPlayers = new List<PlayerInfo>();

        Participant.OnPlayerHurt += OnPlayerChange;
        Participant.OnPlayerDead += OnPlayerDie;

        PinkyInfo.Initialize(pPinky.Name, pPinky.Health, pPinky.PlayerColor);
        Lemonidas.Initialize(pLemin.Name, pLemin.Health, pLemin.PlayerColor);
        Cyani.Initialize(pCyani.Name, pCyani.Health, pCyani.PlayerColor);
        Bloody.Initialize(pBloody.Name, pBloody.Health, pBloody.PlayerColor);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void ResortListAndUpdateInfo()
    {
        PinkyInfo.UpdateInformation(pPinky.Health);
        Lemonidas.UpdateInformation(pLemin.Health);
        Cyani.UpdateInformation(pCyani.Health);
        Bloody.UpdateInformation(pBloody.Health);

        foreach (PlayerInfo pInf in _deadPlayers)
        {
            pInf.transform.SetParent(null);
            pInf.transform.SetParent(transform);
        }
    }

    public void AddToDeadPlayers(PlayerInfo newDeadPlayer)
    {
        _deadPlayers.Insert(0, newDeadPlayer);
    }

    void OnPlayerDie(Participant player)
    {
        ResortListAndUpdateInfo();
    }

    void OnPlayerChange(Participant player, float ammount)
    {
        ResortListAndUpdateInfo();
    }
    
}
