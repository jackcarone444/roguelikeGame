using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]

public class player : MonoBehaviour
{
    public int damage;
    public int health;
    public int attackspeed;
    public BoxCollider2D hitbox;
    public float maxspeed;
    public float moveForce;
    Rigidbody2D rb;
    Animator anim;

    float attackCoolDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();


        if (Input.GetMouseButtonDown(0)  && attackCoolDownTimer >= attackspeed)
        {
            attack();
            attackCoolDownTimer = 0;
        }


        attackCoolDownTimer += Time.deltaTime;


    }

    private void attack()
    {

        // attaxck
        var damageable = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(x => x.TryGetComponent(out IDamageable damageable));

        GameObject target = damageable.FirstOrDefault(x => hitbox.bounds.Contains(x.transform.position));

        if (target != null)
        {
            Vector2 dirtotarget = (target.transform.position - transform.position).normalized;
            target.GetComponent<IDamageable>().TakeDamage(dirtotarget, damage);
        }


        anim.SetTrigger("attack");
    }

    private void move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");



        Vector2 motion = new Vector2(x, y) * moveForce * Time.fixedDeltaTime;
        rb.velocity = motion;

        anim.SetInteger("x", (int)x);

        anim.SetInteger("y", (int)y);


        if ((int)x != 0)
        {
            anim.SetInteger("y", (int)0);
        }
    }
}
