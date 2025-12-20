using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public Vector3 nextSpawnPos;
    float baselineY;

    void Start()
    {
        SpawnGround();
    }

    public void SpawnGround()
    {
        var chunk = Instantiate(groundPrefab, nextSpawnPos, Quaternion.identity);
        Transform marker = chunk.transform.GetChild(1);
        
        nextSpawnPos = new Vector3(marker.position.x, baselineY, nextSpawnPos.z);
    }
    
    void Awake()
    {
        baselineY = nextSpawnPos.y;   
    }


}
