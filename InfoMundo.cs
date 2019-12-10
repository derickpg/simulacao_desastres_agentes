using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMundo : MonoBehaviour
{
	public int tempoExec = 0;
	Vector2 casa = new Vector2(0, 12);
	public int mundoX = 25; //25 quadrados de X
	public int mundoZ = 25; //25 quadrados de Z
	public int limiteCalor = 50; //Limite de calor suportado pelos drones e humanos, acima disso o lugar 
								 // não é acessado.
	public Vector2 getCasa()
	{
		return casa;
	}

	int[][] mundo = new int[][]
	   {                                                                                      //z
        new int[] { 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100},//0
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
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 100, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//11
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//12
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//13
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//14
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//15
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//16
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//17
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//18
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//19
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//20
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//21
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//22
        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},//23
        new int[] { 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100},//24
// x                0  1  2  3  4   5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24
       };

    public void printMundo()
    {
        string aux = "0  1  2  3  4   5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 \n";
        
        for (int z = 0; z < mundoZ; z++) {
            for (int x = 0; x < mundoX; x++)
            {
                aux = aux + mundo[z][x] + ", ";
            }
            aux = aux + "" + z + "\n";
        }
        print(message: "" + aux);
    }

	public int[][] getMundo()
	{
		return mundo;
	}

	public void addNoMundo(int x, int z, int info)
	{
		mundo[z][x] = info;
		//print(message: "MUNDO ATUALIZADO " + mundo[z][x] + " z = " +z + " x= " +x);
	}

    public void addFogoMundo(int x, int z, int info)
    {
        //print(message: "erro pq Z=" + z + " X=" + x);
        if (mundo[z][x] < info) //caso o fogo que tenha no mundo não seja mais forte
            if (!(mundo[z][x] == -3)) //Caso não seja uma pessoa
                mundo[z][x] = info;
    }

	public int resgatados = 1;

	public int getResgatados()
	{
		return resgatados;
	}
	public void setResgatados(int info)
	{
		resgatados = info;
	}



	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("contabiliza", 1f, 1f);
	}

	private void contabiliza()
	{
		tempoExec++;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void FixedUpdate()
	{
        if (resgatados == 0)
		{
			print(message: "FIM -> TEMPO DE EXECUÇÃO: " + tempoExec + " Segundos");
			resgatados = -1;
		}
	}

}
