using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Player : Caractere
{
    public Inventario inventarioPrefab;      // referência ao objeto prefab do Inventario
    Inventario inventario;
    public HealthBar healthBarPrefab;        // referência ao objeto prefab da HealthBar
    HealthBar healthBar;
    public MovimentoPlayer movimentoPlayer;  // referência ao script de movimento

    bool trueOne = false;
    void Awake()
    {
        // Se tem mais de um player na hierarquia, deixa apenas o primeiro
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<Player>().trueOne == false)
                {
                    Destroy(player);
                }
            }
        }
        else
        {
            trueOne = true; 
        }
    }

    void Start()
    {
        // Reseta os prefs e inicia os prefabs
        PlayerPrefs.DeleteAll();
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        movimentoPlayer.movimentoEnabled = true;
        // Setando vida do jogador nas prefs
        if (PlayerPrefs.GetFloat("playerHP", 0) == 0)
        {
            PlayerPrefs.SetFloat("playerHP", inicioPontosDano);
        }
        else
        {
            pontosDano.valor = PlayerPrefs.GetFloat("playerHP");
        }
        PlayerPrefs.SetInt("playerDamage", playerDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sistema de coleta de objetos
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item danoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            if (danoObjeto != null)
            {
                bool DeveDesaparecer = false;
                print("Acertou: " + danoObjeto.NomeObjeto);
                switch (danoObjeto.tipoItem)
                {
                    case Item.TipoItem.TOMATE:
                        AjusteDanoPlayer(danoObjeto.quantidade);
                        DeveDesaparecer = inventario.AddItem(danoObjeto);
                        int moedasAtual = PlayerPrefs.GetInt("tomates", 0);
                        PlayerPrefs.SetInt("tomates", moedasAtual + 1);
                        break;
                    case Item.TipoItem.TRIGO:
                        AjusteDanoPlayer(danoObjeto.quantidade);
                        DeveDesaparecer = inventario.AddItem(danoObjeto);
                        int trigoAtual = PlayerPrefs.GetInt("trigos", 0);
                        PlayerPrefs.SetInt("trigos", trigoAtual + 1);
                        break;
                    case Item.TipoItem.BATATA:
                        inventario.AddItem(danoObjeto);
                        DeveDesaparecer = AjustePontosDano(danoObjeto.quantidade);
                        int coracaoAtual = PlayerPrefs.GetInt("batata", 0);
                        PlayerPrefs.SetInt("batata", coracaoAtual + 1);
                        break;
                    case Item.TipoItem.BETERRABA:
                        inventario.AddItem(danoObjeto);
                        DeveDesaparecer = AjustePontosDano(danoObjeto.quantidade);
                        int beterrabaAtual = PlayerPrefs.GetInt("beterraba", 0);
                        PlayerPrefs.SetInt("beterraba", beterrabaAtual + 1);
                        break;
                    default:
                        break;
                }
                if (DeveDesaparecer)
                {
                    collision.gameObject.SetActive(false);
                }   
            }
        }
        // Sistema de colisão com inimigos para inicio de batalha
        else if (collision.gameObject.CompareTag("Inimigo"))
        {
            PlayerPrefs.SetInt("actualScene", SceneManager.GetActiveScene().buildIndex);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            int deadBosses = PlayerPrefs.GetInt("aboboraFight", 0);
            print("deadBosses: " + deadBosses);
            if (enemy.enemyName == "abobora" && deadBosses == 0)
            {
                PlayerPrefs.SetInt("aboboraFight", 1);
                SceneManager.LoadScene(11);
            }
            else if (enemy.enemyName == "brocolis" && deadBosses == 1)
            {
                PlayerPrefs.SetInt("brocolisFight", 1);
                SceneManager.LoadScene(12);
            }
        }
    }

    // Aumenta o dano do jogador
    public void AjusteDanoPlayer(int quantidade)
    {
        playerDamage += quantidade;
        PlayerPrefs.SetInt("playerDamage", playerDamage);
    }

    // Aumenta a vida do jogador
    public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < maxPontosDano)
        {
            pontosDano.valor += quantidade;
            PlayerPrefs.SetFloat("playerHP", pontosDano.valor);
            return true;
        }
        else if (pontosDano.valor == maxPontosDano)
        {
            maxPontosDano += quantidade;
            pontosDano.valor += quantidade;
            PlayerPrefs.SetFloat("playerHP", pontosDano.valor);
            return true;
        }
        else return false;
    }

    // Sistema para desativar UI em cena de boss
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name.Contains("Boss"))
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
    }
}
