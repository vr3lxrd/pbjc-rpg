using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// por ser abstract, essa classe n�o pode ser instanciada e sim herdada
public abstract class Caractere : MonoBehaviour
{
    public PontosDano pontosDano;
    public float inicioPontosDano;   // valor m�nimo inicial de "sa�de" do Player
    public float MaxPontosDano;      // valor m�ximo de "sa�de" do Player
    public int playerDamage;
}
