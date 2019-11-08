using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PessoasScript : MonoBehaviour
{
    public string idResgate       = "";
    public bool sendoResgatado    = false;
    public bool comportRandom     = false;
    public bool comportSegueLider = false;
    public bool lider             = false;
    public bool visao             = 2; 
    public int limiteCalor;
    public int liderSeguido; //Indica qual o id do lider que deve ser seguido...
    public int[][] mundo;
    // Variaveis de controle dos resgatados pelo lider
    public GameObject[] listaResgatados;
    public int qntResgatados = 0;

    //Conhecimento do mundo
    GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("Mundo");
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -3); //avisa ao mundo ao esta!
        world.GetComponent<InfoMundo>().resgatados = world.GetComponent<InfoMundo>().getResgatados() + 1;
        mundo = mundo = world.GetComponent<InfoMundo>().getMundo();
        limiteCalor = world.GetComponent<InfoMundo>().limiteCalor;
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
        if(sendoResgatado == false){
            if(comportRandom == true || lider == true){
                //Procura Força brut
                int xi = (int)transform.position.x;
                int zi = (int)transform.position.z;

                if ((xi + 1 < tamX) && ((mundo[zi][xi + 1] == menorCaminho)))
                {  //== 0 , é livre  \ == 1 é ocupado 
                    xi = xi + 1;
                    menorCaminho = 0;
                    transform.position = new Vector3(xi, 0, zi);
                }
                else if ((zi - 1 >= 0) && (mundo[zi - 1][xi] == menorCaminho))
                {  //== 0 , é livre  \ == 1 é ocupado
                    zi = zi - 1;
                    menorCaminho = 0;
                    transform.position = new Vector3(xi, 0, zi);
                }
                else if ((xi - 1 >= 0) && (mundo[zi][xi - 1] == menorCaminho))
                {  //== 0 , é livre  \ == 1 é ocupado
                    xi = xi - 1;
                    menorCaminho = 0;
                    transform.position = new Vector3(xi, 0, zi);
                }
                else if ((zi + 1 < tamX) && (mundo[zi + 1][xi] == menorCaminho))
                {  //== 0 , é livre  \ == 1 é ocupado
                    zi = zi + 1;
                    menorCaminho = 0;
                    transform.position = new Vector3(xi, 0, zi);
                }
                else
                {
                    menorCaminho = menorCaminho + 1;
                }
                mundo[zi][xi] = mundo[zi][xi] + 1;  // atualiza o mundo por onde já passou! */
            }else if(comportSegueLider == true){
                AVISA O LIDER
                ADD O GameObject NA LISTA DO LIDER
                E ADD +1 NO qntResgatados 
            }
            //transform.position = new Vector3(caminhoAS[contStar].x, 0, caminhoAS[contStar].y);
        }

    }


    /* Pergunta se a pessoa que foi vista é lider ou não */
    private bool perguntaLider(int x, int z){
        GameObject[] pessoas = GameObject.FindGameObjectsWithTag("Pessoas");
        for(int i = 0; i < pessoas.Length; i++)
            if(pessoas[i].transform.position.x == x && pessoas[i].transform.position.z == z)
                if(pessoas[i].GetComponent<PessoasScript>().lider == true){
                    //Achei um lider
                    liderSeguido = INFORMA O ID ou UMA REFERENCIA PARA ESSE LIDER
                    return true;
                }
        return false;
    }


    private void procurandoLider(){
        int xlider, zlider;
        int px = ((int)transform.position.x);
        int pz = ((int)transform.position.z);
        int xi = 0;
        int zi = 0;
        int xin = 0;
        int zin = 0;
        int destX = -1;
        int destZ = -1;

        for (int i = 1; i <= visao; i++)
        {
            xi = px + i;
            zi = pz + i;

            xin = px - i;
            zin = pz - i;
            if ((xi < tamX))
            {  //-3 == tem uma pessoa nesse espaço
                if ((mundo[pz][xi] == -3))
                {
                    if(perguntaLider(xi,pz)){
                        achei = true;
                        destX = xi;
                        destZ = pz;
                    }
                }
            }
            if ((zi < tamX))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[zi][px] == -3))
                {
                    if(perguntaLider(px,zi)){
                        achei = true;
                        destX = px;
                        destZ = zi;
                    }
                }
            }
            if ((zin > -1))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[zin][px] == -3))
                {
                    if(perguntaLider(px,zin)){
                        achei = true;
                        destX = px;
                        destZ = zin;
                    }
                }
            }
            if ((xin > -1))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[pz][xin] == -3))
                {
                    if(perguntaLider(xin,pz)){
                        achei = true;
                        destX = xin;
                        destZ = pz;
                    }
                }
                    
            }

            if (achei)
            {
                    //lider.addlistaDeSeguidores(essa pessoa)
                    //Tira ela do Mapa
            }

        }
    }
}
