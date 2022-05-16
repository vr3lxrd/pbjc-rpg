using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public Enemy enemy;
    int enemyHP;
    string enemyName;
    string actualScene;
    
    int playerDamage;
    float playerHP;
    bool playerTurn;

    public Text textBossName;
    public Text textBossHP;
    public Text textPlayerHP;
    public Text textPlayerDamage;

    public Dropdown itemDropdown;

    public Button attackButton;
    public Button healButton;

    void Start()
    {
        print(PlayerPrefs.GetInt("moedas"));
        PlayerPrefs.SetString("actualScene", "1");
        playerDamage = PlayerPrefs.GetInt("playerDamage", 1);
        playerHP = PlayerPrefs.GetFloat("playerHP", 5);
        actualScene = PlayerPrefs.GetString("actualScene");
        enemyName = enemy.enemyName;
        enemyHP = enemy.enemyHP;
        SetupBattle(playerHP, playerDamage);
        StartBattle();
    }

    void StartBattle()
    {
        playerTurn = true;
    }

    void botTurn()
    {
        if (enemyHP <= 0)
        {
            endBattle();
        }
        float random = Random.Range(1,3);
        print(random);
        if (random == 1)
        {
            print("O inimigo atacou!");
            playerHP -= enemy.damage;
            textPlayerHP.text = setHPPlayer(playerHP);
            PlayerPrefs.SetFloat("playerHP", playerHP);
        }
        else
        {
            print("O inimigo curou!");
            enemyHP += enemy.damage / 2;
            textBossHP.text = "Vida: " + enemyHP;
        }
        playerTurn = true;
        
    }

    void SetupBattle(float playerHP, int playerDamage)
    {
        itemDropdown.AddOptions(new List<string> { 
            "Coração (" + PlayerPrefs.GetInt("coracao") + ")",
            "Moedas (" + PlayerPrefs.GetInt("moedas") + ")",
        });
        textBossName.text = "Boss: " + enemyName;
        textBossHP.text = "Vida: " + enemyHP;
        textPlayerHP.text = setHPPlayer(playerHP);
        textPlayerDamage.text = "Força do jogador: " + playerDamage;
        attackButton.interactable = false;
        healButton.interactable = false;
    }


    public void Run()
    {
        SceneManager.LoadScene(actualScene);
    }
    public void Heal()
    {
        // Adicionar lógica de itens com legumes
        playerHP += playerDamage;
        textPlayerHP.text = setHPPlayer(playerHP);
        PlayerPrefs.SetFloat("playerHP", playerHP);
        playerTurn = false;
        botTurn();
    }
    public void Attack()
    {
        enemyHP -= playerDamage;
        textBossHP.text = "Vida: " + enemyHP;
        playerTurn = false;
        botTurn();
    }

    string setHPPlayer(float hp)
    {
        return "Vida do jogador: " + hp;
    }

    void endBattle()
    {
        print("Boss morto!");
        PlayerPrefs.SetInt("deadBosses", 1);
        SceneManager.LoadScene(int.Parse(actualScene));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            attackButton.interactable = true;
            healButton.interactable = true;
        }
        else
        {
            attackButton.interactable = false;
            healButton.interactable = false;
        }
        if(enemyHP <= 0)
        {
            endBattle();
        }
    }
}
