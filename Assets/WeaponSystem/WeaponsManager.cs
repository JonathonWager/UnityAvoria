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
        private bool isWaiting = false;
        private float holdDuration;

        public Texture2D customCursor, wait1, wait2, wait3, wait4, wait5; // Your custom cursor textures
        public Vector2 hotSpot = Vector2.zero; // The hotspot (click point) of the cursor

        public void SetWeapon(WeaponBase weapon)
        {
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
                weapon.ResetLevel(); // Assuming you have a ResetLevel() function in WeaponBase
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(isWaiting){
                UpdateCooldownCursor();
            }
            if (currentWeapon != null) // Ensure currentWeapon is not null
            {
                if (currentWeapon.hasCharge)
                {
                    if (isHolding)
                    {
                        holdDuration = Time.time - holdStartTime;
                        if (currentWeapon is ChargedRanged chargedRangedWeapon)
                        {
                            chargedRangedWeapon.chargeTime = holdDuration;
                        }
                    }

                    if (Input.GetMouseButtonDown(0)) // Right mouse button
                    {
                        holdStartTime = Time.time;  // Record the time when the button was pressed
                        isHolding = true;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (isHolding)
                        {
                            isHolding = false;  // Reset the holding flag
                            holdDuration = 0f;
                            currentWeapon.Attack(player);
                        }
                        holdDuration = 0f;
                        isHolding = false;
                        if (currentWeapon is ChargedRanged chargedRangedWeapon)
                        {
                            chargedRangedWeapon.chargeTime = holdDuration;
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0)) // Left mouse button
                    {
                        // Check if currentWeapon is BasicMelee
                        
                            Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto);

                            // Check if the melee weapon can attack

                            currentWeapon.Attack(player); // Attack using BasicMelee
                            
                            if(!isWaiting)
                                isWaiting = true;
                            
                        }
                    
                }
            }
            else
            {
                Debug.LogWarning("No current weapon assigned.");
            }
        }
        void ResetCursor(){
            Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto);
        }
        private void UpdateCooldownCursor()
        {
            float percentCooldown = 0f;
            bool canAttack = true;
            if (currentWeapon is BasicMelee meleeWeapon)
            {
                percentCooldown = meleeWeapon.cooldownElapsedTime / meleeWeapon.attackCooldown;
                canAttack = meleeWeapon.canAttack;
            }
            if(currentWeapon is BasicRanged basicRangedWeapon){
                percentCooldown = basicRangedWeapon.cooldownElapsedTime / basicRangedWeapon.fireRate;
                canAttack = basicRangedWeapon.canFire;
            }
            if(!canAttack){          
                if (percentCooldown >= 1)
                {
                    Cursor.SetCursor(wait5, hotSpot, CursorMode.Auto);
                    if(isWaiting){
                        Invoke("ResetCursor",0.5f);
                    }

                    isWaiting = false;

                    
                }
                else if (percentCooldown >= 0.75f)
                {
                    Cursor.SetCursor(wait4, hotSpot, CursorMode.Auto);
                }
                else if (percentCooldown >= 0.5f)
                {
                    Cursor.SetCursor(wait3, hotSpot, CursorMode.Auto);
                }
                else if (percentCooldown >= 0.25f)
                {
                    Cursor.SetCursor(wait2, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(wait1, hotSpot, CursorMode.Auto);
                }
            }
        }
    }
}
