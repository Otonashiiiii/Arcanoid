using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{

    private GameObject simpleTarget;
    private float startX;
    private float startZ;
    private float step = 12f;
    private int gridX = 8;
    private int gridZ = 2;

    
    
    void Start()
    {
        simpleTarget = GameObject.Find("SimpleTarget");
        startX = GameManager.LeftBorderX - 6f;
        startZ = GameManager.UpBorderZ + 10f;

        GameManager.TargetsCount = 0;

        for (int z = 0; z < gridZ; z++)
        {
            for(int x = 0; x < gridX; x++)
            {
                Vector3 Pos = new Vector3(startX + x * step * -1f, 2f, startZ + z * step);
                Instantiate(simpleTarget, Pos, Quaternion.identity);
                GameManager.TargetsCount ++;
            }
        }
    }

    
}
