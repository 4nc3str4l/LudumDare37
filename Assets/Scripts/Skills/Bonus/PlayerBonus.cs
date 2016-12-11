using UnityEngine;

namespace Assets.Scripts.Skills
{
    public abstract class PlayerBonus
    {
        protected float _timeOut;
        protected Participant _participant;

        public PlayerBonus(Participant participant)
        {
            _participant = participant;
            this.ActivateBonus();
            _participant.AddBonus(this);
        }

        public void CheckExpiration()
        {
            if(Time.time > _timeOut)
            {
                Expirate();
            }
        }

        public abstract void ActivateBonus();
        public abstract void Expirate();
    }
}
