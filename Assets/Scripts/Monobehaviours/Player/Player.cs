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

    void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        movimentoPlayer.movimentoEnabled = true;
        PlayerPrefs.SetFloat("playerHP", inicioPontosDano);
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
            if (enemy.enemyName == "abobora")
            {
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
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor += quantidade;
            PlayerPrefs.SetFloat("playerHP", pontosDano.valor);
            return true;
        }
        else if (pontosDano.valor == MaxPontosDano)
        {
            MaxPontosDano += quantidade;
            pontosDano.valor += quantidade;
            PlayerPrefs.SetFloat("playerHP", pontosDano.valor);
            return true;
        }
        else return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pontosDano.valor <= 0)
        {
            print("Game Over");
            SceneManager.LoadScene(2);
        }
    }
}
