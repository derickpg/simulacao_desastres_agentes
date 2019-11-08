using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogo : MonoBehaviour
{
    public int fogoInicial = 100;
    public int fatorDeExpansao = 50;
    GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("Mundo");
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -1); //avisa ao mundo ao esta!
        int fatorMenor = fatorDeExpansao;
        int posicaoFator = 1;
        int posx = (int)transform.position.x;
        int posz = (int)transform.position.z;
        int maxX = world.GetComponent<InfoMundo>().mundoX;
        int maxZ = world.GetComponent<InfoMundo>().mundoZ;
        int fogoAtual = fogoAtual - fatorDeExpansao;
        while(fogoAtual > 0){
            // (Z-1,X+1)
            if(((posx + posicaoFator) < maxX) && ((posz - posicaoFator) > -1))
                world.GetComponent<InfoMundo>().addFogoMundo(posz - posicaoFator,posx + posicaoFator,fogoAtual);
            // (Z+1,x-1)
            if(((posz + posicaoFator) < maxZ) && ((posx - posicaoFator) > -1))
                world.GetComponent<InfoMundo>().addFogoMundo(posz - posicaoFator,posx + posicaoFator,fogoAtual);
            // (z+1,x+1)
            if(((posx + posicaoFator) < maxX) && ((posz + posicaoFator) < maxZ))
                world.GetComponent<InfoMundo>().addFogoMundo(posz + posicaoFator,posx + posicaoFator,fogoAtual);
            // (z-1,x-1) 
            if((((posx - posicaoFator) > -1) && ((posz - posicaoFator) > -1))
                world.GetComponent<InfoMundo>().addFogoMundo(posz - posicaoFator,posx - posicaoFator,fogoAtual);    
            // (z, x-1)
            if(((posx - posicaoFator) > -1))
                world.GetComponent<InfoMundo>().addFogoMundo(posz,posx - posicaoFator,fogoAtual);
            // (z, x+1)
            if(((posx + posicaoFator) < maxX))
                world.GetComponent<InfoMundo>().addFogoMundo(posz,posx + posicaoFator,fogoAtual);
            // (z+1, x)
            if(((posz + posicaoFator) < maxZ))
                world.GetComponent<InfoMundo>().addFogoMundo(posz + posicaoFator,posx,fogoAtual);   
            // (z-1, x)
            if(((posz - posicaoFator) > -1))
                world.GetComponent<InfoMundo>().addFogoMundo(posz - posicaoFator,posx,fogoAtual);     

            fogoAtual = fogoAtual - fatorDeExpansao;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
