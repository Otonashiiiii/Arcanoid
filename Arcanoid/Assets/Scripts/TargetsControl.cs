using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TargetsControl : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValue;

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


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Обработка попадания в мишень
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.GameScore += 10;
            scoreValue.text=GameManager.GameScore.ToString();
            collisionCount--;
            if(collisionCount == 0)
            {
                GameManager.TargetsCount -= 1;
                Destroy(gameObject);
            }
        }
        
        

    }
   
}
