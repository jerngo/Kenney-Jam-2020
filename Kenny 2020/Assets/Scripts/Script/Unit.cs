using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HPbar;
    public GameObject canvas;
    [SerializeField]
    float health = 100;
    [SerializeField]
    float maxHealth = 100;
   
    public float damage = 10;
    [SerializeField]
    float regen = 1;
    [SerializeField]
    float MovSpeed = 0.3f;

    bool isRegen = true;
    public float atkRadius=0.5f;

    GameObject target;
    NavMeshAgent navmeshAgent;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        navmeshAgent = this.GetComponent<NavMeshAgent>();
        HPbar.GetComponent<Slider>().maxValue = maxHealth;
        //target = GameObject.FindGameObjectWithTag("Player");
    }

    void FindNearestTarget() {
        float distTemp = 99999999999999;
        GameObject tempTarget = null;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject target in targets) {
            float distanceMeasure = Vector3.Distance(transform.position, target.transform.position);
            if (distanceMeasure < distTemp) {
                distTemp = distanceMeasure;
                tempTarget = target;
            }
        }
        target = tempTarget;
    }

    void FindHelper() {
        if (GameObject.FindGameObjectWithTag("Helper") != null) {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 0.3f)
            {
                target = GameObject.FindGameObjectWithTag("Helper");
            }
            else {

                target = GameObject.FindGameObjectWithTag("Player");
                
            }
        }
        else {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
        
    }
    public void setHealth(float hp) {
        health = hp;
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }

    public void setRegen(float reg)
    {
        regen = reg;
    }

    public void setMovspeed(float ms)
    {
        MovSpeed = ms;
    }
    // Update is called once per frame
    public bool isHunter=false;
    void FixedUpdate()
    {
        if (isHunter)
        {
            FindHelper();

        }
        else {
            FindNearestTarget();
        }
        canvas.GetComponent<Canvas>().enabled = false;
        
        regeneration();
        
        HPbar.GetComponent<Slider>().value = health;
        if (health > maxHealth) {
            health = maxHealth;
        }   

        if (health < 0.1)
        {
            Dead();
        }
        else {
            navmeshAgent.speed = MovSpeed;
            movement();
            if (Vector3.Distance(transform.position, target.transform.position) < atkRadius)
            {
                anim.SetTrigger("Attack");
            }
        }
       
    }


    void regeneration() {
        health += regen;
    }
    void movement() {
        anim.SetFloat("Movement",1);
        navmeshAgent.SetDestination(target.transform.position);

    }

    public void getDamage(float dmg) {
        GetComponent<AudioManager>().PlayOnceAtATime("Burn");
        navmeshAgent.speed = MovSpeed / 2.5f;
        isRegen = false;
        if (canvas.GetComponent<Canvas>().enabled == false) {
            canvas.GetComponent<Canvas>().enabled = true;
        }
        health -= dmg;
    }

    public GameObject bottle;
    void Dead() {
        int temp = Random.Range(0, 101);
        if (temp < 30) {
            Instantiate(bottle, transform.position, transform.rotation);
        }
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Poof");
        Destroy(this.transform.parent.gameObject);
    }
}
