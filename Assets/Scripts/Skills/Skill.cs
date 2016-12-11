using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Skills
{
    public abstract class Skill
    {
        protected Participant _participant;
        protected Image _uiImage;
        protected string _name;
        protected float _executionRate;
        protected float _nextExecution;

        public Skill(Participant player)
        {
            _participant = player;
        }
                
        public bool CanBeFired()
        {
            return Time.time > _nextExecution;
        }

        public abstract void Update();
        public abstract bool Execute(Participant participant);
        protected abstract void UpdateCooldown();

    }


}
