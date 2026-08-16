using Unity.VisualScripting;
using UnityEngine;

public class BatControl : MonoBehaviour

{

    [SerializeField] private float Speed = 50f;
    private float HorizontalInput;

    //Границы движения биты
    private float leftBorder;
    private float rightBorder;
    //Рассстояние от центра биты до ее края
    private float Offset = 12f;


    private void Start()
    {
        
        leftBorder = GameManager.LeftBorderX;
        rightBorder = GameManager.RightBorderX;

        //BatStartPosition();
    }

    void FixedUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (((transform.position.x < (leftBorder - Offset)) && HorizontalInput < 0) || ((transform.position.x > (rightBorder + Offset)) && HorizontalInput > 0))
        {
            transform.Translate(new Vector3(1f, 0f, 0f) * Speed * -HorizontalInput * Time.deltaTime);
        }
        
    }
    // Начальная позиция для биты
    public void BatStartPosition()
    {
        Vector3 NewPosition;
       

        NewPosition = transform.position;
        NewPosition.x= leftBorder - (leftBorder - rightBorder) / 2;
        transform.position= NewPosition;
    }
    
}

