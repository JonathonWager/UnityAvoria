using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using UnityEngine.UI;
public class BuildSelection : MonoBehaviour
{
    
    public Transform abilityPanelParent;
    public GameObject abilityEntryPrefab;

    AbilityManager abilityManager;
    bool loadedAbilities = false;
    public void LoadAbilties()
{
    if (!loadedAbilities)
    {
        AbilityBase[] allAbilities = Resources.LoadAll<AbilityBase>("Abilitys");
        for (int i = 0; i < allAbilities.Length; i++)
        {
            AbilityBase currentAbility = allAbilities[i]; // Store in local variable
            GameObject entry = Instantiate(abilityEntryPrefab, abilityPanelParent);
            entry.GetComponent<AbilityEntry>().SetUI(currentAbility.abilityName, currentAbility.icon);
            Button button = entry.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnAbilityEntrySelected(currentAbility, entry));
            }
        }
        loadedAbilities = true; // Mark abilities as loaded to prevent reloading
    }
}
    public void StartBuildSelection(){
        this.gameObject.SetActive(true);
        LoadAbilties();
    }
      private void OnAbilityEntrySelected(AbilityBase ability, GameObject panel)
    {
        if(abilityManager.currentQ == null){
            abilityManager.currentQ = ability;
        }else{
            abilityManager.currentE = ability;
            CloseBuildSelection();
        }
    }
    void CloseBuildSelection(){
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Start(){
        abilityManager = GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>(); 
    }
    
}
