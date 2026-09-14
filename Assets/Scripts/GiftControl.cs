using UnityEngine;

public class GiftControl : MonoBehaviour
{

    private Rigidbody giftRigidBody;
    private float giftSpeed = 20f;

    //Тип подарка. Определяется при создании объекта в скрипте TargetsControl
    public string giftType;
    [SerializeField] private Material shootMaterial;
    [SerializeField] private Material stickyMaterial;
    private Renderer giftRenderer;
    private int bornLivesLeft;


    private void Start()
    {
        giftRenderer = GetComponent<Renderer>();
        if(giftType == "shoot")
        {
            giftRenderer.material = shootMaterial;
        }
        else if(giftType == "sticky")
        {
            giftRenderer.material = stickyMaterial;
        }

        giftRigidBody = GetComponent<Rigidbody>();
        giftRigidBody.linearVelocity = new Vector3(0f, 0f, giftSpeed);
        bornLivesLeft = GameManager.LivesLeft;
    }

    private void Update()
    {
        if(GameManager.TargetsCount == 0 || bornLivesLeft > GameManager.LivesLeft) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bat"))
        {
            GameManager.catchGift = true;
            GameManager.usingGiftType = giftType;
            GameManager.usingGiftTime = 30f;
            Destroy(gameObject);
        }
        if (other.CompareTag("FailLine"))
        {
            Destroy(gameObject);
        }
    }
}
