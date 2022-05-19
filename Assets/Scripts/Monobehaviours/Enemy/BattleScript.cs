using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    int enemyHP;
    string enemyName;
    int actualScene;

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
    public Button runButton;

    void Start()
    {
        // Setando variaveis de jogador
        playerDamage = PlayerPrefs.GetInt("playerDamage", 1);
        playerHP = PlayerPrefs.GetFloat("playerHP", 5);
        actualScene = PlayerPrefs.GetInt("actualScene");
        enemyName = enemy.enemyName;
        enemyHP = enemy.enemyHP;
        SetupBattle(playerHP, playerDamage);
        StartBattle();
    }

    void StartBattle()
    {
        playerTurn = true;
    }

    // Fun��o respons�vel pelas a��es dos inimigos
    void botTurn()
    {
        runButton.interactable = false;
        if (enemyHP <= 0)
        {
            endBattle();
        }
        float random = Random.Range(1, 3);
        if (random == 1)
        {
            print("O inimigo atacou!");
            playerHP -= enemy.damage;
            textPlayerHP.text = setHPPlayer(playerHP);
            PlayerPrefs.SetFloat("playerHP", playerHP);
            player.pontosDano.valor = playerHP;
        }
        else
        {
            print("O inimigo curou!");
            enemyHP += enemy.damage / 2;
            textBossHP.text = "Vida: " + enemyHP;
        }
        playerTurn = true;

    }

    // Fun��o que inicializa a UI da batalha
    void SetupBattle(float playerHP, int playerDamage)
    {
        itemDropdown.AddOptions(new List<string> {
            "Tomates (" + PlayerPrefs.GetInt("tomates") + ")",
            "Trigos (" + PlayerPrefs.GetInt("trigos") + ")"
        });
        textBossName.text = "Boss: " + enemyName;
        textBossHP.text = "Vida: " + enemyHP;
        textPlayerHP.text = setHPPlayer(playerHP);
        textPlayerDamage.text = "For�a do jogador: " + playerDamage;
        attackButton.interactable = false;
        healButton.interactable = false;
    }

    // Fugir
    public void Run()
    {
        SceneManager.LoadScene(actualScene);
    }
    // Fun��o respons�vel pelo bot�o de usar item
    public void Heal()
    {
        int healValue;
        if (itemDropdown.value == 0)
        {
            int tomatesAtual = PlayerPrefs.GetInt("tomates");
            PlayerPrefs.SetInt("tomates", tomatesAtual - 1);
            itemDropdown.ClearOptions();
            itemDropdown.AddOptions(new List<string> {
            "Tomates (" + PlayerPrefs.GetInt("tomates") + ")"
        });
            healValue = 5;
        }
        else
        {
            int tomatesAtual = PlayerPrefs.GetInt("trigos");
            PlayerPrefs.SetInt("trigos", tomatesAtual - 1);
            itemDropdown.ClearOptions();
            itemDropdown.AddOptions(new List<string> {
            "Trigos (" + PlayerPrefs.GetInt("trigos") + ")"
        });
            healValue = 10;
        }

        playerHP += healValue;
        player.pontosDano.valor = playerHP;
        textPlayerHP.text = setHPPlayer(playerHP);
        PlayerPrefs.SetFloat("playerHP", playerHP);
        playerTurn = false;
        botTurn();
    }
    // Fun��o respons�vel pelo bot�o de atacar
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

    // Fun��o ap�s o boss morrer
    void endBattle()
    {
        print("Boss morto!");
        int deadBoss = PlayerPrefs.GetInt("deadBosses", 0);
        PlayerPrefs.SetInt("deadBosses", deadBoss + 1);
        print("Actual Scene: " + actualScene);
        SceneManager.LoadScene(actualScene);
    }

    // Verifica eligibilidade dos bot�es e vida do jogador
    void Update()
    {
        if (playerHP <= 0)
        {
            print("Game Over");
            SceneManager.LoadScene("Start");
        }
        if (PlayerPrefs.GetInt("tomates") == 0 && PlayerPrefs.GetInt("trigos") == 0)
        {
            healButton.interactable = false;
        }
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
    }
}