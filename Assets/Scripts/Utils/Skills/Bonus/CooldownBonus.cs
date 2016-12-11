using System;
using UnityEngine;

namespace Assets.Scripts.Skills.Bonus
{
    class CooldownBonus : PlayerBonus
    {
        float bonus = 4f;

        public CooldownBonus(Participant participant) : base(participant)
        {
            _timeOut = Time.time + 5f;
        }

        public override void ActivateBonus()
        {
            _participant.ReduceCooldowns(bonus);
        }

        public override void Expirate()
        {
            _participant.ResetCoooldownBonus();
            _participant.RemoveBonus(this);
        }
    }
}
