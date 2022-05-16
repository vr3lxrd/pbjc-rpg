using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public string enemyName;
    public int initialEnemyHP;
    public int enemyHP;
    int playerDamage;
    float playerHP;

    public Text textBossName;
    public Text textBossHP;
    public Text textPlayerHP;
    public Text textPlayerDamage;


    void Start()
    {
        playerDamage = PlayerPrefs.GetInt("playerDamage", 1);
        playerHP = PlayerPrefs.GetFloat("playerHP", 5);
        enemyHP = initialEnemyHP;
        SetupBattle(playerHP, playerDamage);
    }

    void SetupBattle(float playerHP, int playerDamage)
    {
        textBossName.text = "Boss: " + enemyName;
        textBossHP.text = "Vida: " + initialEnemyHP;
        textPlayerHP.text = "Vida do Jogador: " + playerHP;
        textPlayerDamage.text = "Força do jogador: " + playerDamage;
    }

    public void Run()
    {
        print("Fugiu");
    }
    public void Heal()
    {
        print("Heal");
    }
    public void Attack()
    {
        enemyHP -= playerDamage;
        textBossHP.text = "Vida: " + enemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHP <= 0)
        {
            print("Boss morreu");
        }
    }
}
