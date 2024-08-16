using UnityEngine;
using System;

namespace AbilitySystem
{
    public abstract class AbilityBase : ScriptableObject
    {
        public string abilityName;
        public string description;
        public Sprite icon;
        public float cooldown;  // Cooldown duration

        public Action OnAbilityEnd;  // Event triggered when the ability ends

        public abstract void Activate(GameObject player);
        public abstract void Deactivate(GameObject player);

        protected void EndAbility()
        {
            // Trigger the end event
            OnAbilityEnd?.Invoke();
        }
    }
}
