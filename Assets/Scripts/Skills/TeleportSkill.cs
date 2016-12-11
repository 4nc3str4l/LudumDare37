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
            _name = "Teleport";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                participant.SpeedEmmiter.CreateSomeParticles(participant.transform.position, 30, participant.PlayerColor);
                if (participant.GetComponent<InputHandler>() != null)
                    _participant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                else
                    _participant.transform.position = MapController.GenerateRandomPointInsideMap();
                _nextExecution = Time.time + _executionRate;

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
