using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
    float elapsed = 0f;
    public int dashAbilitySpeed = 10;
    public float dashDuration = 3f;
    public bool Qactive = false;
    public GameObject player;

    private int dashSpeedSave;
    List<ability> allAbilitys = new List<ability>();
    
    ability curentE,currentQ;
    
    int abilityId;
    string abilityType;
    string abilityName;

    public GameObject cloneObject;
    ability(int id, string type, string name){
        abilityId = id;
        abilityType = type;
        abilityName = name;
    }
    public int getId(){
        return abilityId;
    }
    void makeAbilitys(string[] abilitys){
        foreach(string a in abilitys){
            string[] atts = a.Split(',');
            allAbilitys.Add(new ability(int.Parse(atts[0]),atts[1],atts[2]));
        }
    }
    void cloneAbility(){
          Instantiate(cloneObject, this.transform.position , Quaternion.identity);
    }
    void extraDash(){
        if(Qactive == false){
            Qactive = true;
            characterStats cStats = player.GetComponent<characterStats>();
            dashSpeedSave =  cStats.getSpeed();
            cStats.setSpeed(dashAbilitySpeed);
            elapsed = 0f;
        }
        dashDuration -= Time.deltaTime;
        if(dashDuration <= 0){
            Qactive = false;
            dashDuration = 3f;
            characterStats cStats = player.GetComponent<characterStats>();
            cStats.setSpeed(dashSpeedSave);
        }
      
       
    }
    void abilityGuide(int abilityId){
        if(abilityId == 1){
             cloneAbility();
        }
        if(abilityId == 2){
             extraDash();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentQ = new ability(2,"Q","Dash");
    }

    // Update is called once per frame
    void Update()
    {
        if(Qactive==true){
            Debug.Log("Using Dash");
            abilityGuide(currentQ.getId());
        }
        elapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
            {
                abilityGuide(currentQ.getId());
            }
    }
}
