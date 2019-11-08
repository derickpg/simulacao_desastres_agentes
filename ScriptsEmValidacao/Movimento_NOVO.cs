using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Movimento : MonoBehaviour
{
    public int conti;
    public int tempo = 10;
    public int tamX = 25; //25 quadrados de X
    public int tamZ = 25; //25 quadrados de Z
    public int resgatados;//numero de pessoas para serem resgatadas
    public int visao = 4; // quantos quadrados a sua volta ele olha! 
    public int destx;
     // Limite de calor que o drone considera seguro, mais que isso o caminho é invalido!
     public int limiteCalor; //definido pelo mundo
    public int desty;
    GameObject resgatado = null;
    Vector2 casa = new Vector2(0,12);
    GameObject pessoa;
    int caminhoPessoa = 0;
    bool achei = false;
    int menorCaminho = 0;
    int objX;
    int objZ;
    Boolean aStar = false;
    List<Vector2> caminhoAS;
    int contStar = 0;
    Boolean resgate = false;
    int[][] mundo;
    GameObject world;

    //Variaveis da Bateria
    int bateria = 100; //porcentagem de bateria 
    int autonomia = 2; //Define a autonomia da bateria (quantos tempos++ consome 1 porcento)
    int contAutonomia = 0; //Variavel que conta a autonomia
    bool carregar = false; //Marca quando tem que carregar
    bool retornarCarregar = false; //avisa que esta retornando para carregar
    bool basep = false; //Avisa quando chegou na base e pode carregar


    void Start()
     {
        world = GameObject.FindGameObjectWithTag("Mundo");
        mundo = world.GetComponent<InfoMundo>().getMundo();
        resgatados = world.GetComponent<InfoMundo>().getResgatados();
        limiteCalor = world.GetComponent<InfoMundo>().limiteCalor;
     }

    GameObject marcaResgatado(int x,int z)
    {
        GameObject[] resg = GameObject.FindGameObjectsWithTag("Pessoas");
        Vector3 pos = new Vector3(x, 0, z);
        for(int i = 0; i < resg.Length; i++)
        {
            if (resg[i].transform.position == pos)
            {
                if (resg[i].GetComponent<PessoasScript>().idResgate == "")
                {
                    //print(message: "OPa entrei");
                    resg[i].GetComponent<PessoasScript>().sendoResgatado = true;
                    resg[i].GetComponent<PessoasScript>().idResgate = this.gameObject.name;
                }
                    return resg[i];
            }
        }
        return null;
    }

    
    void olhando()
    {
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
            //print(message: "[DEBUG] Olhando => xi" + xi + " zi" + zi + " zin" + zin + " xin" + xin + " i " + i);
            if ((xi < tamX))
            {  //== 0 , é livre  \ == 1 é ocupado 
                if ((mundo[pz][xi] == -3))
                {
                    achei = true;
                    destX = xi;
                    destZ = pz;
                }
            }
            if ((zi < tamX))
            {  //== 0 , é livre  \ == 1 é ocupado
                if ((mundo[zi][px] == -3))
                {
                    achei = true;
                    destX = px;
                    destZ = zi;
                }
            }
            if ((zin > -1))
            {  //== 0 , é livre  \ == 1 é ocupado]
                if ((mundo[zin][px] == -3))
                {
                    achei = true;
                    destX = px;
                    destZ = zin;
                }
            }
            if ((xin > -1))
            {  //== 0 , é livre  \ == 1 é ocupado
                if ((mundo[pz][xin] == -3))
                {
                    achei = true;
                    destX = xin;
                    destZ = pz;
                }
                    
            }

            if (achei)
            {
               // mundo = world.GetComponent<InfoMundo>().getMundo(); //fica atualizando o mundo
               // if (mundo[destZ][destX] == -3)
                //{
                    objX = destX;
                    objZ = destZ;
                    resgatado = marcaResgatado(objX, objZ);
                    //print(message: "resgtad " + resgatado);
                    //if (resgatado == null)
                      //  achei = false;
                //}
            }

        }
    }

    private class Coordenada
    {
        int x;
        int z;
        Coordenada pai = null;
        public int f; //g + h
        int g = 0; //Distanca para o inicio
        int h = 0; //Distancia para o fim

        public Coordenada(int xa,int za)
        {
            x = xa;
            z = za;
        }
        public int getX()
        {
            return x;
        }
        public int getZ()
        {
            return z;
        }
        public int getH()
        {
            return h;
        }
        public int getF()
        {
            return f;
        }
        public int getG()
        {
            return g;
        }
        public Coordenada getPai()
        {
            return pai;
        }
        public void setX(int aux)
        {
            x = aux;
        }
        public void setZ(int aux)
        {
            z = aux;
        }
        public void setH(int aux)
        {
            h = aux;
        }
        public void setF(int aux)
        {
            f = aux;
        }
        public void setG(int aux)
        {
            g = aux;
        }
        public void setPai(Coordenada aux)
        {
            pai = aux;
            // 10 * (abs (currentX-targetX) + abs (currentY-targetY))
            g = ((10 * ((x - pai.getX()) + (z - pai.getZ()))));
        }
    }

    private Boolean jaFoi(int x, int z)
    {
        for(int u = 0; u < aberta.Count; u++)
        {
            if ((aberta[u].getX() == x) && (aberta[u].getZ() == z))
                return true;
        }
        for (int w = 0; w < fechada.Count; w++)
        {
            if ((fechada[w].getX() == x) && (fechada[w].getZ() == z))
                return true;
        }
        return false;
    }

    private List<Coordenada> getVizinhos(Coordenada p)
    {
        List<Coordenada> retorno = new List<Coordenada>();
        int xi = p.getX();
        int zi = p.getZ();

         /*print(message: " X+1 -> " + (xi + 1) + "," + zi +
            " X-1 -> " + (xi - 1) + "," + zi +
            " z+1 -> " + xi + "," + (zi + 1 ) +
            " z-1 -> " + (xi) + "," + (zi - 1)
            );*/


        if ((xi + 1 < tamX) && ((mundo[zi][xi + 1] != -1)))
        {
            //print(message: "x+1 -> " + (xi+1) + "," + zi);
            if (!jaFoi(xi+1,zi)) { 
                Coordenada nova1 = new Coordenada(xi + 1, zi);
                nova1.setPai(p);
                retorno.Add(nova1);
            }
        }
        if ((zi + 1 < tamX) && (mundo[zi + 1][xi] != -1))
        {
            //print(message: "Z+1 -> " + xi + "," + (zi + 1));
            if (!jaFoi(xi, zi + 1))
            {
                Coordenada nova2 = new Coordenada(xi, zi + 1);
                nova2.setPai(p);
                retorno.Add(nova2);
            }
        }
        if ((zi - 1 > -1) && (mundo[zi - 1][xi] != -1))
        {
            //print(message: "Z-1 -> " + xi + "," + (zi - 1));
            if (!jaFoi(xi, zi - 1))
            {
                //print(message: "Z-1 -> " + xi + "," + (zi - 1));
                Coordenada nova3 = new Coordenada(xi, zi - 1);
                nova3.setPai(p);
                retorno.Add(nova3);
            }
        }
        if ((xi - 1 > -1) && (mundo[zi][xi - 1] != -1))
        {
            //print(message: "X-1 -> " + (xi-1) + "," + zi);
            if (!jaFoi(xi - 1, zi))
            {
                //print(message: "X-1 -> " + (xi - 1) + "," + zi);
                Coordenada nova4 = new Coordenada(xi - 1, zi);
                nova4.setPai(p);
                retorno.Add(nova4);
            }
        }



        return retorno;
    }


    List<Coordenada> aberta = new List<Coordenada>();
    List<Coordenada> fechada = new List<Coordenada>();

    List<Vector2> aEstrela(int xOrigem, int zOrigem, int xDestino, int zDestino)
    {
        mundo = world.GetComponent<InfoMundo>().getMundo(); //fica atualizando o mundo
        //print(message: "[DEBUG] INICIANDO A ESTRELA ORIGEM " + xOrigem +","+ zOrigem + " Destino " + xDestino + "," + zDestino);
        aberta.Clear();
        fechada.Clear();
        List<Coordenada> vizinhos = new List<Coordenada>();
        List <Vector2> caminho = new List<Vector2>();
        aberta.Add(new Coordenada(xOrigem, zOrigem));
        Coordenada fim = null;
        int k = 0;
        while (aberta.Count > 0)
        {
            //print(message: "VOu colocar os vizinhos do " + aberta[k].getX() + "," + aberta[k].getZ());
            vizinhos.Clear();
            vizinhos = getVizinhos(aberta[k]);
            for (int i = 0; i < vizinhos.Count; i++)
            {

                //print(message: "OLHANDO VIZINHOS DO " + aberta[k].getX() + " " + aberta[k].getZ() + " ----------- " + i);
               // print(message: "[DEBUG] tamanho aberta " + aberta.Count);
                if ((vizinhos[i].getX() == xDestino) && (vizinhos[i].getZ() == zDestino)) // um vizinho é o final
                {
                   // print(message: "[DEBUG] colocando fim");
                    fim = vizinhos[i];
                    break;
                }
                else
                {
                    // 10 * (abs (currentX-targetX) + abs (currentY-targetY))
                    int novoh = Math.Abs((10 * ((vizinhos[i].getX() - xDestino) + (vizinhos[i].getZ() - zDestino))));
                    //print(message: "[DEBUG] F do (" + vizinhos[i].getX() + "," + vizinhos[i].getZ() + ")" + (10 * ((vizinhos[i].getX() - xDestino) + (vizinhos[i].getZ() - zDestino))));
                    vizinhos[i].setG(aberta[k].getF() + 1);
                   vizinhos[i].setH(novoh);
                    vizinhos[i].setF(Math.Abs((vizinhos[i].getH() + vizinhos[i].getG())));
                    aberta.Add(vizinhos[i]);
                    //print(message: "[DEBUG] VIZINHO ADD " + vizinhos[i].getX() + " " + vizinhos[i].getZ() + " F = " + vizinhos[i].f);
                    /*  aberta.Sort(delegate (Coordenada c1, Coordenada c2) //ordena a lista aberta
                      {
                          return c1.getF().CompareTo(c2.getF());
                      });  */
                    //people.OrderBy(person => person.LastName);
                    //print(message: "prox aberta " + aberta[1].getX() + " " + aberta[1].getZ());
                }
            }
            if (fim == null)
            {
                fechada.Add(aberta[k]);
                aberta.RemoveAt(k);
                aberta = aberta.OrderBy(Coordenada => Coordenada.f).ToList<Coordenada>();
            }
            else
            {
                //print(message: "[DEBUG] BREAK");
                break;
            }
        }

        //agora com o fim definido vamos voltando entre os pais até encontrar o inicio
        // o inicio tem pai null
        Coordenada volta = fim;
        //print(message: "Vou marcar o caminho" + caminho.Count + " " + (volta == null));
        while (!(volta == null)) // enquanto não for null a volta
        {
            //print(message: "ANOTANDO CAMINHO");
            caminho.Add(new Vector2(volta.getX(), volta.getZ()));
            volta = volta.getPai();
        }
        //print(message: "A ESTRELA CAMINHO FINALIZADO!" + caminho.Count + " " + volta);
        return caminho;
    }


    void procurando()
    {

        //Procura Força brut
        int xi = (int)transform.position.x;
        int zi = (int)transform.position.z;
        //print(message: "[DEBUG] FrocaBruta => " + lado + " MenorCaminho =>" + menorCaminho);


        if (!achei)
        {
            olhando();
            //print(message: "[DEBUG] olhando " + this.gameObject.name);
        }

        if (achei)
        {
            //print(message: "[DEBUG] achei ? " + achei + " objetivo = " + objX + " " + objZ + " --- " + this.gameObject.name);
            mundo = world.GetComponent<InfoMundo>().getMundo(); //fica atualizando o mundo
            caminhoAS = aEstrela(xi, zi, objX, objZ);
            //print(message: "[DEBUG] caminhoAS " + caminhoAS.Count);
            contStar = caminhoAS.Count - 1;
            aStar = true;
            resgate = true;
            //print(message: "[DEBUG] contStar " + contStar);
            //resgatado.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if ((xi + 1 < tamX) && ((mundo[zi][xi + 1] == menorCaminho)) && mundo[zi][xi+1] < limiteCalor)
            {  //== 0 , é livre  \ == 1 é ocupado 
                xi = xi + 1;
                // print(message: "1ENTREI xi 1");
                menorCaminho = 0;
                transform.position = new Vector3(xi, 0, zi);
            }
            else if ((zi - 1 >= 0) && (mundo[zi - 1][xi] == menorCaminho) && mundo[zi-1][xi] < limiteCalor)
            {  //== 0 , é livre  \ == 1 é ocupado
                zi = zi - 1;
                //print(message: "4ENTREI zi-1" + zi);
                menorCaminho = 0;
                transform.position = new Vector3(xi, 0, zi);
            }
            else if ((xi - 1 >= 0) && (mundo[zi][xi - 1] == menorCaminho) && mundo[zi][xi-1] < limiteCalor)
            {  //== 0 , é livre  \ == 1 é ocupado
                xi = xi - 1;
                //print(message: "2ENTREI xi n " + xi);
                menorCaminho = 0;
                transform.position = new Vector3(xi, 0, zi);
            }
            else if ((zi + 1 < tamX) && (mundo[zi + 1][xi] == menorCaminho) && mundo[zi+1][xi] < limiteCalor)
            {  //== 0 , é livre  \ == 1 é ocupado
                zi = zi + 1;
                //print(message: "3ENTREI zi+1" + zi);
                menorCaminho = 0;
                transform.position = new Vector3(xi, 0, zi);
            }
            else
            {
            //print(message: "menor " + this.gameObject.name);
                menorCaminho = menorCaminho + 1;
            }
            // Só atualiza se ainda puder passar ou se o calor é suportado
            if(mundo[zi][xi] + 1 < limiteCalor) //se o drone suporta o calor
                mundo[zi][xi] = mundo[zi][xi] + 1;  // atualiza o mundo por onde já passou! */

     //   }

        conti = 0;
    }


    private void FixedUpdate()
    {

        if (conti == tempo)
        {
	    //BATERIA
	    contAutonomia++;
	    if(autonomia >= contAutonomia) //Se chegamos na autonomia, tira 1 porcento da bateria
		bateria--;
	    if(autonomia <= 20) //Se a bateria tiver menos de 20 porcento, ele volta para a base para carregar
		if ((contStar >= 0) && (!resgate)){ //Se ja esta indo para base por causa do resgate
			//Marca que tem que carregar		
			carregar = true;
		}else{ 
			//agora ele tem que retonar para a base
				// TEM QUE FAZER O A*
			    caminhoAS.Clear();
                            caminhoAS = aEstrela((int)transform.position.x, (int)transform.position.z, (int)casa.x, (int)casa.y);
                            contStar = caminhoAS.Count - 1;
			//Marca que está retornando para base para carregar
			retornarCarregar = true;
			//marca que tem que carregar	
			carregar = true;	
		}
		

            if(carregar && pbase){//Se ele esta na base e tem que carregar
		retornarCarregar = false; //tira a flag que tem que voltar por que esta aqui ja
	    	//Fica parado carregando de acordo com a autonomia *2
		if(100 <= (bateria + (autonomia*2)) //Se ainda nao ta 100 ou maior 
			bateria = bateria + (autonomia*2); //carrega
	        else{ //chegou a 100 porcento
			bateria = 100; 
			carregar = false; 
		}
	    }else if(retornarCarregar){
		//tem que ler o vetor do a* para ir carregar na base
		transform.position = new Vector3(caminhoAS[contStar].x, 0, caminhoAS[contStar].y);
		contStar = contStar - 1;
		if(contStar == =1){
			pbase = true; //flag de que chegou na base
			retornarCarregar = false; //desativa a flag de caminho para carregar
		}
			
	    }else{	 //Se ele nao esta na base e nao tem que carregar ele faz o resto!
            

            mundo = world.GetComponent<InfoMundo>().getMundo(); //fica atualizando o mundo
            if (!aStar)
            {
                //print(message: "[DEBUG] procurando " + this.gameObject.name);
                if (world.GetComponent<InfoMundo>().getResgatados() > 0)
                {
                    //print(message: "[DEBUG] procurando sim " + this.gameObject.name);
                    pbase = false;
		    procurando();
                }
            }
            else
            {
                if ((contStar >= 0) && resgate) //Aproximando para resgate
                {
                    //print(message: "[DEBUG] indo buscar");
                    transform.position = new Vector3(caminhoAS[contStar].x, 0, caminhoAS[contStar].y);
                    contStar = contStar - 1;
                    //print(message: "[DEBUG] indo buscar + " + contStar);
                    if (contStar < 0) //avisa que acabou o resgate e agora vai pra base
                    {
                        //print(message: "[DEBUG] ENTREI E VOU VER O CAMINHO DE VOLTA!");
                        mundo = world.GetComponent<InfoMundo>().getMundo(); //fica atualizando o mundo
                        pessoa = marcaResgatado(((int)transform.position.x), ((int)transform.position.z));
                        if (pessoa != null && pessoa.GetComponent<PessoasScript>().idResgate == this.gameObject.name)
                        {
                            pessoa.GetComponent<PessoasScript>().limpaLugar(); //Marca no mundo que o lugar esta vazio
                            resgate = false;
                            caminhoAS.Clear();
                            caminhoAS = aEstrela((int)transform.position.x, (int)transform.position.z, (int)casa.x, (int)casa.y);
                            contStar = caminhoAS.Count - 1;
                            //print(message: "[DEBUG] avisando retorno ContStar " + contStar + " resgate = " + resgate);
                        }
                        else
                        {
                            //print(message: "[DEBUG] Alguem chegou antes " + this.gameObject.name);
                            aStar = false;
                            achei = false;
                            resgate = false;
                            caminhoPessoa = 0;
                            contStar = -1;
                            menorCaminho = 0;
                            world = GameObject.FindGameObjectWithTag("Mundo");
                            mundo = world.GetComponent<InfoMundo>().getMundo();
                            resgatados = world.GetComponent<InfoMundo>().getResgatados();
                        }
                    }
                }
                else if ((contStar >= 0) && (!resgate)) //Indo para a base
                {
                    //print(message: "[DEBUG] Voltando "+ contStar);
                    transform.position = new Vector3(caminhoAS[contStar].x, 0, caminhoAS[contStar].y);
                    if (caminhoPessoa > 0)
                        pessoa.transform.position = new Vector3(caminhoAS[contStar + 1].x, 0, caminhoAS[contStar + 1].y);
                    contStar = contStar - 1;
                    caminhoPessoa++;
                    if (contStar == -1)
                    {
                        pessoa.transform.position = new Vector3(caminhoAS[contStar + 1].x, 0, caminhoAS[contStar + 1].y);
                        if(pessoa.GetComponent<PessoasScript>.lider){ // a pessoa resgatada é lider!
                            qnt = pessoa.GetComponent<PessoasScript>.qntResgatados; //Vê quantos seguiam o lider
                            // avisa que todos esses foram resgatados
                            world.GetComponent<InfoMundo>().resgatados = world.GetComponent<InfoMundo>().resgatados - qnt;
                        }else{ // a pessoa resgatada não é lider
                            // tira só o único que foi resgatado
                            world.GetComponent<InfoMundo>().resgatados = world.GetComponent<InfoMundo>().resgatados - 1;
                        }
                        resgatados = world.GetComponent<InfoMundo>().getResgatados();
                        //print(message: "RESGATADOS = " + resgatados);
                        if (resgatados > 0)
                        {
                            //print(message: "Chegamos");
                            //print(message: "RESGATADOS = " + resgatados + " world resgatados" + world.GetComponent<InfoMundo>().resgatados + " e geet " + world.GetComponent<InfoMundo>().getResgatados());
                            aStar = false;
                            achei = false;
                            resgate = true;
			                basep = true;
                            caminhoPessoa = 0;
                            world = GameObject.FindGameObjectWithTag("Mundo");
                            mundo = world.GetComponent<InfoMundo>().getMundo();
                            resgatados = world.GetComponent<InfoMundo>().getResgatados();
                        }
                    }
                }
            }
	    }

            conti = 0;
        }
        else {
            conti++;
        }

    }
}
