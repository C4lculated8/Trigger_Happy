using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float aggressionRadius;
    public float cooldown;
    private float counter = 0f;

    private bool facingRight = true;
    private bool playerInRange;
    public bool aggressive;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyGunScript enemyGunScript;
    [SerializeField] private GameObject gun;

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, aggressionRadius, playerLayer);

        if (playerInRange)
        {
            aggressive = true;
        }

        if(aggressive == true)
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
        Flip();

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

    private void Flip()
    {
        Vector3 playerPosition = player.position;

        Vector2 direction = playerPosition - transform.position;

        if (facingRight && direction.x < 0f || !facingRight && direction.x > 0f)
        {
            facingRight = !facingRight;

            Vector3 gunScale = gun.transform.localScale;
            Vector3 localScale = transform.localScale;

            gunScale.x *= -1;
            localScale.x *= -1;

            gun.transform.localScale = gunScale;
            transform.localScale = localScale;
        }
    }
}