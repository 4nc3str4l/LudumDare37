using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class InvisibilitySkill : Skill
    {
        public InvisibilitySkill()
        {
            Name = "Invisibility";
        }

        public override bool Execute(Participant participant)
        {
            new InvisibilityBonus(participant);
            JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Speed, 0.5f);
            return true;
        }       

        protected override void UpdateCooldown(){}

        public override void Update()
        {
            UpdateCooldown();
        }
    }
}
