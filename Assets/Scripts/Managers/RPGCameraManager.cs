using UnityEngine;
using Cinemachine;

public class RPGCameraManager : MonoBehaviour
{

    public static RPGCameraManager instanciaCompartilhada = null;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
