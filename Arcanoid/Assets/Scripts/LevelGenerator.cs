using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private GameObject SimpleTarget;
    [SerializeField] private float StartX;
    [SerializeField] private float StartZ;
    [SerializeField] private float Step;
    private int GridX = 5;
    private int GridZ = 2;

    
    
    void Start()
    {
        for(int z = 0; z < GridZ; z++)
        {
            for(int x = 0; x < GridX; x++)
            {
                Vector3 Pos = new Vector3(StartX + x * Step, 0.2f, StartZ + z * Step);
                Instantiate(SimpleTarget, Pos, Quaternion.identity);
            }
        }
    }

    
}
