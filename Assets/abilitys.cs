using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
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

    void makeAbilitys(string[] abilitys){
        foreach(string a in abilitys){
            string[] atts = a.Split(',');
            allAbilitys.Add(new ability(int.Parse(atts[0]),atts[1],atts[2]));
        }
    }
    void cloneAbility(){
          Instantiate(cloneObject, this.transform.position , Quaternion.identity);
    }
    void abilityGuide(int abilityId){
        if(abilityId == 1){
             cloneAbility();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
