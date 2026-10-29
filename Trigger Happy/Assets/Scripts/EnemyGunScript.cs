using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform holder;

    void Update()
    {
        Vector3 playerPosition = player.position;

        Vector2 direction = playerPosition - transform.position;

        transform.right = direction;

        float yScale = direction.x < 0 ? -1f : 1f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (holder.localScale.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, angle);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), yScale, Mathf.Abs(transform.localScale.z));
    }
}