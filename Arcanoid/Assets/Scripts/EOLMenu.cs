using UnityEngine;
using UnityEngine.SceneManagement;

public class EOLMenu : MonoBehaviour
{
    [SerializeField] private Canvas EOL;

    private void Start()
    {
        
        EOL= GetComponent<Canvas>();
        EOL.enabled = false;
    }

    public void QuitGame()
    {
        EOL.enabled = false;
        GameManager.onPause = false;
        SceneManager.LoadSceneAsync(0);

    }

    public void NextLevel()
    {
        if (!GameManager.onPause)
        {
            GameManager.CurrentLevel++;
            GameManager.nextLevel = true;
        }
        Cursor.visible= false;
        Time.timeScale = 1f;
        EOL.enabled = false;
        GameManager.onPause = false;
    }
}
