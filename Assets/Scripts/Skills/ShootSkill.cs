using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class ShootSkill : Skill
    {
        public ShootSkill(Participant player) : base(player)
        {
            _executionRate = 0.5f;
            _uiImage = null;
            Name = "Proton Cannon";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                participant.ShootCannon();
                _nextExecution = Time.time + _executionRate;
                _lastFired = Time.time;
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
;