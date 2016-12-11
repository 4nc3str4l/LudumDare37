using System;
using UnityEngine;

namespace Assets.Scripts.Skills.Bonus
{
    class DamageBonus : PlayerBonus
    {
        float bonus = 2f;

        public DamageBonus(Participant participant) : base(participant)
        {
            _timeOut = Time.time + 4f;
        }

        public override void ActivateBonus()
        {
            UIController.Instance.CreateFloatingText("Damage x" + bonus.ToString(), Color.white, _participant.transform);
            _participant.DamageBonus = bonus;
        }

        public override void Expirate()
        {
            _participant.DamageBonus = 1;
            _participant.RemoveBonus(this);
        }
    }
}
