using System;
using UnityEngine;

namespace Assets.Scripts.Skills.Bonus
{
    class InvisibilityBonus : PlayerBonus
    {

        public InvisibilityBonus(Participant participant) : base(participant)
        {
            _timeOut = Time.time + 10f;
        }

        public override void ActivateBonus()
        {
            _participant.SetVisible(false);
        }

        public override void Expirate()
        {
            _participant.SetVisible(true);
            _participant.RemoveBonus(this);
        }
    }
}
