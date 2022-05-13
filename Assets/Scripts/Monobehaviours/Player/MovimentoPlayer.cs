using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f;  // equivale ao impulso a ser dado ao player
    Vector2 Movimento = new Vector2();
    public bool movimentoEnabled = true;       
    Animator animator;                        // guarda a componente do controlador de animacao // guarda o nome do parametro do animador
    Rigidbody2D rb2D;                         // guarda a componente de corpo rigido do player

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEstado();
        if (!movimentoEnabled)
        {
            rb2D.velocity = new Vector2(0, 0);
            animator.SetBool("walking", false);
        }
    }

    private void FixedUpdate()
    {
        
        MoveCaractere();
    }

    private void MoveCaractere()
    {
        if (this.movimentoEnabled)
        {
            Movimento.x = Input.GetAxisRaw("Horizontal");
            Movimento.y = Input.GetAxisRaw("Vertical");
            Movimento.Normalize();
            rb2D.velocity = Movimento * VelocidadeMovimento;
        }
        
    }

    private void UpdateEstado()
    {
        if (Movimento.x != 0 || Movimento.y != 0)
        {
            if(Movimento.x < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(-180,0,180));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

}
