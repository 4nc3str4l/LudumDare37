using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class MinigunSkill : Skill
    {
        public MinigunSkill(Participant player) : base(player)
        {
            _executionRate = 0.1f;
            _uiImage = null;
            Name = "Minigun";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                participant.ShootMinigun();
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