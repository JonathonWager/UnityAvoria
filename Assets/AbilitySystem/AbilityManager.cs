using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityManager : MonoBehaviour
    {
        public GameObject player;
        public AbilityBase currentQ;
        public AbilityBase currentE;

        // Change fields to properties
        public bool IsQOnCooldown { get; private set; } = false;
        public bool IsEOnCooldown { get; private set; } = false;
        GameObject UI;

        void Start()
        {
            UI = GameObject.FindGameObjectWithTag("UI");
            if (player == null)
            {
                player = transform.root.gameObject;
            }
            AbilityBase[] allAbilitys = Resources.LoadAll<AbilityBase>("Abilitys");

            // Loop through each weapon and call a function (e.g., ResetToDefault)
            foreach (AbilityBase ability in allAbilitys)
            {
                ability.ResetLevel(); // Assuming you have a ResetToDefault() function in WeaponBase
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentQ != null && !IsQOnCooldown)
            {
                currentQ.Activate(player);
                IsQOnCooldown = true;
                StartCoroutine(StartCooldown("Q", currentQ.cooldown));
            }

            if (Input.GetKeyDown(KeyCode.E) && currentE != null && !IsEOnCooldown)
            {
                currentE.Activate(player);
                IsEOnCooldown = true;
                StartCoroutine(StartCooldown("E", currentE.cooldown));
            }


        }

       private IEnumerator StartCooldown(string abilityKey, float cooldownTime)
        {
            float elapsedTime = 0f;  // Variable to track elapsed time

            while (elapsedTime < cooldownTime)
            {
               
                // Increment the elapsed time
                elapsedTime += Time.deltaTime;
                 UI.GetComponent<uiUpdater>().UpdateAbilityUI(abilityKey,cooldownTime, elapsedTime);
                
                // Wait for the next frame before continuing
                yield return null;
            }

            // Cooldown complete, reset the cooldown state for the specific ability
            if (abilityKey == "Q")
            {
                IsQOnCooldown = false;
            }
            else if (abilityKey == "E")
            {
                IsEOnCooldown = false;
            }
        }

        
    }
}
