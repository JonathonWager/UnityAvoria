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
        public int level;

        public abstract void Activate(GameObject player);
        public abstract void Deactivate(GameObject player);

        public abstract void ResetLevel();
          GameObject UI;
        protected void EndAbility()
        {
            // Trigger the end event
            OnAbilityEnd?.Invoke();
        }
        public void UpdateLevelUI(){
            Debug.Log("SERTTING UI");
            UI = GameObject.FindGameObjectWithTag("UI");
            UI.GetComponent<uiUpdater>().AbilityLevelUp(abilityName,level);
        }
    }
}
