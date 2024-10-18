using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStatManager : MonoBehaviour
{
    public float enemyHpPlayerHpMultiplier = 4f;
    public float enemyDamageWaveMultiplier = 2f;

    WaveManager wManager;
    enemyStats eStats;

    characterStats cStats;
    // Start is called before the first frame update
    void Start()
    {
        wManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        eStats = GetComponent<enemyStats>();
        cStats = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>();
        Debug.Log(cStats.basehp + " * " +enemyHpPlayerHpMultiplier);
        eStats.hp =  (int)(cStats.basehp * enemyHpPlayerHpMultiplier);
        eStats.dmgBuff += (int)(wManager.wave * enemyDamageWaveMultiplier);
    }


}
