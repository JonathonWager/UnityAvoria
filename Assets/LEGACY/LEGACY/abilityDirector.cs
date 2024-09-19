// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class abilityDirector : MonoBehaviour
// {
//     // Reference to the player GameObject
//     public GameObject player;

//     // Prefabs for ability effects
//     public GameObject fireRing, meteor;

//     // List to store all available abilities
//     List<Ability> allAbility = new List<Ability>();

//     // Parameters for the Rangerator ability
//     private int rangeUseCount = 0;
//     private int rangeLevelCount = 5;
//     public int rangeAbiltyLevel = 1;
//     public float rangeAbilityModifer = 1.5f;
//     public float rangeAbilityTime = 5f;
//     public float rangeAbilityResetTime = 5f;

//     public int speedAbilityLevel = 1;
//     public float speedAbilityResetTime = 5f;
//     public float speedAbilityTime = 5f;
//     public float speedAbilityModifier = 1.5f;
//     private int speedUseCount = 0;
//     private int speedLevelCount = 5;

//     // Parameters for the Fire Ring ability
//     private int fireUseCount = 0;
//     private int fireLevelCount = 5;
//     public int fireRingLevel = 1;
//     public float fireringAbilityDuration = 5f;
//     public float fireringAbilityResetTime = 5f;

//     // Parameter for the Meteor Smash ability
//     public int meteorLevel = 1;
//     private int meteorLevelCount = 5;
//     private int meteorUseCount = 0;
//     public float meteorResetTime = 5f;

//     // Flags indicating whether abilities Q and E are available
//     public bool canQ, canE = true;

//     // Current Q and E abilities
//     private Ability currentQ, currentE;

//     // Temporary variable to store the original range for Rangerator
//     private float tempRange;
//     private float tempSpeed;

//     // Method to create abilities based on a string array
//     void makeAbilitys(string[] abilitys)
//     {
//         foreach (string a in abilitys)
//         {
//             string[] atts = a.Split(',');
//             allAbility.Add(new Ability(int.Parse(atts[0]), atts[1], atts[2][0]));
//         }
//     }

//     // Method to find an ability by ID from the allAbility list
//     Ability findAbilitysFromAll(int ID)
//     {
//         foreach (Ability a in allAbility)
//         {
//             if (a.getId() == ID)
//             {
//                 return a;
//             }
//         }
//         return null;
//     }

//     // Getter method to retrieve the current Q ability
//     public Ability getCurrentQ()
//     {
//         return currentQ;
//     }

//     // Getter method to retrieve the current E ability
//     public Ability getCurrentE()
//     {
//         return currentE;
//     }
//     void meteorLeveling(){
//                 meteorUseCount += 1;
//                 if(meteorUseCount == meteorLevelCount){
//                     meteorLevel += 1;
//                     meteorLevelCount = meteorLevelCount * 2;
//                 }
//             }
//     // Method for the Meteor Smash ability
//     void MeteorSmash()
//     {
//         meteorLeveling();
//         Vector3 mousePosition = Input.mousePosition;

//         // Set the z-coordinate to the distance from the camera
//         mousePosition.z = Camera.main.nearClipPlane;

//         // Convert the mouse position to world coordinates
//         Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
//         Instantiate(meteor, worldPosition, Quaternion.identity);
//         canE = false;
//         Invoke("meteorReset", meteorResetTime);
//     }

//     // Method to reset the availability of the Meteor Smash ability
//     void meteorReset()
//     {
//         canE = true;
//     }

//     void rangeAbilityLeveling(){
//             rangeUseCount += 1;
//             if(rangeUseCount == rangeLevelCount){
//                 rangeAbiltyLevel += 1;
//                 rangeLevelCount = rangeLevelCount * 2;
//                 rangeAbilityModifer = rangeAbilityModifer + 0.1f;
//                 rangeAbilityTime = rangeAbilityTime + 0.1f;
//             }
//         }
//     // Method to deactivate the Rangerator ability
//     void deactivateRangerator()
//     {
//         GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
//         characterStats cStats = player.GetComponent<characterStats>();
//         cStats.range = tempRange;
//         Invoke("qReset", rangeAbilityResetTime);
//     }
    
