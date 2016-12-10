using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour {

    public const float MAX_KILL_DISTANCE = 8f;

    public enum GLOBAL_STATE { ASSASSIN, VICTIM }
    public enum STATE { GOING_TO_SPOT, HUNTING, KILLING }
    private Participant _participant;

    private GLOBAL_STATE _globalState = GLOBAL_STATE.VICTIM;
    private STATE _actualState = STATE.GOING_TO_SPOT;

    private Participant _killingParticipant;

	// Use this for initialization
	void Start () {
        _participant = GetComponent<Participant>();
        MapController.OnAssasinChange += OnAssassinChanged;
        Participant.OnPlayerDead += OnPlayerDeath;
	}
	
	// Update is called once per frame
	void Update () {

        switch (_globalState)
        {
            case GLOBAL_STATE.ASSASSIN:
                PredatorBehaivour();
                break;
            case GLOBAL_STATE.VICTIM:
                VictimBehaivour();
                break;
            default:
                break;
        }      
	}

    public void PredatorBehaivour()
    {
        switch (_actualState)
        {
            case STATE.HUNTING:
                Debug.Log("Hunting!!!");
                Hunting();
                break;
            case STATE.KILLING:
                Killing();
                break;
            default:
                _actualState = STATE.HUNTING;
                break;
        }
    }

    public KeyValuePair<float, Participant> MinPlayerDistance()
    {
        float minDistance = float.MaxValue;
        Participant closestParticipant = null;
        foreach (GameObject participant in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!participant.gameObject.Equals(this.gameObject))
            {
                float distance = Vector3.Distance(transform.position, participant.transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    closestParticipant = participant.GetComponent<Participant>();
                }
            }

        }
        return new KeyValuePair<float, Participant>(minDistance, closestParticipant);
    }


    public void Hunting()
    {
        KeyValuePair<float, Participant> closestDistanceParticipant = MinPlayerDistance();
        if(closestDistanceParticipant.Key < MAX_KILL_DISTANCE)
        {
            _killingParticipant = closestDistanceParticipant.Value;
            _actualState = STATE.KILLING;
        }
        else
        {
            if (DetectMapBoundsDistance() < 10f)
            {
                _participant.TurnRight();
            }

            Debug.Log("Closest Distance To participant" + closestDistanceParticipant.Value.Name + " Distance: " + closestDistanceParticipant.Key);
            _participant.MoveForward();
        }
    }

    public void Killing()
    {
        if(Vector3.Distance(transform.position, _killingParticipant.transform.position) > MAX_KILL_DISTANCE)
        {
            _killingParticipant = null;
            _actualState = STATE.HUNTING;
        }
        else
        {
            var dir = (_killingParticipant.transform.position + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), transform.position.z) + _killingParticipant.transform.right) - transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
            _participant.Shoot();
            _participant.MoveForward();
            Hunting();
        }
    }

    public void VictimBehaivour()
    {
        _participant.Shoot();
        if (DetectMapBoundsDistance() < 20f)
        {
            _participant.TurnLeft();
        }
        _participant.MoveForward();
    }

    public float DetectMapBoundsDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(_participant.Cannon.transform.position, transform.right, 100);
        if (hit.collider != null)
        {
            if (!hit.collider.tag.Equals("MapComponent"))
                return float.MaxValue;
            float distance = Vector3.Distance(transform.position, hit.point);
            return distance;
        }
        return float.MaxValue;
    }

    public void OnAssassinChanged(Participant participant)
    {
        _globalState = participant.gameObject == gameObject ? GLOBAL_STATE.ASSASSIN : GLOBAL_STATE.VICTIM;
    }

    public void OnPlayerDeath(Participant participant)
    {
        if (_globalState == GLOBAL_STATE.ASSASSIN)
        {
            _killingParticipant = null;
            _actualState = STATE.HUNTING;
        }
            
    }
}
