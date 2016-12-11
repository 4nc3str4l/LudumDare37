using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class SprintSkill : Skill
    {
        public SprintSkill(Participant player) : base(player)
        {
            _executionRate = 2f;
            _uiImage = null;
            _name = "Sprint";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                SpeedBonus speedBonus = new SpeedBonus(participant);
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
