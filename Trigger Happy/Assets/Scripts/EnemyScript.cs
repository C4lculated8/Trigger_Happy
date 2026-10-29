using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float aggressionRadius;
    public float cooldown;
    public float aftermath;
    private float counter = 0f;

    private bool facingRight = true;
    private bool playerInRange;
    public bool flipped = false;
    public bool aggressive = false;
    public bool dead = false;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyGunScript enemyGunScript;
    [SerializeField] private GameObject gun;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, aggressionRadius, playerLayer);

        if (playerInRange)
        {
            aggressive = true;
        }

        if(dead == true)
        {
            Die();
        }
        else if(aggressive == true)
        {
            Attack();
        }
        else
        {
            Idle();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }

    public void Idle()
    {
        anim.Play("Idle");
    }

    public void Attack()
    {
        LookAtPlayer();

        anim.SetBool("isAggressive", true);
        
        counter += Time.deltaTime;

        if(counter >= cooldown)
        {
            Shoot();
            counter = 0f;
        }
    }

    public void Shoot()
    {
        enemyGunScript.Shoot();
    }

    public void Die()
    {
        anim.SetBool("isDead", true);
        aftermath -= Time.deltaTime;
        Destroy(gun);
        if (aftermath <= 0f)
        {
            Destroy(gameObject);
        }
        if (((facingRight && rb.linearVelocity.x > 0f) || !facingRight && rb.linearVelocity.x < 0f) && !flipped)
        {
            flipped = true;
            Flip();
        }
    }

    private void LookAtPlayer()
    {
        Vector3 playerPosition = player.position;

        Vector2 direction = playerPosition - transform.position;

        if (facingRight && direction.x < 0f || !facingRight && direction.x > 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        if(gun != null)
        {
            Vector3 gunScale = gun.transform.localScale;
            Vector3 localScale = transform.localScale;

            gunScale.x *= -1;
            localScale.x *= -1;

            gun.transform.localScale = gunScale;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;

            localScale.x *= -1;

            transform.localScale = localScale;
        }
    }
}