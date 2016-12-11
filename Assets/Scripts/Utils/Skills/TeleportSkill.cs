using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class TeleportSkill : Skill
    {
        public TeleportSkill(Participant player) : base(player)
        {
            _executionRate = 10f;
            _uiImage = null;
            Name = "Teleport";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                participant.SpeedEmmiter.CreateSomeParticles(participant.transform.position, 30, participant.PlayerColor, 0.3f);
                if (participant.GetComponent<InputHandler>() != null)
                    _participant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                else
                {
                    if(PlayerPrefs.GetInt("Difficulty") < 5)
                    {
                        _participant.transform.position = MapController.GenerateRandomPointInsideMap();
                    }
                    else
                    {
                        if(InputHandler.Instance != null)
                        {
                            if(InputHandler.Instance.participant != null)
                            {
                                _participant.transform.position = InputHandler.Instance.participant.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                            }
                            else
                            {
                                _participant.transform.position = MapController.GenerateRandomPointInsideMap();
                            }
                        }
                        else
                        {
                            _participant.transform.position = MapController.GenerateRandomPointInsideMap();
                        }
                    }
                    
                }
                   
                _nextExecution = Time.time + _executionRate;
                _lastFired = Time.time;
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Teleport, 1f);
                return true;
            }

            return false;
        }

        protected override void UpdateCooldown()
        {

        }

        public override void Update()
        {
            UpdateCooldown();
        }
    }
}
