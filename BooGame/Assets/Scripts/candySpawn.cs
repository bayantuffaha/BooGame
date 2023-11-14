using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candySpawn : MonoBehaviour
{
    public GameObject CandyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 30; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(-10, 11), Random.Range(-10, 11));
            Instantiate(CandyPrefab, randomSpawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
