using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

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
    public static int MaxLevel = 2;

    //Количество жизней игрока
    public static int LivesLeft = 5;

    //Номер текущего уровня игры
    public static int CurrentLevel = 0;

    //Признак паузы в игре
    public static bool onPause = false;

    //Признак перехода на следующий уровень
    public static bool nextLevel = false;

    //Признак шарика на бите
    public static bool ballOnBat = true;

    //Скорость горизонтального движения биты
    public static float batSpeed = 50f;

    //Скорость движения шарика
    public static float ballSpeed;



    private GameObject obj;
    private LevelGenerator levelGenerator;
    private BallControl ballControl;

    [SerializeField] private Canvas EOLMenu;
    [SerializeField] private TMP_Text pauseHeader;

    void Awake()
    {
        
        obj = GameObject.Find("LeftBorder");
        LeftBorderX=obj.transform.position.x;
        
        obj = GameObject.Find("UpBorder");
        UpBorderZ = obj.transform.position.z;

        obj = GameObject.Find("RightBorder");
        RightBorderX = obj.transform.position.x;

        GameScore = 0;

        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        ballControl=GameObject.Find("Ball").GetComponent<BallControl>();
        
    }

    private void Start()
    {
        levelGenerator.BuildLevel(CurrentLevel);
        ballControl.StartPosition();
        //levelGenerator.CreateLevel();
    }

    private void Update()
    {
        // Если закончились жизни, то конец игры
        if(LivesLeft == 0)
        {
            EndOfGame();
        }
        // Проверка нажатия кнопки паузы
        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            onPause = true;
        }
        // Генерация следующего уровня
        if (nextLevel && CurrentLevel < MaxLevel)
        {
            levelGenerator.BuildLevel(CurrentLevel);
            ballOnBat = true;
            ballControl.StartPosition();
            nextLevel = false;
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
                EndOfGame();
            }
            
        }
        
            
        
    }
    public void EndOfGame()
    {
        if(TargetsCount == 0 && CurrentLevel == MaxLevel-1)
        {
            Debug.Log("Victory!!!");

        }
        else
        {
            Debug.Log("Game Over");
        }
        
    }

}
