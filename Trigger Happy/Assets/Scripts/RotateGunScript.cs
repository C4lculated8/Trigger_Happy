using UnityEngine;

public class RotateGunScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;

        transform.right = direction;

        float yScale = direction.x < 0 ? -1f : 1f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (player.localScale.x < 0)
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