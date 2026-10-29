using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private GameObject bullet;

    public void Shoot()
    {
        Instantiate(bullet, bulletSpawnpoint.position, transform.rotation);
    }
}