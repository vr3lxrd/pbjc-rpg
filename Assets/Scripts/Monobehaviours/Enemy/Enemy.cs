using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public string enemyName;
    public int enemyHP;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        print(PlayerPrefs.GetInt("deadBosses"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("deadBosses") == 1)
        {
            Enemy expectedEnemy = GetComponent<Enemy>();
            print(expectedEnemy.enemyName);
            if (expectedEnemy.enemyName == "abobora")
            {
                Destroy(expectedEnemy.gameObject);
            }
        }
    }
}
