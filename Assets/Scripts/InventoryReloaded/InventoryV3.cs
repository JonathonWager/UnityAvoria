using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV3 : MonoBehaviour
{
    public GameObject player;
    public Weapon currentWeapon;

    public Weapon[] InvWeapons= new Weapon[2];


    List<Weapon> allWeapons = new List<Weapon>();

    List<Potion> allPotions = new List<Potion>();

    List<Potion> invPotions = new List<Potion>();


    // Start is called before the first frame update
    void makeWeapons(string[] weapons)
    {
        foreach(string w in weapons){
            string[] atts = w.Split(',');
           
            allWeapons.Add(new Weapon(int.Parse(atts[0]),atts[1],int.Parse(atts[2]),int.Parse(atts[3]),atts[4],atts[5],float.Parse(atts[6])));
        }
    }
    Weapon findWeaponFromAll(int ID){
        foreach(Weapon w in allWeapons){
            if(w.id == ID){
                return w;
            }
        }
        return null;
    }

    public Weapon getCurrentWeapon(){
        return currentWeapon;
    }
    public void addWeapon(int id){
        //Debug.Log("Weapon add " + id);
        if(findWeaponFromAll(id).isRanged == "M"){  
            InvWeapons[0] = findWeaponFromAll(id);
            //Debug.Log(findWeaponFromAll(id).name);
        }else if(findWeaponFromAll(id).isRanged == "R"){
            InvWeapons[1] = findWeaponFromAll(id);
        }
    }

    public void currentWeaponChange(){
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.weaponStats(currentWeapon);
        cStats.currentSelectedWeapon = currentWeapon;
        Debug.Log(InvWeapons[1].projectilePrefabName);
        cStats.rangeObject = Resources.Load(InvWeapons[1].projectilePrefabName) as GameObject;
        
    }
    void Start()
    {
        string[] weapons = {
            "1,Great Sword,10,2,M,NA,0",
            "2,Long Sword,10,5,M,NA,0",
            "3,Basic Bow,30,5,R,Arrow,1"
        };
        makeWeapons(weapons);
        addWeapon(2);
         addWeapon(3);
        Debug.Log("Weapon Start");
        currentWeapon = InvWeapons[0];
        currentWeaponChange();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = InvWeapons[0];
            currentWeaponChange();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

                currentWeapon = InvWeapons[1];
                currentWeaponChange();
            
           
        }
    }
}
