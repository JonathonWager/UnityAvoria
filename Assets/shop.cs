using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    public int shopTier = 1;
    public int weaponAmount = 2;
    List<Weapon> shopWeapons = new List<Weapon>();

    public bool shopActive = false;

    uiUpdater script;
    GameObject player;

    public void showShop(){
          Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                GameObject uiObject = mainCamera.transform.Find("UiReloadedV1")?.gameObject;
                if (uiObject != null)
                {
                    script = uiObject.GetComponent<uiUpdater>();
                    if (script != null)
                    {
                        List<string> shopItems = new List<string>();
                        foreach (Weapon wep in shopWeapons)
                        {
                            shopItems.Add(wep.weaponName + " " + wep.price + " Gold");
                        }
                        script.showShop(shopItems);
                    }
                    else
                    {
                        Debug.LogWarning("uiUpdater script not found on UiReloadedV1.");
                    }
                }
                else
                {
                    Debug.LogWarning("UiReloadedV1 GameObject not found as a child of the main camera.");
                }
            }
            else
            {
                Debug.LogWarning("Main camera not found.");
            }
    }
     public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered trigger");

        if (other.gameObject.tag == "character")
        {
            player = other.gameObject;
            shopActive = true;
            Debug.Log("Character detected");
            showShop();
            // Get the main camera and find the UiReloadedV1 GameObject
          
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {
            shopActive = false;
            script.disableShop();
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

        // Loop through the weapons in the shop and print their names
        foreach (Weapon w in shopWeapons)
        {
            Debug.Log(w.weaponName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shopActive){
            for (int i = 1; i <= shopWeapons.Count; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    Debug.Log("Player pressed key: " + i);
                    characterStats cStats = player.GetComponent<characterStats>();
                    if(cStats.gold > shopWeapons[i-1].price){
                        cStats.gold = cStats.gold - shopWeapons[i-1].price;
                        
                        InventoryV3 inventory = player.GetComponentInChildren<InventoryV3>();
                        if (inventory != null)
                        {
                            // Now you can call methods on the InventoryV3 script
                            inventory.swapOutWeapon(shopWeapons[i-1]);
                            shopWeapons.RemoveAt(i-1);
                            showShop();
                        }
                    }
                    // Access the InventoryV3 script directly by child index
                    
                }
            }
        }
    }
}
