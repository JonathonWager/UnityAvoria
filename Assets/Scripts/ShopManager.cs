using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int shopCount = 3;
    public List<Vector3> vector3List; 
    public GameObject shopMan;
    private List<int> shopList = new List<int>();
    // Start is called before the first frame update
    int CalculateTeir(int shopNumber){
        int wave = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>().wave;
        if(shopNumber > 7){
            return 3;
        }
        if(shopNumber> 3)
        {
            if(wave > 20){
                return 3;
            }
            return 2;
        }
        if (shopNumber>= 0){
            if(wave > 12){
                return 2;
            }
            return 1;
        }
        if(wave > 5){
             return 1;
        }else{
             return 0;
        }
       
    }
    void UpdateShops(){
        for(int i = 0; i < shopCount; i++){           
            int rand = Random.Range(0, vector3List.Count);
            if(!shopList.Contains(rand)){
                shopList.Add(rand);
                Vector3 spawnPosition = vector3List[rand];
                GameObject shop = Instantiate(shopMan, spawnPosition, Quaternion.identity);
                shop.GetComponent<ShopV2>().setStart(CalculateTeir(rand),2);
            }else{
                i--;
            }
        }
    }
    public void DeleteShops(){
        GameObject[] shops = GameObject.FindGameObjectsWithTag("shop");
        for(int i = 0; i < shops.Length; i++){
            Destroy(shops[i]);
        }
        shopList = new List<int>();
        UpdateShops();
    }
    void Start()
    {
        UpdateShops();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
