using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class abilityDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject fireRing, meteor;
    List<Ability> allAbility = new List<Ability>();


    public float rangeAbilityModifer = 1.5f;
    public float rangeAbilityTime = 5f;
    public float rangeAbilityResetTime = 5f;

    public float fireringAbilityDuration = 5f;
    public float fireringAbilityResetTime = 5f;

    public float meteorResetTime = 5f;
    public bool canQ,canE = true;

    private Ability currentQ,currentE;




    private float tempRange;
    void makeAbilitys(string[] abilitys)
    {
        foreach(string a in abilitys){
            string[] atts = a.Split(',');
            allAbility.Add(new Ability(int.Parse(atts[0]),atts[1],atts[2][0]));
        }
    }
     Ability findAbilitysFromAll(int ID){
        foreach(Ability a in allAbility){
            if(a.getId() == ID){
                return a;
            }
        }
        return null;
    }


    public Ability getCurrentQ(){
        return currentQ;
    }
    public Ability getCurrentE(){
        return currentE;
    }
    void MeteorSmash(){
        Vector3 mousePosition = Input.mousePosition;

        // Set the z-coordinate to the distance from the camera
        mousePosition.z = Camera.main.nearClipPlane;

        // Convert the mouse position to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Instantiate(meteor, worldPosition, Quaternion.identity);
        canE = false;
        Invoke("meteorReset", meteorResetTime);
    }
    void meteorReset(){
        canE = true;
    }
    void deactivateRangerator(){
        GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
        Debug.Log("Rangerator DEActivate!1");
        characterStats cStats = player.GetComponent<characterStats>();  
        cStats.range = tempRange;
        Invoke("qReset", rangeAbilityResetTime);
    }
    void activateRangerator(){
        GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
        canQ = false;
        Debug.Log("Rangerator Activate!1");
        characterStats cStats = player.GetComponent<characterStats>();
        tempRange = cStats.range;
        cStats.range = tempRange * rangeAbilityModifer;
        Invoke("deactivateRangerator", rangeAbilityTime);
    }
    void deactivateFireRing(){
        Invoke("eReset", fireringAbilityResetTime);
    }
    void activateFireRing(){
        Instantiate(fireRing, this.transform.position, Quaternion.identity);
        canE = false;
        Invoke("deactivateFireRing", fireringAbilityDuration);
    }
    void useAbility(char Code)
    {
        if(Code == 'E'){
          if(currentE.getName() == "Fire Ring"){
            activateFireRing();
          }  
          if(currentE.getName() == "Meteor Smash"){
            MeteorSmash();
          } 
        }else if(Code == 'Q'){
            if(currentQ.getName() == "Rangerator"){
                activateRangerator();
            }
        }
    }
    void qReset(){
         canQ = true;
    }
    void eReset(){
         canE = true;
    }

    public bool getQ(){
        return canQ;
    }
    public bool getE(){
        return canE;
    }
    // Start is called before the first frame update
    void Start()
    {
         string[] abiltys = {
            "1,Rangerator,Q",
            "2,Fire Ring,E",
            "3,Meteor Smash,E"
        };
        makeAbilitys(abiltys);
        currentQ = findAbilitysFromAll(1);
        currentE = findAbilitysFromAll(2);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(canE){
                useAbility('E');
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(canQ){
                useAbility('Q');
            }
        }
    }
}
