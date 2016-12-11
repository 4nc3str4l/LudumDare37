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
            Name = "Sprint";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                new SpeedBonus(participant);
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Speed, 0.5f);
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
