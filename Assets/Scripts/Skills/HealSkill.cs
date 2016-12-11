using UnityEngine.UI;
using UnityEngine;


namespace Assets.Scripts.Skills
{
    class HealSkill : Skill
    {
        float _healAmmount;
        public HealSkill()
        {
            _healAmmount = Random.Range(10, 25);
        }

        public override bool Execute(Participant participant)
        {
            participant.Heal(_healAmmount);
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
