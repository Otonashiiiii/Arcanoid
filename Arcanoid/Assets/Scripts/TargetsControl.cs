using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TargetsControl : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValue;

    void Start()
    {
        
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
            GameManager.TargetsCount -= 1;
            Destroy(gameObject);
        }
        if (GameManager.TargetsCount == 0)
        {
            Debug.Log("End of level");
        }
        

    }
   
}
