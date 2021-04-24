using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cont : MonoBehaviour
{
   
    public float moveSpeed = 0.3f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    

    void Movement() {
        

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticallAxis = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Movement", Mathf.Abs(horizontalAxis) + Mathf.Abs(verticallAxis));

        this.transform.Translate(horizontalAxis * moveSpeed * Time.fixedDeltaTime, 0, verticallAxis * moveSpeed * Time.fixedDeltaTime);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<AudioManager>().Play("GetHit");
            GetComponent<Player_Unit>().getDamage(collision.gameObject.GetComponent<Unit>().damage);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "heal")
        {
            Destroy(other.gameObject);
            GetComponent<AudioManager>().Play("Drink");
            GetComponent<Player_Unit>().getHeal(80);
        }
    }
}
