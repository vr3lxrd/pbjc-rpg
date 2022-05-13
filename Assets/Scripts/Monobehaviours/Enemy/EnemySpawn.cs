using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefabEnemy;

    void spawnEnemy()
    {
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
