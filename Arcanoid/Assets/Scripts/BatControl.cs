using Unity.VisualScripting;
using UnityEngine;

public class BatControl : MonoBehaviour

{

    [SerializeField] private float Speed = 50f;
    private float HorizontalInput;

    [SerializeField] private float LeftBorder = 40f;
    [SerializeField] private float RightBorder = -48f;
    [SerializeField] private float Offset;


    private void Start()
    {
        BatStartPosition();
    }

    void FixedUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (((transform.position.x < (LeftBorder + Offset)) && HorizontalInput < 0) || ((transform.position.x > (RightBorder - Offset)) && HorizontalInput > 0))
        {
            transform.Translate(new Vector3(1f, 0f, 0f) * Speed * -HorizontalInput * Time.deltaTime);
        }
        
    }
    // Начальная позиция для биты
    public void BatStartPosition()
    {
        Vector3 NewPosition;
       

        NewPosition = transform.position;
        NewPosition.x= LeftBorder - (LeftBorder - RightBorder) / 2;
        transform.position= NewPosition;
    }
    
}

