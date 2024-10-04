using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DropBuffs{
     [CreateAssetMenu(menuName = "Drop/NukeDrop")]
    public class NukeDrop : DropBase
    {
        // Start is called before the first frame update
        public float nukeRange;
        private GameObject dropedPotion;
        public override void Drop(GameObject enemy){
             dropedPotion = Instantiate(drop, enemy.transform.position, Quaternion.identity);
             dropedPotion.GetComponent<NukeDropObject>().nukeExpanTime = nukeRange;
             enemy.GetComponent<MonoBehaviour>().StartCoroutine(DeleteDrop());
        }
        private IEnumerator DeleteDrop()
        {
            yield return new WaitForSeconds(dropTime);
            Destroy(dropedPotion.gameObject);
        }
    }

}
