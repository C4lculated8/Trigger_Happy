using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public float impactRadius;
    public float force;

    [SerializeField] private GameObject particles;
    [SerializeField] private Animator anim;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactRadius);

        foreach(Collider2D obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            Vector2 direction = obj.transform.position - transform.position;
            if (direction == Vector2.zero) continue;
            direction.Normalize();

            PlayerScript playerScript = obj.GetComponent<PlayerScript>();
            EnemyScript enemyScript = obj.GetComponent<EnemyScript>();

            if (playerScript != null)
            {
                playerScript.Explode(direction, force);
            }
            else
            {
                rb.AddForce(direction * force, ForceMode2D.Impulse);
                if(enemyScript != null)
                {
                    enemyScript.dead = true;
                }
            }
        }

        anim.SetBool("isExploding", true);
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}