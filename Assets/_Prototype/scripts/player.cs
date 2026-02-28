using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public class player : MonoBehaviour
{
    public float maxspeed;
    public float moveforce;
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 motion = new Vector2(x, y) * moveforce * Time.fixedDeltaTime;
        rb.velocity = motion;


        if(y > 0)
        {
            anim.SetInteger("y", 1);
                
        }
        if (y < 0)
        {
            anim.SetInteger("y",-1);

        }
        if (x > 0)
        {
            anim.SetInteger("x", 1);

        }
        if (x < 0)
        {
            anim.SetInteger("x",-1);

        }
    }
}
