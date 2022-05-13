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
                        DeveDesaparecer = inventario.AddItem(danoObjeto);
                        break;
                    case Item.TipoItem.HEALTH:
                        DeveDesaparecer = AjustePontosDano(danoObjeto.quantidade);
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
            enterBattle(enemy, pontosDano);
            print("Encontrou com:" + enemy);
        }
    }

    public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor += quantidade;
            print("Ajustando PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }
        else return false;
    }

    public void enterBattle(GameObject enemy, PontosDano pontosDano)
    {
        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        Enemy inimigo = enemy.GetComponent<Enemy>();
        movimentoPlayer.movimentoEnabled = false;
        vcam.m_Lens.OrthographicSize = 2;
        vcam.Follow = enemy.transform;
        vcam.LookAt = enemy.transform;
    }

    void exitBattle()
    {
        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        movimentoPlayer.movimentoEnabled = true;
        vcam.m_Lens.OrthographicSize = 3.75f;
        vcam.Follow = this.transform;
        vcam.LookAt = this.transform;
    }

    public void attack(GameObject enemy, PontosDano pontosDano)
    {
        pontosDano.valor = pontosDano.valor - 1;
        GameObject.Destroy(enemy);
        exitBattle();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
