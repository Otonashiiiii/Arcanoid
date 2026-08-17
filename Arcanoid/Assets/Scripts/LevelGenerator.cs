using System.Collections;
using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{

    private GameObject simpleTarget, doubleTarget, tripleTarget;
    private float startX;
    private float startZ;
    private float stepX = 12f;
    private float stepZ = 5f;
    private int maxCol = 8;
    private int maxLine = 10;

    private string path = Path.Combine(Application.streamingAssetsPath, "levelsdata.json");

    private Levels levels = new Levels();

    private string jsonString;


    void Start()
    {

        levels.level = new LevelData[GameManager.MaxLevel];
        for (int i = 0; i < GameManager.MaxLevel; i++)
        {
            levels.level[i] = new LevelData();
            levels.level[i].targets = new Targets[maxCol * maxLine];
        }

        
        simpleTarget = GameObject.Find("SimpleTarget");
        doubleTarget = GameObject.Find("DoubleTarget");
        tripleTarget = GameObject.Find("TripleTarget");
        startX = GameManager.LeftBorderX - 8f;
        startZ = GameManager.UpBorderZ + 10f;

        //CreateLevel();

        //BuildLevel(0);
    }

    //Построение уровня (вывод целей) на сцене из предварительно заготовленного файла
    public void BuildLevel(int levelNum)
    {

        if (File.Exists(path))
        {
            jsonString= File.ReadAllText(path);
            levels = JsonUtility.FromJson<Levels>(jsonString);

            GameManager.TargetsCount = 0;
            GameManager.ballSpeed = levels.level[levelNum].ballSpeed;

            for (int i = 0; i < levels.level[levelNum].targets.Length; i++)
            {
                Vector3 Pos = new Vector3(startX + levels.level[levelNum].targets[i].column * stepX * -1f, 2f, startZ + levels.level[levelNum].targets[i].line * stepZ);
                string levelType = levels.level[levelNum].targets[i].targetType;
                switch(levelType)
                {
                    case "simple":
                        Instantiate(simpleTarget, Pos, Quaternion.identity);
                        GameManager.TargetsCount++;
                        break;
                    case "double":
                        Instantiate(doubleTarget, Pos, Quaternion.identity);
                        GameManager.TargetsCount++;
                        break;
                    case "triple":
                        Instantiate(tripleTarget, Pos, Quaternion.identity);
                        GameManager.TargetsCount++;
                        break;
                }
              
            }
        }
        else
        {
            Debug.Log("Файл с уровнями не найден!");
        }


    }
    //Создание уровня, запись в JSON и сохранение в файл
    public void CreateLevel()
    {
        
        

        levels.level[0].levelNumber = 0;
        levels.level[0].ballSpeed = 50f;
        int i = 0;

        for (int z = 0; z < maxLine; z++)
        {
            for (int x = 0; x < maxCol; x++)
            {
                levels.level[0].targets[i] = new Targets();
                levels.level[0].targets[i].line = z;
                levels.level[0].targets[i].column = x;
                levels.level[0].targets[i].targetType = "simple";
                i++;
            }
        }

        levels.level[1].levelNumber = 1;
        levels.level[1].ballSpeed = 50f;
        i = 0;

        for (int z = 0; z < maxLine; z++)
        {
            for (int x = 0; x < maxCol; x++)
            {
                levels.level[1].targets[i] = new Targets();
                levels.level[1].targets[i].line = z;
                levels.level[1].targets[i].column = x;
                levels.level[1].targets[i].targetType = "double";
                i++;
            }
        }
        jsonString = JsonUtility.ToJson(levels, true);
        
        File.WriteAllText(path, jsonString);
        Debug.Log("Файл сохранен по пути: " + path);
    }

    
}
