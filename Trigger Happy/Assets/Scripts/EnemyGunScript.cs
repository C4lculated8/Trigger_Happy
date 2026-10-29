using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject particles;

    public void Shoot()
    {
        Instantiate(bullet, bulletSpawnpoint.position, transform.rotation);
        Instantiate(particles, bulletSpawnpoint.position, transform.rotation);
    }
}