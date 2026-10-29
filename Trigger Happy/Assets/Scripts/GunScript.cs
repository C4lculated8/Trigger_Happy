using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject particles;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletSpawnpoint.position, transform.rotation);
        Instantiate(particles, bulletSpawnpoint.position, transform.rotation);
    }
}