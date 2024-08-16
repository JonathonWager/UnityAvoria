using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityManager : MonoBehaviour
    {
        public GameObject player;
        public AbilityBase currentQ;
        public AbilityBase currentE;

        private bool isQOnCooldown = false;
        private bool isEOnCooldown = false;

        void Start()
        {
            if (player == null)
            {
                player = transform.root.gameObject;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentQ != null && !isQOnCooldown)
            {
                currentQ.Activate(player);
                isQOnCooldown = true;
                StartCoroutine(StartCooldown("Q", currentQ.cooldown));
            }

            if (Input.GetKeyDown(KeyCode.E) && currentE != null && !isEOnCooldown)
            {
                currentE.Activate(player);
                isEOnCooldown = true;
                StartCoroutine(StartCooldown("E", currentE.cooldown));
            }

            UpdateUI();
        }

        private IEnumerator StartCooldown(string abilityKey, float cooldownTime)
        {
            yield return new WaitForSeconds(cooldownTime);

            if (abilityKey == "Q")
            {
                isQOnCooldown = false;
            }
            else if (abilityKey == "E")
            {
                isEOnCooldown = false;
            }

            Debug.Log($"{abilityKey} ability is ready to use again.");
        }

        private void UpdateUI()
        {
            uiUpdater ui = player.GetComponentInChildren<uiUpdater>();
            if (ui != null)
            {
                ui.UpdateAbilityUI(isQOnCooldown, isEOnCooldown);
            }
        }
    }
}
