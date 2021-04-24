using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cam;

    public GameObject playerChar;

    public GameObject cartBox1;
    public GameObject cartBox2;
    public GameObject cartBox3;
    public GameObject cartBox4;

    public GameObject spawnerManager;

    public GameObject helper;
    public GameObject helperCheckPoint;

    public GameObject progressBar;

    public GameObject carrier;
    public GameObject playerCharas;

    public GameObject cart;

    public GameObject cartHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progressBar.GetComponent<Slider>().value >= 1000 && progressBar.GetComponent<Slider>().value <= 2000) {
            if (spawnerManager.GetComponent<Spawner>().enemyLock < 2) {
                spawnerManager.GetComponent<Spawner>().enemyLock = 2;
                cartBox1.SetActive(true);
            }
        }

        if (progressBar.GetComponent<Slider>().value > 2000)
        {
            if (spawnerManager.GetComponent<Spawner>().enemyLock < 2)
            {
                spawnerManager.GetComponent<Spawner>().enemyLock = 2;
            }

            if (spawnerManager.GetComponent<Spawner>().hunterSpawn == false)
            {
                spawnerManager.GetComponent<Spawner>().hunterSpawn = true;
            }

            cartBox2.SetActive(true);
        }


        if (progressBar.GetComponent<Slider>().value > 3000) {
            cartBox3.SetActive(true);
        }

        if (progressBar.GetComponent<Slider>().value > 4000)
        {
            cartBox4.SetActive(true);
        }

        if (GameObject.FindGameObjectWithTag("Helper") == null) {
            Instantiate(helper, helperCheckPoint.transform.position, helperCheckPoint.transform.rotation);
        }

        if (progressBar.GetComponent<Slider>().value >= 4000) {
            Debug.Log("You Win");
            cart.GetComponent<Animator>().SetBool("Walk", true);
            carrier.SetActive(true);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy.transform.parent.gameObject);
            }
            spawnerManager.SetActive(false);
            playerChar.SetActive(false);
            playerCharas.SetActive(true);
            helper.SetActive(false);
            cam.GetComponent<PlayerCamera>().player = cartHolder;
            cartHolder.GetComponent<Animator>().SetBool("Walk", true);
        }
    }
}
