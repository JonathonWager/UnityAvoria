using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSystem
{
    public class WeaponsManager : MonoBehaviour
    {
        public GameObject player;
        public WeaponBase currentWeapon;

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
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
            {
                currentWeapon.Attack(player);
            }
        }
    }

}
