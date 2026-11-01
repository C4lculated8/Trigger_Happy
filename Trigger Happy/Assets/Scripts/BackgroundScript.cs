using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float parallaxSpeed;
    public GameObject cam;

    private float startPosX, length;
    
    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxSpeed;
        float movement = cam.transform.position.x * (1 - parallaxSpeed);

        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);

        if(movement > startPosX + length)
        {
            startPosX += length;
        }
        else if(movement < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
