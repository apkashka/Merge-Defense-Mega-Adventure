using UnityEngine;

namespace CodeBase.Systems
{
    public class SystemBehavior : MonoBehaviour
    {
        private bool _active;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                OnActive();
            }
        }

        protected virtual void OnActive()
        {
        }
    }
}