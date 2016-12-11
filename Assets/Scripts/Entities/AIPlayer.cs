using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour {

    public float MAX_KILL_DISTANCE;

    public enum GLOBAL_STATE { ASSASSIN, VICTIM }
    public enum STATE { GOING_TO_SPOT, HUNTING, KILLING }
    private Participant _participant;

    private GLOBAL_STATE _globalState = GLOBAL_STATE.VICTIM;
    private STATE _actualState = STATE.GOING_TO_SPOT;

    private Participant _killingParticipant;

    private float _changeRandomSpotTime;
    private Vector3 _destination;

    private int _dificulty = 1;
    

	// Use this for initialization
	void Start () {
        _dificulty = PlayerPrefs.GetInt("Difficulty");
        MAX_KILL_DISTANCE = 4 + 2 * _dificulty;
        _participant = GetComponent<Participant>();
        MapController.OnAssasinChange += OnAssassinChanged;
        Participant.OnPlayerDead += OnPlayerDeath;
        Participant.OnPlayerHurt += OnPlayerHurt;
    }
	
    public void OnPlayerHurt(Participant participant, float ammount)
    {
        if (participant.Name != _participant.Name) return;
        if (ammount < 0) return;

        if (_dificulty <= 1) return;
        else if(_dificulty > 1 && _dificulty < 4)
        {
            participant.PrincipalSkill();
            participant.SecondarySkill();
        }
        else
        {
            if (!participant.SpecialSkill())
            {
                participant.PrincipalSkill();
                participant.SecondarySkill();

            }
        }

        _destination = MapController.GenerateRandomPointInsideMap();
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
        if (_dificulty == 5)
        {
            _participant.SpecialSkill();
        }
        if (closestDistanceParticipant.Key < MAX_KILL_DISTANCE && closestDistanceParticipant.Value.isVisible)
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
            _participant.MoveForward();
        }
    }

    public void Killing()
    {
        if (_killingParticipant == null || !_killingParticipant.isVisible)
        {
            _actualState = STATE.HUNTING;
            return;
        }
            
        if(Vector3.Distance(transform.position, _killingParticipant.transform.position) > MAX_KILL_DISTANCE)
        {
            _killingParticipant = null;
            _actualState = STATE.HUNTING;
        }
        else
        {
            var dir = Vector3.zero;
            float distanceToTarget = Vector3.Distance(_killingParticipant.transform.position, transform.position);
            if (distanceToTarget < 2)
            {
                dir = (_killingParticipant.transform.position - transform.position);
            }
            else
            {
                if (_dificulty <= 1)
                {
                    dir = (_killingParticipant.transform.position + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), transform.position.z) + _killingParticipant.transform.right) - transform.position;
                }
                else if (_dificulty > 1 && _dificulty < 4)
                {
                    dir = (_killingParticipant.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), transform.position.z) + _killingParticipant.transform.right) - transform.position;
                }
                else if(_dificulty == 4)
                {
                    dir = (_killingParticipant.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), transform.position.z) + _killingParticipant.transform.right) - transform.position;
                }
                else
                {
                    dir = (_killingParticipant.transform.position + new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), transform.position.z) + _killingParticipant.transform.right) - transform.position;
                }
            }


            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));

            if (distanceToTarget > 1)
            {
                _participant.MoveForward();
            }

            if (_killingParticipant.isVisible)
            {
                _participant.PrincipalSkill();
                _participant.SecondarySkill();
            }
            Hunting();
        }
    }

    public void VictimBehaivour()
    {
        if(Vector3.Distance(transform.position, _destination) < 1f || _changeRandomSpotTime < Time.time)
        {
            ChangeDestinationAndLookAtIt();
        }

        _participant.MoveForward();
    }

    public void ChangeDestinationAndLookAtIt()
    {
        _destination = MapController.GenerateRandomPointInsideMap();
        _changeRandomSpotTime = Time.time + Random.Range(1, 3);
        var dir = _destination - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
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
        if (participant == null)
        {
            _globalState = GLOBAL_STATE.VICTIM;
            return;
        }
        _globalState = participant.Name == _participant.Name ? GLOBAL_STATE.ASSASSIN : GLOBAL_STATE.VICTIM;
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
