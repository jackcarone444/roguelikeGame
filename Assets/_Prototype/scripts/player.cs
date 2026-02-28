using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public class player : MonoBehaviour
{
    public  int damage;
    public  int health;
    public  int attackSpeed;
    public BoxCollider2D hitbox;



    public float maxspeed;
    public float moveforce;
    Rigidbody2D rb;
    Animator anim;


    float attackCooldownTimer;




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
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer >= attackSpeed)
        {
            attack();
            attackCooldownTimer = 0;
        }

        attackCooldownTimer += Time.deltaTime;

    }

    private void attack()
    {
        // attack
        var damageable = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
          .Where(x => x.TryGetComponent(out IDamageable damageable));

        GameObject target = damageable.FirstOrDefault(x => hitbox.bounds.Contains(x.transform.position));

        if (target != null)
        {
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            target.GetComponent<IDamageable>().TakeDamage(dirToTarget, damage);
        }

        anim.SetTrigger("attack");
    }

    private void move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 motion = new Vector2(x, y) * moveforce * Time.fixedDeltaTime;
        rb.velocity = motion;

        anim.SetInteger("y", (int)y);
        anim.SetInteger("x", (int)x);

        if ((int)x != 0)
        {
            anim.SetInteger("y", 0);
        }
    }
}
