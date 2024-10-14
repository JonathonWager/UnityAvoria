using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DropBuffs{
    [CreateAssetMenu(menuName = "Drop/MagnetDrop")]
    public class MagnetDrop : DropBase
    {
         private GameObject dropedPotion;
        public override void Drop(GameObject enemy){
            dropedPotion = Instantiate(drop, enemy.transform.position, Quaternion.identity);
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(DeleteDrop());
        }
          private IEnumerator DeleteDrop()
        {
            yield return new WaitForSeconds(dropTime);
            Destroy(dropedPotion.gameObject);
        }
    }
}
