using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.TargetsCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Borders"))
        {
            Destroy(gameObject);
        }
    }
}
