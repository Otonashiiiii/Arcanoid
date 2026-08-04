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

    private GameObject _obj;

   
    void Awake()
    {
        
        _obj = GameObject.Find("LeftBorder");
        LeftBorderX=_obj.transform.position.x;
        
        _obj = GameObject.Find("UpBorder");
        UpBorderZ = _obj.transform.position.z;

        _obj = GameObject.Find("RightBorder");
        RightBorderX = _obj.transform.position.x;

        GameScore = 0;
        
    }

}
