using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DropBuffs{
    [CreateAssetMenu(menuName = "Drop/HealthDrop")]
    public class HealthDrop : DropBase
    {
        public int heal;
        public override void Drop(GameObject enemy){
            GameObject dropedPotion = Instantiate(drop, enemy.transform.position, Quaternion.identity);
            dropedPotion.GetComponent<HealthPotion>().heal = heal;
        }

    }
}

