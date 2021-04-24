using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int limitEnemy = 35;
    public GameObject[] Enemies;
    public GameObject[] Spawners;
    public int enemyLock=1;
    public bool hunterSpawn=false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawning(Random.Range(1, 8), Random.Range(7.0f, 20.0f)));
    }
    [SerializeField]
    bool limitReach = false;
    bool checkEnemyLimit(int limit) {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
        if (temp.Length >= limit)
        {
            limitReach = true;
        }
        else {
            limitReach = false;
        }
        return limitReach;
    }

    bool hunterCD=false;
    private void Update()
    {
        int cd;
        if (enemyLock == 1)
        {
            cd = 55;
        }
        else {
            cd = 35;
        }
        if (!checkEnemyLimit(limitEnemy)) {
            if (hunterSpawn == true)
            {

                if (hunterCD == false)
                {
                    for (int i = 0; i < Random.Range(0, 3); i++)
                    {
                        Instantiate(Enemies[2], Spawners[Random.Range(0, 7)].transform.position, Spawners[Random.Range(0, 7)].transform.rotation);
                    }

                    hunterCD = true;
                    StartCoroutine(huntercdbegin(Random.Range(cd-10, cd)));
                }
            }
        }
        
    }

    IEnumerator huntercdbegin(float time) {
        yield return new WaitForSeconds(time);
        hunterCD = false;
    }


    IEnumerator spawning(int sum, float time) {
        if (!checkEnemyLimit(limitEnemy))
        {
            for (int i = 0; i < sum; i++)
            {
                Instantiate(Enemies[Random.Range(0, enemyLock)], Spawners[i].transform.position, Spawners[i].transform.rotation);
            }

            

        }

        yield return new WaitForSeconds(time);
        StartCoroutine(spawning(Random.Range(2, 8), Random.Range(7.0f, 20.0f)));

    }
}
