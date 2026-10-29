using UnityEngine;

public class FlipScript : MonoBehaviour
{
    [SerializeField] private Transform holder;

    void Update()
    {
        if (holder.localScale.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, transform.localRotation.z);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, transform.localRotation.z);
        }
    }
}
