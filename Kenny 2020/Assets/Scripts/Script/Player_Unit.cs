using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Unit : MonoBehaviour
{
    public Slider HPbar;
    [SerializeField]
    float health = 200;
    [SerializeField]
    float maxHealth = 200;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GameObject.FindGameObjectWithTag("MainHealthBar").GetComponent<Slider>();
        HPbar.maxValue = maxHealth;
    }

    private void Update()
    {
        if (health < 0.1) {
            dead();
        }
    }

    void dead() {
        GetComponent<AudioManager>().Play("Dead");
        this.transform.position= GameObject.Find("PlayerCheckPoint").transform.position;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy.transform.parent.gameObject);
        }
        health = maxHealth;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (health > maxHealth) health = maxHealth;

        HPbar.value = health;
    }

    public void getDamage(float dmg) {
        health -= dmg;
    }

    public void getHeal(float heal) {
        health += heal;
    }
}
