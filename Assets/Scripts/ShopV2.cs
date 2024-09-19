using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsSystem;
using UnityEngine.UI;
public class ShopV2 : MonoBehaviour
{
    public int shopTier = 1;
    public int weaponAmount = 2;
    List<WeaponBase> shopWeapons = new List<WeaponBase>();
    GameObject player;

    public GameObject UI;

    public GameObject shopEntryPrefab;
    public Transform shopPanelParent;

    public List<GameObject> shopPannels =  new List<GameObject>();
    // Start is called before the first frame update

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
            foreach (GameObject pannel in shopPannels){
                Destroy(pannel.gameObject);
            }
        }
    }
     public void showShop(){
         foreach (WeaponBase wep in shopWeapons)
        {
            AddShopEntry(wep.name,wep.icon, wep.price);
        }
        ToggleChildByName(UI, "ShopBlur", true);
        ToggleChildByName(UI, "ShopPanel", true);
    }
    public void AddShopEntry(string name, Sprite icon, int goldAmount)
    {
        // Instantiate the prefab
        GameObject entry = Instantiate(shopEntryPrefab, shopPanelParent);
        shopPannels.Add(entry);
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
        Debug.Log(name  + " " + goldAmount);
        characterStats cStats = player.GetComponent<characterStats>();
        if(cStats.gold > goldAmount){
            cStats.gold = cStats.gold - goldAmount;
            
            InventoryV4 inventory = player.GetComponentInChildren<InventoryV4>();
            if (inventory != null)
            {
                inventory.swapOutWeapon(shopWeapons.Find(weapon => weapon.name == name));
                shopWeapons.RemoveAll(weapon => weapon.name == name);
                Destroy(panel);
            }
        }
        // Perform the desired actions, such as buying the item or showing more details
    }
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
    public void setStart(int teir,int wepCount){
        UI = GameObject.FindGameObjectWithTag("UI");
        shopPanelParent = UI.transform.Find("ShopPanel");
        shopTier = teir;
        weaponAmount = wepCount;
        Debug.Log(this.gameObject.name + " making "+ wepCount+ " pannels");
        WeaponBase[] allWeaponsArray  = Resources.LoadAll<WeaponBase>("Weapons");
        List<WeaponBase> allWeapons = new List<WeaponBase>();
        for(int i = 0; i <  allWeaponsArray.Length; i++){
            if(allWeaponsArray[i].tier == shopTier){
                allWeapons.Add(allWeaponsArray[i]);
            }
        } 
        if(allWeapons.Count < weaponAmount){
            weaponAmount  = allWeapons.Count;
        }
        for (int i = 0; i < weaponAmount; i++)
        {
            int index = Random.Range(0, allWeapons.Count);
            shopWeapons.Add(allWeapons[index]);
            allWeapons.RemoveAt(index);
        }

       
    }
    public void Start(){
        //setStart(shopTier,weaponAmount);
    }

}
