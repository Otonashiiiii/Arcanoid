using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Координаты границ игрового поля
    public static float LeftBorderX;
    public static float RightBorderX;
    public static float UpBorderZ;

    //Счетчик очков
    public static int GameScore;

    //Счетчик мишеней в уровне
    public static int TargetsCount;

    //Количество уровней в игре
    public static int MaxLevel;

    //Количество жизней игрока
    public static int LivesLeft;

    //Номер текущего уровня игры
    public static int CurrentLevel;

    //Признак паузы в игре
    public static bool onPause;

    //Признак перехода на следующий уровень
    public static bool nextLevel;

    //Признак шарика на бите
    public static bool ballOnBat;

    //Скорость горизонтального движения биты
    public static float batSpeed;

    //Скорость движения шарика
    public static float ballSpeed;

    //Признак открытого окна сообщений
    private bool messageOpen = false;

    private bool endOfGame = false;
    private bool closeScene = false;



    private GameObject obj;
    private LevelGenerator levelGenerator;
    private BallControl ballControl;

    [SerializeField] private Canvas EOLMenu;
    [SerializeField] private Canvas message;
    [SerializeField] private TMP_Text pauseHeader;
    [SerializeField] private TMP_Text messageHeader;

    void Awake()
    {
       //Настройка компонентов для вызываемых методов других классов
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        ballControl=GameObject.Find("Ball").GetComponent<BallControl>();

       //Установка глобальных переменных
        GameScore = 0;
        MaxLevel = 2;
        LivesLeft = 5;
        CurrentLevel = 0;
        onPause = false;
        nextLevel = false;
        batSpeed = 50f;

        obj = GameObject.Find("LeftBorder");
        LeftBorderX = obj.transform.position.x;

        obj = GameObject.Find("UpBorder");
        UpBorderZ = obj.transform.position.z;

        obj = GameObject.Find("RightBorder");
        RightBorderX = obj.transform.position.x;
    }

    private void Start()
    {
        ballOnBat = true;
        levelGenerator.BuildLevel(CurrentLevel);
        ballControl.StartPosition();
        Time.timeScale = 1f;
        //levelGenerator.CreateLevel();
    }

    private void Update()
    {
        // Если закончились жизни, то конец игры
        if(LivesLeft == 0)
        {
            messageHeader.text = "Game Over";
            endOfGame = true;
        }

        // Проверка нажатия кнопки паузы
        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            onPause = true;
        }

        // Генерация следующего уровня
        if (nextLevel && CurrentLevel < MaxLevel)
        {
            nextLevel = false;
            levelGenerator.BuildLevel(CurrentLevel);
            ballOnBat = true;
            ballControl.StartPosition();
            
        }
        // Вызов меню паузы / завершения уровня
        if (TargetsCount == 0 || onPause)
        {
            if(CurrentLevel < MaxLevel-1)
            {
                Time.timeScale = 0f;
                if(onPause)
                {
                    pauseHeader.text = "Pause";
                }
                else
                {
                    pauseHeader.text = "Level complete!!!";
                }
                Cursor.visible = true;
                EOLMenu.enabled = true;
            }
            else
            {
                messageHeader.text = "Victory!!!";
                endOfGame = true;
            }
        }

        // Закрытие окна сообщений, если пользователь нажал кнопку
        if(messageOpen && Input.anyKeyDown)
        {
            message.enabled = false;
            messageOpen = false;
            Time.timeScale = 1f;
            if(closeScene)
            {
                Cursor.visible = true;
                SceneManager.LoadSceneAsync(0);
            }
        }
        //Обработка завершения игры
        if(endOfGame)
        {
            Time.timeScale = 0f;
            messageOpen = true;
            message.enabled = true;
            closeScene = true;
            endOfGame = false;
        }
        
    }
         
}
