using UnityEngine;

public class EOLMenu : MonoBehaviour
{
    [SerializeField] private Canvas EOL;

    private void Awake()
    {
        
        EOL= GetComponent<Canvas>();
        EOL.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void NextLevel()
    {
        GameManager.CurrentLevel++;
        Cursor.visible= false;
        Time.timeScale = 1f;
        GameManager.nextLevel = true;
        EOL.enabled = false;
    }
}
