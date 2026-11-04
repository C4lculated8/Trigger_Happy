using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyScript>().dead = true;
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().Die();
        }
    }
}
