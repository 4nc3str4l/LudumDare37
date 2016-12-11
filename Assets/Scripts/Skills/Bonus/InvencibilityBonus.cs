using System;
using UnityEngine;

namespace Assets.Scripts.Skills.Bonus
{
    class InvencivibilityBonus : PlayerBonus
    {

        public InvencivibilityBonus(Participant participant) : base(participant)
        {
            _timeOut = Time.time + 2f;
            this.ActivateBonus();
        }

        public override void ActivateBonus()
        {
            Debug.Log("Invencible");
            _participant.SetInvencible(true);
        }

        public override void Expirate()
        {
            _participant.SetInvencible(false);
            _participant.RemoveBonus(this);
        }
    }
}
