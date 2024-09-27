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

        void Start()
        {
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

            UpdateUI();
        }

        private IEnumerator StartCooldown(string abilityKey, float cooldownTime)
        {
            yield return new WaitForSeconds(cooldownTime);

            if (abilityKey == "Q")
            {
                IsQOnCooldown = false;
            }
            else if (abilityKey == "E")
            {
                IsEOnCooldown = false;
            }
        }

        private void UpdateUI()
        {
            uiUpdater ui = player.GetComponentInChildren<uiUpdater>();
            if (ui != null)
            {
                ui.UpdateAbilityUI(IsQOnCooldown, IsEOnCooldown);
            }
        }
    }
}
