using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool facingRight = true;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gun;

    void Update()
    {
        Flip();
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
