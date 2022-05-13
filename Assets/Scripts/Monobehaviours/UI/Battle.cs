using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public GameObject player;

    public void attack()
    {
        player.GetComponent<Player>().attack();
    }
    public void run()
    {
        player.GetComponent<Player>().exitBattle();
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