//     // Method to activate the Rangerator ability
//     void activateRangerator()
//     {
//         rangeAbilityLeveling();
//         GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
//         canQ = false;
//         characterStats cStats = player.GetComponent<characterStats>();
//         tempRange = cStats.range;
//         cStats.range = tempRange * rangeAbilityModifer;
//         Invoke("deactivateRangerator", rangeAbilityTime);
//     }
//     void fireRingLeveling(){
//                 fireUseCount += 1;
//                 if(fireUseCount == fireLevelCount){
//                     fireRingLevel += 1;
//                     fireLevelCount = fireLevelCount * 2;
//                 }
//             }
//     // Method to deactivate the Fire Ring ability
//     void deactivateFireRing()
//     {
//         Invoke("eReset", fireringAbilityResetTime);
//     }

//     // Method to activate the Fire Ring ability
//     void activateFireRing()
//     {
//         fireRingLeveling();
//         Instantiate(fireRing, this.transform.position, Quaternion.identity);
//         canE = false;
//         Invoke("deactivateFireRing", fireringAbilityDuration);
//     }
//     void speederatorLeveling(){
//         speedUseCount += 1;
//         if(speedUseCount == speedLevelCount){
//             speedAbilityLevel += 1;
//             speedLevelCount = speedLevelCount * 2;
//             speedAbilityModifier = speedAbilityModifier + 0.1f;
//             speedAbilityTime = speedAbilityTime + 0.1f;
//         }
//     }
//     void deactivateSpeederator(){
//         GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
//         playerMovement mStats = player.GetComponent<playerMovement>();
//         mStats.moveSpeed = tempSpeed;
//         Invoke("qReset", speedAbilityResetTime);
//     }
//     void activateSpeederator(){
//         GameObject.Find("QLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
//         speederatorLeveling();
//         canQ = false;
//         playerMovement mStats = player.GetComponent<playerMovement>();
//         tempSpeed = mStats.moveSpeed;
//         mStats.moveSpeed = tempSpeed + speedAbilityModifier;
//          Invoke("deactivateSpeederator", rangeAbilityTime);
//     }

//     // Method to use an ability based on its code
//     void useAbility(char Code)
//     {
//         if (Code == 'E')
//         {
//             if (currentE.getName() == "Fire Ring")
//             {
//                 activateFireRing();
//             }
//             if (currentE.getName() == "Meteor Smash")
//             {
//                 MeteorSmash();
//             }
//         }
//         else if (Code == 'Q')
//         {
//             if (currentQ.getName() == "Rangerator")
//             {
//                 activateRangerator();
//             }
//              if (currentQ.getName() == "Speederator")
//             {
//                 activateSpeederator();
//             }
//         }
//     }

//     // Method to reset the availability of the Q ability
//     void qReset()
//     {
//         canQ = true;
//     }

//     // Method to reset the availability of the E ability
//     void eReset()
//     {
//         canE = true;
//     }

//     // Getter method to retrieve the availability of the Q ability
//     public bool getQ()
//     {
//         return canQ;
//     }

//     // Getter method to retrieve the availability of the E ability
//     public bool getE()
//     {
//         return canE;
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Sample data for abilities
//         string[] abiltys = {
//             "1,Rangerator,Q",
//             "2,Fire Ring,E",
//             "3,Meteor Smash,E",
//             "4,Speederator,Q"
//         };

//         // Initialize abilities and set the current Q and E abilities
//         makeAbilitys(abiltys);
//         currentQ = findAbilitysFromAll(4);
//         currentE = findAbilitysFromAll(3);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Check for player input to use abilities
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (canE)
//             {
//                 useAbility('E');
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.Q))
//         {
//             if(canQ){
//                 useAbility('Q');
//             }
//         }
//     }
// }
