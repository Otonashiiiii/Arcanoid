using UnityEngine;

public class GiftControl : MonoBehaviour
{

    private Rigidbody giftRigidBody;
    private float giftSpeed = 20f;
    public string giftType;


    private void Start()
    {
        giftRigidBody = GetComponent<Rigidbody>();
        giftRigidBody.linearVelocity = new Vector3(0f, 0f, giftSpeed);
    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bat"))
        {
            GameManager.catchGift = true;
            GameManager.usingGiftType = giftType;
            GameManager.usingGiftTime = 30f;
            Debug.Log(giftType);
            Destroy(gameObject);
        }
        if (other.CompareTag("FailLine"))
        {
            Destroy(gameObject);
        }
    }
}
