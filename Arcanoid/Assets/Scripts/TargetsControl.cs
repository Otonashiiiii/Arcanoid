using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class TargetsControl : MonoBehaviour
{
    public string targetGiftType;
    [SerializeField] private GameObject giftPrefab;
    private Vector3 startGiftPosition;
    private GiftControl giftControl;
    private TMP_Text scoreValue;
    private GameObject obj;

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
        startGiftPosition = transform.position;
        startGiftPosition.z += 3f;

        obj = GameObject.Find("ScoreValue");
        scoreValue = obj.GetComponent<TMP_Text>();
    }
        
    // Обработка попадания в мишень шариком
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            CheckGift();        
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
            CheckGift();
            GameManager.GameScore += 10 * collisionCount;
            scoreValue.text = GameManager.GameScore.ToString();
            collisionCount = 0;
            GameManager.TargetsCount -= 1;
            Destroy(gameObject);
        }  
    }

    private void CheckGift()
    {

        if (targetGiftType != "")
        {
            giftControl = Instantiate(giftPrefab, startGiftPosition, Quaternion.Euler(0, 0, 90)).GetComponent<GiftControl>();
            giftControl.giftType = targetGiftType;

            targetGiftType = "";
        }
    }

}
