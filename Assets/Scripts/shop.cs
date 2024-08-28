using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shop : MonoBehaviour
{
    public int shopTier = 1;
    public int weaponAmount = 2;
    List<Weapon> shopWeapons = new List<Weapon>();

    GameObject player;
    public GameObject UI;

    public GameObject shopEntryPrefab;
    public Transform shopPanelParent;

    public void ToggleChildByName(GameObject parent, string childName, bool isActive)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"Child with name '{childName}' not found.");
        }
    }
    public void AddShopEntry(string name, Sprite icon, int goldAmount)
    {
        // Instantiate the prefab
        GameObject entry = Instantiate(shopEntryPrefab, shopPanelParent);

        // Get the ShopEntryUI component and set the values
        ShopEntryUI entryUI = entry.GetComponent<ShopEntryUI>();
        entryUI.SetWeaponName(name);
        entryUI.SetWeaponIcon(icon);
        entryUI.SetGoldAmount(goldAmount);
        Button button = entry.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnShopEntryClicked(name, goldAmount, entry));
        }
    }
    private void OnShopEntryClicked(string name, int goldAmount, GameObject panel)
    {
        // Handle what happens when a shop entry is clicked
        Debug.Log("Clicked on: " + name + " for " + goldAmount + " gold");
        characterStats cStats = player.GetComponent<characterStats>();
        if(cStats.gold > goldAmount){
            cStats.gold = cStats.gold - goldAmount;
            
            InventoryV3 inventory = player.GetComponentInChildren<InventoryV3>();
            if (inventory != null)
            {
                inventory.swapOutWeapon(shopWeapons.Find(weapon => weapon.weaponName == name));
                shopWeapons.RemoveAll(weapon => weapon.weaponName == name);
                Debug.Log("Showing second shop?");
                Destroy(panel);
            }
        }
        // Perform the desired actions, such as buying the item or showing more details
    }
    public void showShop(){
        ToggleChildByName(UI, "ShopBlur", true);
        ToggleChildByName(UI, "ShopPanel", true);
    }
     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "character")
        {  
            player = other.gameObject;  
            showShop();
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {
            ToggleChildByName(UI, "ShopBlur", false);
            ToggleChildByName(UI, "ShopPanel", false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
         // No need to get the WeaponDatabase component since GetWeaponById is static
        List<Weapon> allWeapons = WeaponDatabase.getAllWeapons();
        for (int i = 0; i < weaponAmount; i++)
        {
            // Use the class name to access the static method
            Weapon w = allWeapons[Random.Range(0, allWeapons.Count)];
            if(w.tier == shopTier){
                shopWeapons.Add(w);
                allWeapons.Remove(w);
            }else{
                i--;
            }           
        }
        foreach (Weapon wep in shopWeapons)
        {
            Debug.Log("Printing WEp");
            AddShopEntry(wep.weaponName, Resources.Load<Sprite>("WeaponIcons/"+ wep.weaponName), wep.price);
        }
        // Loop through the weapons in the shop and print their names
        foreach (Weapon w in shopWeapons)
        {
            Debug.Log(w.weaponName);
        }
    }
}
