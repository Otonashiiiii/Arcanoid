using Unity.VisualScripting;
using UnityEngine;

public class BatControl : MonoBehaviour

{

    private float HorizontalInput;

    //Границы движения биты
    private float leftBorder;
    private float rightBorder;
    //Рассстояние от центра биты до ее края
    private float Offset = 12f;

    private Color defaultColor;
    private Renderer batRenderer;


    private void Start()
    {
        
        batRenderer = GetComponent<Renderer>();
        defaultColor = batRenderer.material.color;
        leftBorder = GameManager.LeftBorderX;
        rightBorder = GameManager.RightBorderX;

        //BatStartPosition();
    }

    void FixedUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (((transform.position.x < (leftBorder - Offset)) && HorizontalInput < 0) || ((transform.position.x > (rightBorder + Offset)) && HorizontalInput > 0))
        {
            transform.Translate(new Vector3(1f, 0f, 0f) * GameManager.batSpeed * -HorizontalInput * Time.deltaTime);
            GameManager.batPosition = transform.position;
        }
        
    }

    private void Update()
    {
        //Изменение цвета биты в зависимости от используемого подарка
        if(GameManager.usingGiftType != "")
        {
            if(GameManager.usingGiftType == "shoot")
            {
                batRenderer.material.color = Color.black;
            }
            else if(GameManager.usingGiftType == "sticky")
            {
                batRenderer.material.color = Color.yellow;
            }            
        }
        else if(batRenderer.material.color != defaultColor) 
        {
            batRenderer.material.color = defaultColor;
            
        }
    }
    // Начальная позиция для биты
    public void BatStartPosition()
    {
        Vector3 NewPosition;
       

        NewPosition = transform.position;
        NewPosition.x= leftBorder - (leftBorder - rightBorder) / 2;
        transform.position= NewPosition;
        GameManager.batPosition = transform.position;
    }
    
}

