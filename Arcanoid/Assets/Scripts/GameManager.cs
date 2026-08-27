using System.Runtime.CompilerServices;
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

    //Позиция биты по координате Х (горизонтальная позиция)
    public static Vector3 batPosition;

    //Скорость движения шарика
    public static float ballSpeed;

    //Признак пойманного подарка
    public static bool catchGift;

    //Тип пойманного подарка
    public static string usingGiftType;

    //Счетчик времени действия подарка
    public static float usingGiftTime;

    //Признак открытого окна сообщений
    private bool messageOpen = false;
        
    private bool closeScene = false;
    private float timeDelay = 0f;



    private GameObject obj, bulletObject;
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
        catchGift= false;
        usingGiftType = "";
        usingGiftTime = 0f;

        obj = GameObject.Find("LeftBorder");
        LeftBorderX = obj.transform.position.x;

        obj = GameObject.Find("UpBorder");
        UpBorderZ = obj.transform.position.z;

        obj = GameObject.Find("RightBorder");
        RightBorderX = obj.transform.position.x;

        bulletObject = GameObject.Find("Bullet");
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
            nextLevel = false;
            levelGenerator.BuildLevel(CurrentLevel);
            ballOnBat = true;
            ballControl.StartPosition();
            
        }
        // Вызов меню паузы / завершения уровня
        if (TargetsCount == 0 || onPause)
        {
            CallPauseMenu();
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
        //Обработка использования подарка
        if(usingGiftType != "")
        {
            GiftInActive();
        }
        
    }

    private void GiftInActive()
    {
       
        if(usingGiftTime > 0)
        {
            if(usingGiftType == "shoot")
            {
                if (Input.GetKey(KeyCode.Space) && timeDelay <= 0)
                {
                    MakeShoot();
                    timeDelay = 1f;
                }                
                timeDelay -= Time.deltaTime;
            }
            usingGiftTime -= Time.deltaTime;
        }
        else
        {
            usingGiftTime = 0f;
            usingGiftType = "";
        }
    }
    private void MakeShoot()
    {
        float bulletSpeed = -60f;
        
        Vector3 bulletStartPosition = batPosition;
        bulletStartPosition.z -= 2f;

        GameObject currentBullet = Instantiate(bulletObject, bulletStartPosition, Quaternion.Euler(90, 0, 0));
        Rigidbody bulletRigidBody = currentBullet.GetComponent<Rigidbody>();

        bulletRigidBody.linearVelocity = new Vector3(0f, 0f, bulletSpeed);

    }

    //Обработка завершения игры
    public void EndOfGame()
    {

        Time.timeScale = 0f;
        messageOpen = true;
        message.enabled = true;
        closeScene = true;
    }

    //Вызов меню паузы
    public void CallPauseMenu()
    {

        Time.timeScale = 0f;

        if(onPause)
        {
            pauseHeader.text = "Pause";
            Cursor.visible = true;
            EOLMenu.enabled = true;
        }
        else if(TargetsCount == 0 && !closeScene)  //Конец уровня
        {
            if(CurrentLevel == MaxLevel-1) //Конец последнего уровня в игре
            {
                messageHeader.text = "Victory!!!";
                EndOfGame();
            }
            else
            {
                pauseHeader.text = "Level complete!!!";
                Cursor.visible = true;
                EOLMenu.enabled = true;
            }
        }
        else  //Ошибочный вызов функции
        {
            Time.timeScale = 0f;
        }
       
    }
         
}
