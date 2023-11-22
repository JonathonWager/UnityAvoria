using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV3 : MonoBehaviour
{
    // Reference to the player GameObject and the current weapon
    public GameObject player;
    public Weapon currentWeapon;

    // Array to store inventory weapons
    public Weapon[] InvWeapons = new Weapon[2];

    // Lists to store all weapons and potions
    List<Weapon> allWeapons = new List<Weapon>();
    List<Potion> allPotions = new List<Potion>();

    // Dictionary to store potions and their counts
    public Dictionary<string, int> potionDictionary = new Dictionary<string, int>();

    public Potion selectedPotion;
    private float storageFloat;

    private bool canB = true;

    // Method to initialize weapons based on a string array
    void makeWeapons(string[] weapons)
    {
        foreach (string w in weapons)
        {
            string[] atts = w.Split(',');

            // Create a new Weapon object and add it to the allWeapons list
            allWeapons.Add(new Weapon(int.Parse(atts[0]), atts[1], int.Parse(atts[2]), int.Parse(atts[3]), atts[4], atts[5], float.Parse(atts[6])));
        }
    }

    // Method to find a weapon by ID from the allWeapons list
    Weapon findWeaponFromAll(int ID)
    {
        foreach (Weapon w in allWeapons)
        {
            if (w.id == ID)
            {
                return w;
            }
        }
        return null;
    }

    // Getter method to retrieve the current weapon
    public Weapon getCurrentWeapon()
    {
        return currentWeapon;
    }

    // Method to add a weapon to the inventory
    public void addWeapon(int id)
    {
        if (findWeaponFromAll(id).isRanged == "M")
        {
            InvWeapons[0] = findWeaponFromAll(id);
        }
        else if (findWeaponFromAll(id).isRanged == "R")
        {
            InvWeapons[1] = findWeaponFromAll(id);
        }
    }

    // Method to update character stats when the current weapon changes
    public void currentWeaponChange()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.weaponStats(currentWeapon);
        cStats.currentSelectedWeapon = currentWeapon;
        cStats.rangeObject = Resources.Load(InvWeapons[1].projectilePrefabName) as GameObject;
    }

    // Method to initialize potions based on a string array
    void makePotions(string[] potions)
    {
        foreach (string p in potions)
        {
            string[] atts = p.Split(',');

            // Create a new Potion object and add it to the allPotions list
            allPotions.Add(new Potion(int.Parse(atts[0]), atts[1], atts[2], float.Parse(atts[3]), bool.Parse(atts[4]), float.Parse(atts[5])));
        }
    }

    // Method to initialize the potion dictionary with default counts
    void initPotionDictonary()
    {
        foreach (Potion p in allPotions)
        {   
            potionDictionary.Add(p.potionName, 0);
        }
    }

    Potion getPotion(string name){
        foreach(Potion p in allPotions){
            if(p.potionName == name){
                return p;
            }
        }
        return null;
    }
    // Method to add a potion to the potion dictionary and increase its count
    public void addPotion(int id)
    {
        foreach (var kvp in potionDictionary)
        {
            if (getPotion(kvp.Key).id == id)
            {
                // Increase the count of the potion in the dictionary
                potionDictionary[kvp.Key]++;
                return; // Optional: If you want to stop searching after finding the matching ID
            }
        }
    }

    // Method to use a potion from the potion dictionary and decrease its count
    public void usePotion(int id)
    {
        foreach (var kvp in potionDictionary)
        {
            if (getPotion(kvp.Key).id == id)
            {
                if(kvp.Value > 0){

                    if(getPotion(kvp.Key).statusType == "Hp"){
                        characterStats cStats = player.GetComponent<characterStats>();
                        cStats.setHp((int)(cStats.getHp() + (int)getPotion(kvp.Key).modifier));
                        potionDictionary[kvp.Key]--;
                    }
                    if(getPotion(kvp.Key).statusType == "Spd"){
                        canB = false;
                        playerMovement mStats = player.GetComponent<playerMovement>();
                        storageFloat = mStats.getSpeed();
                        mStats.setSpeed(storageFloat * getPotion(kvp.Key).modifier);
                        potionDictionary[kvp.Key]--;
                        Invoke("cancelSpeed",getPotion(kvp.Key).duration);
                    }
                    
                    
                    // Decrease the count of the potion in the dictionary
                   
                }
                
                return; // Optional: If you want to stop searching after finding the matching ID
            }
        }

        Debug.LogWarning("Potion with ID " + id + " not found in the dictionary.");
    }
    public void cancelSpeed(){
        playerMovement mStats = player.GetComponent<playerMovement>();
        mStats.setSpeed(storageFloat);
        canB = true;
    }
    public Dictionary<string, int> getpotionDictionary(){
        return potionDictionary;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Sample data for weapons
        string[] weapons = {
            "1,Great Sword,10,2,M,NA,0",
            "2,Long Sword,10,5,M,NA,0",
            "3,Basic Bow,30,5,R,Arrow,1"
        };

        // Initialize weapons and add them to the inventory
        makeWeapons(weapons);
        addWeapon(2);
        addWeapon(3);
        currentWeapon = InvWeapons[0];
        currentWeaponChange();

        // Sample data for potions
        string[] potions = {
            "1,Health Potion,Hp,25,false,0",
            "2,Stamina,Spd,1.5,false,5"
        };

        // Initialize potions and the potion dictionary
        makePotions(potions);
        initPotionDictonary();
        addPotion(1);
        addPotion(2);
    }

    // Update is called once per frame
    void Update()
    {
        // Switch to the first weapon in the inventory
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = InvWeapons[0];
            currentWeaponChange();
        }
        // Switch to the second weapon in the inventory
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = InvWeapons[1];
            currentWeaponChange();
        }
         if (Input.GetKeyDown(KeyCode.V))
        {
            usePotion(1);
        }
         if (Input.GetKeyDown(KeyCode.B))
        {
            if(canB){
                usePotion(2);
            }
            
        }
    }
}