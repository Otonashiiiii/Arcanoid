using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class TargetsControl : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValue;
    public string targetGiftType;
    private GameObject giftObject, currentGift;
    private Vector3 startGiftPosition;
    private GiftControl giftControl;

    private int collisionCount;

    void Start()
    {
        if(gameObject.CompareTag("SimpleTargets"))
        {
            collisionCount = 1;
        } 
        else if (gameObject.CompareTag("DoubleTargets"))
        {
            collisionCount = 2;
        }
        else if (gameObject.CompareTag("TripleTargets"))
        {
            collisionCount = 3;
        }
        giftObject = GameObject.Find("Gift");
        startGiftPosition = transform.position;
        startGiftPosition.z += 3f;

    }
        
    // Обработка попадания в мишень шариком
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            if(targetGiftType != "")
            {
                giftControl = Instantiate(giftObject, startGiftPosition, Quaternion.Euler(0,0,90)).GetComponent<GiftControl>();
                giftControl.giftType = targetGiftType;
                
                targetGiftType = "";
            }            
            GameManager.GameScore += 10;
            collisionCount--;            
            scoreValue.text=GameManager.GameScore.ToString();            
            if(collisionCount == 0)
            {
                GameManager.TargetsCount -= 1;
                Destroy(gameObject);
            }
        }
     
    }

    // Обработка попадания в мишень пулей
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            GameManager.GameScore += 10 * collisionCount;
            scoreValue.text = GameManager.GameScore.ToString();
            collisionCount = 0;
            GameManager.TargetsCount -= 1;
            Destroy(gameObject);
        }  
    }

}
