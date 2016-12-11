using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class DamageSkill : Skill
    {
        public DamageSkill()
        {
            Name = "Damage Skill";
        }

        public override bool Execute(Participant participant)
        {
            if (participant.IsAssasin)
                new DamageBonus(participant);
            else
                new SpeedBonus(participant);

            JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Speed, 0.5f);
            return true;
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
