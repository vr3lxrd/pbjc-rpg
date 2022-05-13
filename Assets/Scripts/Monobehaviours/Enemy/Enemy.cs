using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealthPoints;
    public int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (this.healthPoints <= 0)
        {
            print("Morreu");
            GameObject.Destroy(gameObject);
        }
    }
}
