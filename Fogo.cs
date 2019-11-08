using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogo : MonoBehaviour
{
    GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("Mundo");
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -1); //avisa ao mundo ao esta!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
