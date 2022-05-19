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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Limpa bosses do mapa após morrer em batalha
        if (PlayerPrefs.GetInt("deadBosses") == 1)
        {
            Enemy expectedEnemy = GetComponent<Enemy>();
            print("enemy: " + expectedEnemy.enemyName);
            if (expectedEnemy.enemyName == "abobora")
            {
                Destroy(expectedEnemy.gameObject);
            }
        }
        else if (PlayerPrefs.GetInt("deadBosses") == 2)
        {
            Enemy expectedEnemy = GetComponent<Enemy>();
            print("enemy: " + expectedEnemy.enemyName);
            if (expectedEnemy.enemyName == "brocolis")
            {
                Destroy(expectedEnemy.gameObject);
            }
        }
    }
}
