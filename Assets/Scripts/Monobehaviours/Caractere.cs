using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// por ser abstract, essa classe não pode ser instanciada e sim herdade
public abstract class Caractere : MonoBehaviour
{
    public PontosDano pontosDano;
    public float inicioPontosDano;   // valor mínimo inicial de "saúde" do Player
    public float MaxPontosDano;      // valor máximo de "saúde" do Player
}
