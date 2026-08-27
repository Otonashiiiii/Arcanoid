using UnityEngine;

public class MessageControl : MonoBehaviour
{
    [SerializeField] private Canvas message;

    private void Start()
    {

        message = GetComponent<Canvas>();
        message.enabled = false;
    }

}
