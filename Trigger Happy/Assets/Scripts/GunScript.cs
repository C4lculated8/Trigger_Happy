using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private GameObject bullet;
    
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
    }
}