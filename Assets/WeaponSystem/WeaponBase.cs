using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeaponsSystem
{
    public abstract class WeaponBase : ScriptableObject 
    {
        public string name;
        public string description;
        public Sprite icon;

        public int level;
        public int tier;
        public bool hasRightClick;
        public int price;
        public enum WeaponClass{
            Melee,
            Ranged
        }
        public WeaponClass weaponClass;
        public abstract void Attack(GameObject player);
    
    }
}