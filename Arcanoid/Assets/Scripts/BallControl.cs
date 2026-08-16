using TMPro;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody BallRigidbody;
    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float startOffsetFromTop = 121f;

    private BatControl bat;

    [SerializeField] private TMP_Text livesValue;


    void Awake()
    {
        BallRigidbody = GetComponent<Rigidbody>();
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
       if(Input.GetKey(KeyCode.M))
        {
            BallRigidbody.linearVelocity = new Vector3(0f, 0f, -40f);
            //BallRigidbody.AddForce(0f, 0f, -20f);
        }
    }

    //Начальная позиция шарика перед началом игры
    public void BallStartPosition()
    {
        Vector3 NewPosition;


        NewPosition = transform.position;
        NewPosition.x = leftBorder - (leftBorder - rightBorder) / 2;
        NewPosition.z = topBorder + startOffsetFromTop;
        transform.position = NewPosition;
    }

    //Обработка выхода шарика из игры
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FailLine"))
        {
            GameManager.LivesLeft--;
            livesValue.text = GameManager.LivesLeft.ToString();
            StartPosition();
        }
    }

    //установка шарика и биты на стартовую позицию
    public void StartPosition()
    {
        BallRigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
        bat.BatStartPosition();
        BallStartPosition();
    }
}
