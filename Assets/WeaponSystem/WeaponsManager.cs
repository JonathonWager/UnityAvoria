using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSystem
{
    public class WeaponsManager : MonoBehaviour
    {
        public GameObject player;
        public WeaponBase currentWeapon;



        private float holdStartTime;  // Stores the time when the button was pressed
        private bool isHolding = false;

        private float holdDuration;

        public void SetWeapon(WeaponBase weapon){
            currentWeapon = weapon;
        }
        // Start is called before the first frame update
        void Start()
        {
            if (player == null)
            {
                player = transform.root.gameObject;
            }
             WeaponBase[] allWeapons = Resources.LoadAll<WeaponBase>("Weapons");

            // Loop through each weapon and call a function (e.g., ResetToDefault)
            foreach (WeaponBase weapon in allWeapons)
            {
                weapon.ResetLevel(); // Assuming you have a ResetToDefault() function in WeaponBase
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(currentWeapon.hasCharge)
            {

                if (isHolding)
                {
                    holdDuration = Time.time - holdStartTime;
                    if (currentWeapon is ChargedRanged chargedRangedWeapon)
                    {
                        chargedRangedWeapon.chargeTime =holdDuration;
                    }
                }
                if (Input.GetMouseButtonDown(1)) // 1 is for right mouse button
                {
                    holdStartTime = Time.time;  // Record the time when the button was pressed
                    isHolding = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    holdDuration = 0f;
                    isHolding = false;
                    if (currentWeapon is ChargedRanged chargedRangedWeapon)
                    {
                        chargedRangedWeapon.chargeTime =holdDuration;
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (isHolding)
                    {
                          // Calculate how long the button was held
                        
                        isHolding = false;  // Reset the holding flag


                        holdDuration = 0f;
                        currentWeapon.Attack(player);
                        if (currentWeapon is ChargedRanged chargedRangedWeapon)
                        {
                            chargedRangedWeapon.chargeTime =holdDuration;
                        }
                    }
                }
            }else{
                if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
                {
                    currentWeapon.Attack(player);
                }
            }


            
        }
    }

}
