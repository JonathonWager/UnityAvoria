using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DropBuffs{
    [CreateAssetMenu(menuName = "Drop/SpeedDrop")]
    public class SpeedDrop : DropBase
    {
        public float speedBuff;
        public float duration;
        private GameObject dropedPotion;
        public override void Drop(GameObject enemy){
            dropedPotion = Instantiate(drop, enemy.transform.position, Quaternion.identity);
            dropedPotion.GetComponent<SpeedPotion>().speedBuff = speedBuff;
            dropedPotion.GetComponent<SpeedPotion>().duration = duration;
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(DeleteDrop());
        }
        private IEnumerator DeleteDrop()
        {
            yield return new WaitForSeconds(dropTime);
            Destroy(dropedPotion.gameObject);
        }
    }
}
