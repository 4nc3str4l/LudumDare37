using UnityEngine.UI;
using UnityEngine;

using Assets.Scripts.Skills.Bonus;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    class FasterCooldownSkill : Skill
    {
        public FasterCooldownSkill()
        {
            Name = "Faster Cooldown";
        }

        public override bool Execute(Participant participant)
        {
            if (CanBeFired())
            {
                new CooldownBonus(participant);
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Speed, 0.5f);
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
