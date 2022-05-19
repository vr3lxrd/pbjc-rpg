using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void LoadOnClick(int sceneIndex)
    {
        Debug.LogError("as");
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartMundoGame()
    {
        /* M�todo unity para carga de cena
         * Pode ser passado no param o nome da cena ou seu index
         */
        SceneManager.LoadScene("FarmMap");
    }
    public void StartSceneCreditos()
    {
        SceneManager.LoadScene("Credits");
    }
}
