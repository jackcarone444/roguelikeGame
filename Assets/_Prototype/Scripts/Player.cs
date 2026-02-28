using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float moveForce;
    Animator anim;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 motion = new Vector2(x, y) * moveForce * Time.fixedDeltaTime;
        rb.velocity = motion;

        if(y > 0)
        {
            anim.SetInteger("y", 1);
        }

        if (y < 0)
        {
            anim.SetInteger("y", -1);

        }

        if (x > 0)
        {
            anim.SetInteger("x", 1);
        }

        if (x > 0)
        {
            anim.SetInteger("x", -1);
        }



    }
}

