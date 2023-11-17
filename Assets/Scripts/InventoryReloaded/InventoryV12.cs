using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV12 : MonoBehaviour
{
    public GameObject player;
     public Weapon currentWeapon;

    public List<Weapon> InvWeapons = new List<Weapon>();

    List<Weapon> allWeapons = new List<Weapon>();

    List<Potion> allPotions = new List<Potion>();

    List<Potion> invPotions = new List<Potion>();


    // Start is called before the first frame update
    void makeWeapons(string[] weapons)
    {
        foreach(string w in weapons){
            string[] atts = w.Split(',');
            allWeapons.Add(new Weapon(int.Parse(atts[0]),atts[1],int.Parse(atts[2]),int.Parse(atts[3])));
        }
    }
    Weapon findWeaponFromAll(int ID){
        foreach(Weapon w in allWeapons){
            if(w.getId() == ID){
                return w;
            }
        }
        return null;
    }
    Weapon findWeaponFromInv(int ID){
        foreach(Weapon w in allWeapons){
            if(w.getId() == ID){
                return w;
            }
        }
        return null;
    }
    public Weapon getCurrentWeapon(){
        return currentWeapon;
    }
    public void addWeapon(int id){
         InvWeapons.Add(findWeaponFromAll(id));
    }

    public void currentWeaponChange(){
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.weaponStats(currentWeapon);
    }
    void Start()
    {
        string[] weapons = {
            "1,Great Sword,10,2"
        };
        makeWeapons(weapons);
        addWeapon(1);
        Debug.Log("Weapon Start");
        currentWeapon = findWeaponFromInv(1);
        currentWeaponChange();
    }

    // Update is called once per frame
}
