using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class InvencibilitySkill : Skill
    {
        public InvencibilitySkill(Participant player) : base(player)
        {
            _executionRate = 5f;
            _uiImage = null;
            _name = "Invencibility";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                InvencivibilityBonus speedBonus = new InvencivibilityBonus(participant);
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
