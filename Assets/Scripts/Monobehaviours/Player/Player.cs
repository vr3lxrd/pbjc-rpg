using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Player : Caractere
{
    public Inventario inventarioPrefab;      // referÍncia ao objeto prefab do Inventario
    Inventario inventario;
    public HealthBar healthBarPrefab;        // referÍncia ao objeto prefab da HealthBar
    HealthBar healthBar;
    public MovimentoPlayer movimentoPlayer;

    void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = PlayerPrefs.GetFloat("playerHP") == null ? PlayerPrefs.GetFloat("playerHP") : inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        movimentoPlayer.movimentoEnabled = true;
    
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
                    case Item.TipoItem.MOEDA:
                        AjusteDanoPlayer(danoObjeto.quantidade);
                        DeveDesaparecer = inventario.AddItem(danoObjeto);
                        int moedasAtual = PlayerPrefs.GetInt("moedas", 0);
                        PlayerPrefs.SetInt("moedas", moedasAtual + 1);
                        break;
                    case Item.TipoItem.HEALTH:
                        inventario.AddItem(danoObjeto);
                        DeveDesaparecer = AjustePontosDano(danoObjeto.quantidade);
                        int coracaoAtual = PlayerPrefs.GetInt("coracao", 0);
                        PlayerPrefs.SetInt("coracao", coracaoAtual + 1);
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
            GameObject enemy = collision.gameObject;
            print("Encontrou com:" + enemy);
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
