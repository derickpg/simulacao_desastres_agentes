using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PessoasScript : MonoBehaviour
{
    public string idResgate = "";
    GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("Mundo");
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -3); //avisa ao mundo ao esta!
        world.GetComponent<InfoMundo>().resgatados = world.GetComponent<InfoMundo>().getResgatados() + 1;
        //print("Avisei " + world.GetComponent<InfoMundo>().resgatados);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimento
    }

    public void limpaLugar()
    {
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), 0);
    }

    private void FixedUpdate()
    {
        //if ((world.GetComponent<InfoMundo>().getCasa().x == (int)transform.position.x) && (world.GetComponent<InfoMundo>().getCasa().y == (int)transform.position.z))
            // desaparece 



        // Tempo individual
        // Tempo medio
        // Tempo Total

    }
}
