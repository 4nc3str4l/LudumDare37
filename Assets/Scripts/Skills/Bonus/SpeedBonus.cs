using System;
using UnityEngine;

namespace Assets.Scripts.Skills.Bonus
{
    class SpeedBonus : PlayerBonus
    {
        float bonus = 5f;

        public SpeedBonus(Participant participant) : base(participant)
        {
            _timeOut = Time.time + 0.4f;
        }

        public override void ActivateBonus()
        {
            _participant.ChangeSpeed(Participant.SPEED + bonus);
        }

        public override void Expirate()
        {
            _participant.RestartSpeed();
            _participant.RemoveBonus(this);
        }
    }
}
