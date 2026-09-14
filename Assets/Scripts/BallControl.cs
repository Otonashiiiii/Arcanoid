using TMPro;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody ballRigidBody;
    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float Offset = 12f;
    private float HorizontalInput;
    private BatControl bat;

    [SerializeField] private TMP_Text livesValue;


    void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        bat = GameObject.Find("Bat").GetComponent<BatControl>();

        leftBorder = GameManager.LeftBorderX;
        rightBorder = GameManager.RightBorderX;
        topBorder = GameManager.UpBorderZ;
    }
   
    private void Start()
    {
        livesValue.text = GameManager.LivesLeft.ToString();




        //BallStartPosition();
    }
    
    private void FixedUpdate()
    {
        if (GameManager.ballOnBat)
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            if (((transform.position.x < (leftBorder - Offset)) && HorizontalInput < 0) || ((transform.position.x > (rightBorder + Offset)) && HorizontalInput > 0))
            {
                transform.Translate(new Vector3(1f, 0f, 0f) * GameManager.batSpeed * -HorizontalInput * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                ballRigidBody.linearVelocity = new Vector3(0f, 0f, GameManager.ballSpeed * -1f);
                GameManager.ballOnBat = false;

            }
            
        }
        
    }

    //Начальная позиция шарика перед началом игры
    public void BallStartPosition()
    {
        Vector3 NewPosition;


        NewPosition = GameManager.batPosition;
        NewPosition.z -= 3f;
        transform.position = NewPosition;
    }

    //Обработка выхода шарика из игры
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FailLine"))
        {
            GameManager.LivesLeft--;
            livesValue.text = GameManager.LivesLeft.ToString();
            GameManager.ballOnBat = true;
            GameManager.usingGiftType = "";
            GameManager.usingGiftTime = 0f;
            StartPosition();
        }
    }

    //установка шарика и биты на стартовую позицию
    public void StartPosition()
    {
        ballRigidBody.linearVelocity = new Vector3(0f, 0f, 0f);
        bat.BatStartPosition();
        BallStartPosition();
    }

    //Обработка подарка "липучка"
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bat") && GameManager.usingGiftType == "sticky")
        {
            ballRigidBody.linearVelocity = new Vector3(0f, 0f, 0f);
            BallStartPosition();
            GameManager.ballOnBat = true;

        }
    }
}
