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
    public MovimentoPlayer movimentoPlayer;

    bool trueOne = false;
    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        // if there are more than 1 players, then we know that we have more than we need
        // so find the original one
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
            trueOne = true; // only first one is true one
        }
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        movimentoPlayer.movimentoEnabled = true;
        if (PlayerPrefs.GetFloat("playerHP", 0) == 0)
        {
            PlayerPrefs.SetFloat("playerHP", inicioPontosDano);
        }
        else
        {
            pontosDano.valor = PlayerPrefs.GetFloat("playerHP");
        }
        PlayerPrefs.SetInt("playerDamage", playerDamage);
        PlayerPrefs.SetInt("actualScene", SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        else if (collision.gameObject.CompareTag("Inimigo"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            int deadBosses = PlayerPrefs.GetInt("aboboraFight", 0);
            print("deadBosses: " + deadBosses);
            if (enemy.enemyName == "abobora" && deadBosses == 0)
            {
                PlayerPrefs.SetInt("aboboraFight", 1);
                SceneManager.LoadScene(11);
            }
        }
    }

    public void AjusteDanoPlayer(int quantidade)
    {
        playerDamage += quantidade;
        PlayerPrefs.SetInt("playerDamage", playerDamage);
    }

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

    // Update is called once per frame
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
