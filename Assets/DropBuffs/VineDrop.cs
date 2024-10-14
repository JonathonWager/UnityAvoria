using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DropBuffs{
    [CreateAssetMenu(menuName = "Drop/VineDrop")]
    public class VineDrop : DropBase
    {
        public float vineSlowAmount;
        public float vineDuration;
        public float vineRange;
        private GameObject dropedPotion;
        public override void Drop(GameObject enemy){
            dropedPotion = Instantiate(drop, enemy.transform.position, Quaternion.identity);
            dropedPotion.GetComponent<VinePotion>().slowAmount = vineSlowAmount;
            dropedPotion.GetComponent<VinePotion>().slowDuration = vineDuration;
            dropedPotion.GetComponent<VinePotion>().vineRange = vineRange;
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(DeleteDrop());
        }
        private IEnumerator DeleteDrop()
        {
            yield return new WaitForSeconds(dropTime);
            Destroy(dropedPotion.gameObject);
        }
    }
}
