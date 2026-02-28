using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public int damage;
    public int health;
    public int attackSpeed;
    public BoxCollider2D hitBox;
    
    
    public float maxSpeed;
    public float moveForce;
    
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
        Move();
        
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer >= attackSpeed)
        {
            Attack();
            attackCooldownTimer = 0;
        }
        
        attackCooldownTimer += Time.deltaTime;
        
    }

    private void Attack()
    {
        var damageable = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(x => x.TryGetComponent(out IDamageable ignore));

        GameObject target = damageable.FirstOrDefault(x => hitBox.bounds.Contains(x.transform.position));

        if (target != null)
        {
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            target.GetComponent<IDamageable>().TakeDamage(dirToTarget, damage);
        }
            
        anim.SetTrigger("attack");
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        Vector2 motion = new Vector2(x,y) * moveForce * Time.fixedDeltaTime;
        rb.velocity = motion;
        
        anim.SetInteger("x", (int) x);
        
        anim.SetInteger("y", (int) y);

        if ((int)x != 0)
        {
            anim.SetInteger("y", 0);
        }
    }
}
