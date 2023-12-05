using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candySpawn : MonoBehaviour
{
    public GameObject CandyPrefab;
    public Vector2 max;
    public Vector2 min;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 99; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            Instantiate(CandyPrefab, randomSpawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
