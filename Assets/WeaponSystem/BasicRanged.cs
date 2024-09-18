using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeaponsSystem{
    [CreateAssetMenu(menuName = "Weapon/BasicRanged")]
    public class BasicRanged : WeaponBase
    {
        [Header("Ranged Specific Properties(Base)")]
        public float baseFireRate;
        public int baseDamage;
        public float baseSpeed;
        public float baseRange;
        public int baseLevelInc;

        [Header("Ranged Specific Properties(Actual)")]
        public float fireRate;

        public string projectilePrefabName;
        private GameObject projectile;
        private bool canFire = true;

        [Header("Ranged Object Specific Properties")]
        public int damage;
        public float speed;
        public float range;

        public float knockBack;

        public float safteyDestoryTime;
        public override void ResetLevel(){
            damage = baseDamage;
            speed = baseSpeed;
            range = baseRange;
            fireRate = baseFireRate;
            level = 1;
            levelInc = baseLevelInc;
        }
        public override void Attack(GameObject player)
        {
            if (canFire)
            {
                GameObject instantProjectile = Instantiate(projectile, player.transform.position, Quaternion.identity);
                Debug.Log(instantProjectile.name);
                BasicProjectile proStats = instantProjectile.GetComponent<BasicProjectile>();
                proStats.dmg = damage;
                proStats.speed = speed;
                proStats.range = range;
                proStats.knockBack = knockBack;
                proStats.deleteTime = safteyDestoryTime;
                
                canFire = false;

                // Set a cooldown for firing
                player.GetComponent<MonoBehaviour>().StartCoroutine(Cooldown());
            }
        }
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(fireRate);
            FireReset();
        }
        void FireReset()
        {
            canFire = true;
        }
        // Start is called before the first frame update
        void OnEnable()
        {
            canFire = true;
            projectile = Resources.Load(projectilePrefabName) as GameObject;
        }

    }
}


