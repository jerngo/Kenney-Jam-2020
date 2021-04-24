using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Helper_Cont : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HPbar;
    public GameObject canvas;
    [SerializeField]
    float health = 500;
    [SerializeField]
    float maxHealth = 500;

    [SerializeField]
    float inventory = 0;
    [SerializeField]
    float bagLimit = 200;

    public float damage = 10;
    [SerializeField]
    float regen = 1;
    [SerializeField]
    float MovSpeed = 0.3f;

    bool isRegen = true;
    public float atkRadius = 0.5f;
    [SerializeField]
    GameObject target;
    NavMeshAgent navmeshAgent;
    Animator anim;

    public GameObject progressBar;
    void Start()
    {
        anim = GetComponent<Animator>();
        navmeshAgent = this.GetComponent<NavMeshAgent>();
        //target = GameObject.FindGameObjectWithTag("Player");
        HPbar.GetComponent<Slider>().maxValue = maxHealth;

        pandoraBox[0] = GameObject.Find("Pandoras_4");
        pandoraBox[1] = GameObject.Find("Pandoras_3");
        pandoraBox[2] = GameObject.Find("Pandoras_2");
        pandoraBox[3] = GameObject.Find("Pandoras_1");

        progressBar = GameObject.Find("Progress");
    }

    [SerializeField]
    bool isBagFull = false;

    public GameObject[] pandoraBox;
    void FindNearestTarget()
    {
        GameObject tempTarget = null;
        if (!isBagFull)
        {

            foreach (GameObject pandora in pandoraBox) {
                if (pandora.GetComponent<Pandoras_Cont>().checkTreasure() > 0) {
                    tempTarget = pandora;
                }
            }
           // if (pandoraBox[0].GetComponent<Pandoras_Cont>().checkTreasure() > 0)
           // {
           //     tempTarget = pandoraBox[0];
           // }
           // else if (pandoraBox[1].GetComponent<Pandoras_Cont>().checkTreasure() > 0)
           // {
            //    tempTarget = pandoraBox[1];
            //}
            //else if (pandoraBox[2].GetComponent<Pandoras_Cont>().checkTreasure() > 0)
            //{
            //    tempTarget = pandoraBox[2];
            //}
           // else if (pandoraBox[3].GetComponent<Pandoras_Cont>().checkTreasure() > 0)
            //{
           //     tempTarget = pandoraBox[3];
           // }
            if (tempTarget == null) {
                tempTarget = GameObject.FindGameObjectWithTag("Cart");
            }

            target = tempTarget;
        }
        else {
            target = GameObject.FindGameObjectWithTag("Cart");
        }
    }

    public void setHealth(float hp)
    {
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
    public bool isHunter = false;
    void FixedUpdate()
    {
        FindNearestTarget();
        if (inventory >= bagLimit) {
            isBagFull = true;
        }
        else {
            isBagFull = false;
        }
       

        //regeneration();

        HPbar.GetComponent<Slider>().value = health;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health < 0.1)
        {
            Dead();
        }
        else
        {
            navmeshAgent.speed = MovSpeed;

            movement();
            if (Vector3.Distance(transform.position, target.transform.position) < atkRadius && !isBagFull)
            {
                if (target.tag != "Player") {
                    anim.SetBool("Taking", true);
                    target.GetComponent<Pandoras_Cont>().takeTreasure(0.5f);
                    inventory += 0.5f;
                    progressBar.GetComponent<Slider>().value += 0.5f;
                }
                else
                {
                    anim.SetBool("Taking", false);
                }
            }
            else {
                anim.SetBool("Taking", false);
            }
        }

    }


    void regeneration()
    {
        health += regen;
    }
    void movement()
    {
        anim.SetFloat("Movement", 1);
        navmeshAgent.SetDestination(target.transform.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            GetComponent<AudioManager>().PlayOnceAtATime("GetHit");
            isRegen = false;
            health -= collision.gameObject.GetComponent<Unit>().damage;
        }

        if (collision.gameObject.tag == "Cart")
        {
            //GetComponent<AudioManager>().PlayOnceAtATime("GetHit");
            //Debug.Log("Simpan");
            inventory = 0;
        }
    }
    public void getDamage(float dmg)
    {
        GetComponent<AudioManager>().PlayOnceAtATime("GetHit");
        isRegen = false;
        health -= dmg;
    }

    public GameObject bottle;
    public GameObject checkpointHelper;
    void Dead()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Poof");
        this.transform.position = checkpointHelper.transform.position;
        health = maxHealth;
        //Destroy(this.transform.parent.gameObject);
    }
}
