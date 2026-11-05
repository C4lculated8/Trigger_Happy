using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public float impactRadius;
    public float force;

    [SerializeField] private GameObject particles;
    [SerializeField] private Animator anim;

    private bool hasExploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "PlayerBullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Collider2D[] ownColliders = GetComponents<Collider2D>();

        foreach (Collider2D col in ownColliders)
        {
            col.enabled = false;
        }

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
            ExplodeScript explodeScript = obj.GetComponent<ExplodeScript>();

            if (obj.gameObject == gameObject)
            {
                continue;
            }

            if (playerScript != null)
            {
                playerScript.Explode(direction, force);
            }
            else if(explodeScript != null)
            {
                explodeScript.Explode();
            }
            else
            {
                rb.AddForce(direction * force, ForceMode2D.Impulse);
                if (enemyScript != null)
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