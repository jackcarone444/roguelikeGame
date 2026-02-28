using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]

public class player : MonoBehaviour
{
    public float maxspeed;
    public float moveForce;
    Rigidbody2D rb;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");



        Vector2 motion = new Vector2(x,y) * moveForce * Time.fixedDeltaTime;
        rb.velocity = motion;

        anim.SetInteger("x", (int) x);

        anim.SetInteger ("y", (int) y);


        if ((int)x != 0)
        {
            anim.SetInteger("y", (int)0);
        }
        




















    }
}
