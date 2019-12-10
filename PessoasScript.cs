using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PessoasScript : MonoBehaviour
{
    public string name;
    public int conti;
    private int tempo = 50; //5x mais lento que os drones
    public string idResgate = "";
    private bool sendoResgatado = false;
    public bool comportRandom = true;
    public bool comportSegueLider = false;
    public bool lider = false;
    public int visao = 1;
    public int limiteCalor;
    public int liderSeguido; //Indica qual o id do lider que deve ser seguido...
    public int[][] mundo;
    private bool achei = false;
    // Variaveis de controle dos resgatados pelo lider
    public ArrayList listaResgatados; //<GameObject>
    public int qntResgatados = 0;
    int muda = 0;

    int menorCaminho = 0;
    int tamX, tamZ;
    int[][] caminhosValidos = new int[][]
    {                                                                                      //z
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//0
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//1
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//2
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//3
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//4
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//5
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//6
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//7
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//8
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//9
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//10
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//11
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//12
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//13
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//14
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//15
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//16
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//17
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//18
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//19
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//20
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//21
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//22
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//23
        new int[] { 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//24
    };

    //Conhecimento do mundo
    GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        listaResgatados = new ArrayList();
        name = this.gameObject.name;
        world = GameObject.FindGameObjectWithTag("Mundo");
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -3); //avisa ao mundo ao esta!
        world.GetComponent<InfoMundo>().resgatados = world.GetComponent<InfoMundo>().getResgatados() + 1;
        mundo = mundo = world.GetComponent<InfoMundo>().getMundo();
        limiteCalor = 50;
        tamX = world.GetComponent<InfoMundo>().mundoX;
        tamZ = world.GetComponent<InfoMundo>().mundoZ;
        //print("Avisei " + world.GetComponent<InfoMundo>().resgatados);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimento
    }

    public void addPessoaLider(GameObject pes)
    {
        listaResgatados.Add(pes);
        qntResgatados++;
        print(message: "Sou o " + this.gameObject.name + " e tenho " + qntResgatados + "resgatados comigos");
    }

    public void limpaLugar()
    {
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), 0);
    }
    public void marcaLugar()
    {
        world.GetComponent<InfoMundo>().addNoMundo(((int)transform.position.x), ((int)transform.position.z), -3);
    }
    public void droneResgate()
    {
        sendoResgatado = true;
    }

    void caminhando()
    {

        //Procura Força bruta
        int xi = (int)transform.position.x;
        int zi = (int)transform.position.z;

        if ((xi + 1 < tamX) && mundo[zi][xi + 1] <= limiteCalor && mundo[zi][xi + 1] != -3 && caminhosValidos[zi][xi + 1] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado 
            xi = xi + 1;
            //print(message: "1ENTREI xi 1" + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();

        }
        else if ((zi - 1 > 0) && mundo[zi - 1][xi] <= limiteCalor && mundo[zi - 1][xi] != -3 && caminhosValidos[zi - 1][xi] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            zi = zi - 1;
            //print(message: "4ENTREI zi-1" + zi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }
        else if ((xi - 1 > 0) && mundo[zi][xi - 1] <= limiteCalor && mundo[zi][xi - 1] != -3 && caminhosValidos[zi][xi - 1] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            xi = xi - 1;
            //print(message: "2ENTREI xi n " + xi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }
        else if ((zi + 1 < tamX) && mundo[zi + 1][xi] <= limiteCalor && mundo[zi + 1][xi] != -3 && caminhosValidos[zi + 1][xi] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            zi = zi + 1;
            //print(message: "3ENTREI zi+1" + zi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }
        else
        {
            //print(message: "menor " + this.gameObject.name);
            menorCaminho = menorCaminho + 1;
        }

        muda++;

        //   }

    }

    void caminhandoOposto()
    {

        //Procura Força bruta
        int xi = (int)transform.position.x;
        int zi = (int)transform.position.z;



        if ((zi - 1 > 0) && mundo[zi - 1][xi] <= limiteCalor && mundo[zi - 1][xi] != -3 && caminhosValidos[zi - 1][xi] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            zi = zi - 1;
            //print(message: "4ENTREI zi-1" + zi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }else if ((zi + 1 < tamX) && mundo[zi + 1][xi] <= limiteCalor && mundo[zi + 1][xi] != -3 && caminhosValidos[zi + 1][xi] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            zi = zi + 1;
            //print(message: "3ENTREI zi+1" + zi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }else if ((xi - 1 > 0) && mundo[zi][xi - 1] <= limiteCalor && mundo[zi][xi - 1] != -3 && caminhosValidos[zi][xi - 1] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado
            xi = xi - 1;
            //print(message: "2ENTREI xi n " + xi + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();
        }else if ((xi + 1 < tamX) && mundo[zi][xi + 1] <= limiteCalor && mundo[zi][xi + 1] != -3 && caminhosValidos[zi][xi + 1] == menorCaminho)
        {  //== 0 , é livre  \ == 1 é ocupado 
            xi = xi + 1;
            //print(message: "1ENTREI xi 1" + " --- " + menorCaminho + " --- " + caminhosValidos[zi][xi]);
            caminhosValidos[zi][xi] = caminhosValidos[zi][xi] + 1;
            menorCaminho = 0;
            limpaLugar();
            transform.position = new Vector3(xi, 0, zi);
            marcaLugar();

        }
        else
        {
            //print(message: "menor " + this.gameObject.name);
            menorCaminho = menorCaminho + 1;
        }
        muda++;
        if (muda > 10)
            muda = 0;

        //   }

    }

    private void FixedUpdate()
    {
        if (conti == tempo)
        {
            if (sendoResgatado == false)
            {
                if (comportRandom == true || lider == true)
                {
                    if (muda < 5)
                        caminhando();
                    else
                        caminhandoOposto();

                    if (!lider)
                        procurandoLider();
                }
                //transform.position = new Vector3(caminhoAS[contStar].x, 0, caminhoAS[contStar].y);
            }
            conti = 0;
        }
        else
        {
            conti++;
        }

    }


    /* Pergunta se a pessoa que foi vista é lider ou não */
    private bool perguntaLider(int x, int z)
    {
        GameObject[] pessoas = GameObject.FindGameObjectsWithTag("Pessoas");
        for (int i = 0; i < pessoas.Length; i++)
            if (pessoas[i].transform.position.x == x && pessoas[i].transform.position.z == z)
                if (pessoas[i].GetComponent<PessoasScript>().lider == true)
                {
                    pessoas[i].GetComponent<PessoasScript>().addPessoaLider(this.gameObject);
                    this.limpaLugar();
                    transform.position = new Vector3(120, 0, 120); //Manda o boneco para o limbo
                    comportSegueLider = true;
                    comportRandom = false;
                    lider = false;

                    return true;
                }
        return false;
    }


    private bool procurandoLider()
    {
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
                    if (perguntaLider(xi, pz))
                    {
                        achei = true;
                        destX = xi;
                        destZ = pz;
                        return achei;
                    }
                }
            }
            if ((zi < tamX))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[zi][px] == -3))
                {
                    if (perguntaLider(px, zi))
                    {
                        achei = true;
                        destX = px;
                        destZ = zi;
                        return achei;
                    }
                }
            }
            if ((zin > -1))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[zin][px] == -3))
                {
                    if (perguntaLider(px, zin))
                    {
                        achei = true;
                        destX = px;
                        destZ = zin;
                        return achei;
                    }
                }
            }
            if ((xin > -1))
            {   //-3 == tem uma pessoa nesse espaço
                if ((mundo[pz][xin] == -3))
                {
                    if (perguntaLider(xin, pz))
                    {
                        achei = true;
                        destX = xin;
                        destZ = pz;
                        return achei;
                    }
                }

            }
        }
        return achei;
    }
}
