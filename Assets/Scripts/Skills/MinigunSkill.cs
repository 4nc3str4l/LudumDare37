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
            _name = "Minigun";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                participant.ShootMinigun();
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
;