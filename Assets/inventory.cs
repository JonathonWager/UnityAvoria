using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventory : MonoBehaviour
{
    public GameObject player;
    public weapon currentWeapon;
    public List<weapon> InvWeapons = new List<weapon>();

    List<weapon> allWeapons = new List<weapon>();

    void makeWeapons(string[] weapons)
    {
        foreach(string w in weapons){
            string[] atts = w.Split(',');
            allWeapons.Add(new weapon(int.Parse(atts[0]),atts[1],atts[2],int.Parse(atts[3]),int.Parse(atts[4])));
        }
    }
    weapon findWeaponFromAll(int ID){
        foreach(weapon w in allWeapons){
            if(w.getId() == ID){
                return w;
            }
        }
        return null;
    }
    weapon findWeaponFromInv(int ID){
        foreach(weapon w in allWeapons){
            if(w.getId() == ID){
                return w;
            }
        }
        return null;
    }
    public weapon getCurrentWeapon(){
        return currentWeapon;
    }
    public void addWeapon(int id){
         InvWeapons.Add(findWeaponFromAll(id));
    }
    // Start is called before the first frame update
    public void currentWeaponChange(){
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.weaponStats(currentWeapon);
    }

    public weapon getWeapon(){
        return currentWeapon;
    }
    void Start()
    {
        string[] weapons = {
            "1,Melee,Great Sword,10,2"
        };
        makeWeapons(weapons);
        addWeapon(1);
        currentWeapon = findWeaponFromInv(1);
        currentWeaponChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
