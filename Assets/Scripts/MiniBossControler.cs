using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniBossControler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject miniBoss;
    GameObject UI;
    bool isPrompt = false;
    bool readyToSpawn = false;
    WaveManager waveManager;
    int waveStorage = 0;
     private Animator animator;
     public GameObject bossBarrier;
     BossBlockade bossBlockade;
    public void ToggleChildByName(GameObject parent, string childName, bool isActive, string miniBossName)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(isActive);
            Transform bossNameText = childTransform.Find("MiniBossName");
            if(bossNameText != null){
                bossNameText.gameObject.GetComponent<Text>().text = miniBossName;
            }
        }
        else
        {
            Debug.LogWarning($"Child with name '{childName}' not found.");
        }
    }

     public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            if(!isPrompt && !readyToSpawn){
                isPrompt = true;
                ToggleChildByName(UI,"MiniBossPrompt", true, miniBoss.gameObject.name);
            }
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {  
            isPrompt = false;
            ToggleChildByName(UI,"MiniBossPrompt", false, miniBoss.gameObject.name);
        }
    }
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        animator = GetComponent<Animator>();
        bossBlockade = bossBarrier.GetComponent<BossBlockade>();
    }
    void spawnMiniBoss(){
        GameObject miniBossSpawn = Instantiate(miniBoss, this.transform.position, Quaternion.identity);
        isPrompt = false;
        ToggleChildByName(UI,"MiniBossPrompt", false, miniBoss.gameObject.name);
        bossBlockade.miniBossCount--;
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(isPrompt && !readyToSpawn){
            if (Input.GetKeyDown(KeyCode.F)){
                readyToSpawn = true;
                if(!waveManager.roundTransition){
                    animator.SetBool("isSpawning", true);
                }else{
                    isPrompt = false;
                    ToggleChildByName(UI,"MiniBossPrompt", false, miniBoss.gameObject.name);
                    waveStorage = waveManager.wave;
                }
            }
        }
        if(readyToSpawn){
            if(waveStorage != waveManager.wave){
                animator.SetBool("isSpawning", true);
            }
        }

    }
}
