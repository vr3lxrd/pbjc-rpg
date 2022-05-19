using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;       // Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;
    public Image medidorImagem;         // recebe a barra de medição
    public Text pdTexto;                // recebe os dados de PD
    float maxPontosDano;                // armazena a quantidade limite de "saúde" do Player
    // Start is called before the first frame update
    void Start()
    {
    
        maxPontosDano = caractere.maxPontosDano;
    }


    // Update is called once per frame
    void Update()
    {
        maxPontosDano = caractere.maxPontosDano;
        if (caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdTexto.text = "PD:" + pontosDano.valor + "/" + maxPontosDano;
        }
    }
}
