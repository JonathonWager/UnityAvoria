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

    private bool isPrompt = false;
     private bool isActive = false;
     bool gotWeps = false;
    // Start is called before the first frame update
    void SetTeirLight(){
        foreach(Transform child in this.transform){
            if(child.gameObject.name == "Teir" + shopTier.ToString()){
                child.gameObject.SetActive(true);
            }
        }
    }
    void Update(){
        if(isActive){

             if (Input.GetKeyDown(KeyCode.F)){

                ToggleChildByName(UI, "ShopPrompt", false);
                ToggleChildByName(UI, "ShopBlur", false);
                ToggleChildByName(UI, "ShopPanel", false);
                foreach (GameObject pannel in shopPannels){
                    Destroy(pannel.gameObject);
                }
                isActive = false;
                //isPrompt = true;
                //showShopPrompt();
             }
        }
        if(isPrompt){
            if (Input.GetKeyDown(KeyCode.F)){
                ToggleChildByName(UI, "ShopPrompt", false);
                showShop();
                isPrompt = false;
                isActive = true;
            }
        }
    }
     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "character")
        {  
            player = other.gameObject;  
            showShopPrompt();
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {
            ToggleChildByName(UI, "ShopPrompt", false);
            ToggleChildByName(UI, "ShopBlur", false);
            ToggleChildByName(UI, "ShopPanel", false);
            foreach (GameObject pannel in shopPannels){
                Destroy(pannel.gameObject);
            }
            isPrompt = false;
            isActive = false;
        }
    }
    void showShopPrompt(){
        ToggleChildByName(UI, "ShopPrompt", true);
        isPrompt = true;
    }
     public void showShop(){
         
        shopPanelParent = UI.transform.Find("ShopPanel");
        if(!gotWeps){
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
            gotWeps =true;
        }
        
         foreach (WeaponBase wep in shopWeapons)
        {
             if (wep is ChargedRanged chargedRangedWeapon)
            {
                 AddShopEntry(chargedRangedWeapon.name,chargedRangedWeapon.icon, chargedRangedWeapon.price, chargedRangedWeapon.maxDamageBase, chargedRangedWeapon.maxRangeBase, chargedRangedWeapon.maxChargeBase, chargedRangedWeapon.knockBackBase, 1f,"Charged Ranged");
            }
            if (wep is BasicMelee BasicMelee)
            {
                AddShopEntry(BasicMelee.name,BasicMelee.icon, BasicMelee.price, BasicMelee.baseDamage, BasicMelee.baseRange, BasicMelee.attackCooldown, BasicMelee.knockBack, BasicMelee.attackAngle,"Melee");
            }
            if (wep is BasicRanged BasicRange)
            {
                  AddShopEntry(BasicRange.name,BasicRange.icon, BasicRange.price, BasicRange.baseDamage, BasicRange.baseRange, BasicRange.fireRate, BasicRange.knockBack, 1f,"Ranged");
            }
           
        }
        ToggleChildByName(UI, "ShopBlur", true);
        ToggleChildByName(UI, "ShopPanel", true);
    }
    public void AddShopEntry(string name, Sprite icon, int goldAmount, float damage, float range, float speed, float knockback,float atkArc,string type)
    {
        // Instantiate the prefab
        GameObject entry = Instantiate(shopEntryPrefab, shopPanelParent);
        shopPannels.Add(entry);
        // Get the ShopEntryUI component and set the values
        ShopEntryUI entryUI = entry.GetComponent<ShopEntryUI>();
        entryUI.SetUI(name,icon,goldAmount,damage,range,speed,knockback,atkArc,type);

        Button button = entry.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnShopEntryClicked(name, goldAmount, entry));
        }
    }
      private void OnShopEntryClicked(string name, int goldAmount, GameObject panel)
    {
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
        shopTier = teir;
        weaponAmount = wepCount;
       
         SetTeirLight();
       
    }
    public void Start(){
        //setStart(shopTier,weaponAmount);
    }

}
