using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody BallRigidbody;
    private BatControl Bat;


    void Awake()
    {
        BallRigidbody= GetComponent<Rigidbody>();
        Bat = GameObject.FindWithTag("Bat").GetComponent<BatControl>();
    }
   
    private void Start()
    {
       
        BallStartPosition();
    }
    
    private void FixedUpdate()
    {
       if(Input.GetKey(KeyCode.M))
        {
            BallRigidbody.AddForce(0f, 0f, -20f);
        }
             
    }

    void BallStartPosition()
    {
        Vector3 NewPosition;
        
        
        NewPosition = Bat.transform.position;
        NewPosition.z = NewPosition.z - 4f;
        NewPosition.x = NewPosition.x - 2f;
        transform.position = NewPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FailLine"))
        {
            BallRigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
            Bat.BatStartPosition();
            BallStartPosition();
        }
    }
}
