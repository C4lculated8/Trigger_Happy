using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private float lifetime = 5f;
    private float counter = 0f;

    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        counter += Time.deltaTime;

        if(counter >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().dead = true;
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}