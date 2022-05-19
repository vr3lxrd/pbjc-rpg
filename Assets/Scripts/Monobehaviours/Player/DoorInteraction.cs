using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorInteraction : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))//verifica a tag com qual meu player interagiu
        {
            SceneManager.LoadScene("ForestMap");
            player.gameObject.transform.position = new Vector3(-6, -2, 0);
        }
    }
  
}


