using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsSystem;
public class InventoryV4 : MonoBehaviour
{
    // Reference to the player GameObject and the current weapon
    public GameObject player;
    public WeaponBase currentWeapon;

    // Array to store inventory weapons
    public WeaponBase[] InvWeapons = new WeaponBase[2];

    public GameObject WeaponsSystem;
    // Method to add a weapon to the inventory
    public void addWeapon(WeaponBase incomingWeapon)
    {
        if (incomingWeapon != null)
        {
            if(incomingWeapon.weaponClass == WeaponBase.WeaponClass.Melee){
                 InvWeapons[0] = incomingWeapon;
            }
           if(incomingWeapon.weaponClass == WeaponBase.WeaponClass.Ranged){
                 InvWeapons[1] = incomingWeapon;
            }
        }
        
    }
    public void WeaponSwap(){
        WeaponsSystem.GetComponent<WeaponsManager>().SetWeapon(currentWeapon);
    }

    // Start is called before the first frame update
    void Start()
    {
        WeaponsSystem = GameObject.FindGameObjectWithTag("WeaponsSystem");
        // Initialize weapons and add them to the inventory
        addWeapon(Resources.Load("Weapons/BasicSword") as WeaponBase);
        addWeapon(Resources.Load("Weapons/BasicBow") as WeaponBase);
        currentWeapon = InvWeapons[0];
        WeaponSwap();
    }
    public void swapOutWeapon(WeaponBase weapon){
        if(weapon.weaponClass == WeaponBase.WeaponClass.Melee){
            InvWeapons[0] = weapon;

        }
        if(weapon.weaponClass == WeaponBase.WeaponClass.Ranged){
            InvWeapons[1] = weapon;
        }
        currentWeapon = weapon;
        WeaponSwap();
    }
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentWeapon != InvWeapons[0]){
                currentWeapon = InvWeapons[0];
                WeaponSwap();
            }
            
        }
        // Switch to the second weapon in the inventory
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(currentWeapon != InvWeapons[1]){
                currentWeapon = InvWeapons[1];
                WeaponSwap();
            }
            
        }
    }
}
