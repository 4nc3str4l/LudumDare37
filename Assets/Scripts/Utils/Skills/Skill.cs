using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Skills
{
    public abstract class Skill
    {
        protected Participant _participant;
        protected Image _uiImage;
        public string Name;
        protected float _executionRate;
        protected float _nextExecution;
        protected float _lastFired;


        public Skill()
        {
        }

        public Skill(Participant player)
        {
            _participant = player;
        }
                
        public bool CanBeFired()
        {
            return Time.time > _nextExecution;
        }

        public float GetFillAmount()
        {
            return (Time.time - _lastFired) / (_nextExecution - _lastFired);
        }

        public abstract void Update();
        public abstract bool Execute(Participant participant);
        protected abstract void UpdateCooldown();

    }


}
