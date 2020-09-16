using System;
using Sistemare.Estado;

namespace Sistemare.Salones
{
    public class salon{
        int value;
        string esta;
        public void imprimirdispo(int sala){
            Console.Clear();
            Console.Write("-----------------Sala {0}------------------ \n",sala);
            Console.Write("|No| {0,17} | {1,14} | {2,14} | {3,14} | {4,14} | {5,14} | \n","intervalo","Lunes(0)","martes(1)","miercoles(2)","jueves(3)","viernes(4)");
            estados Aula = new estados();
            
            for(int i=0;i<12;i++){
                Console.Write("|{0,2}| {1:D2}:00 hasta {2:D2}:00 |",i,i+7,i+8);
                for(int j=0;j<Aula.cd;j++){
                value=Aula.consutadispo(sala,j,i);
                if(value==0){esta="Disponible";}
                else if(value==1){esta="En clase";}
                else if(value==2){esta="Reservado";}
                else if(value==3){esta="Deshabilitado";}
                else{esta="nonas";}
                Console.Write(" {0,14} |",esta);
                }
                Console.Write("\n");

            }
        }
        public void imprimirsalas(){
            Console.Clear();
            Console.Write("-------Edificio------ \n");
            estados Aula = new estados();
            for(int i=0;i<Aula.cs;i++){
                Console.Write("|{0}. | Sala {1} | \n",i,i);
            }
        }
        public void modi_dispo(int sala,int dia, int hor,int repla){
            estados Aula = new estados();
            int val;
            val=Aula.consutadispo(sala,dia,hor);
            if(hor>=0&&hor<12&& dia <Aula.cd&&sala <Aula.cs){
                if (val==0){
                    Aula.modificar_parametro(sala,dia,hor,0,repla);
                    Aula.modificar_parametro(sala,dia,hor,4,5);
                    Aula.modificar_parametro(sala,dia,hor,5,10);
                    Aula.modificar_parametro(sala,dia,hor,6,10);
                    Aula.modificar_parametro(sala,dia,hor,7,5);
                    Aula.modificar_parametro(sala,dia,hor,2,5);
                    
                    if(hor>0){
                        Aula.modificar_parametro(sala,dia,hor-1,3,1);
                    }
                    Console.Write("Desea refrigerio? si(1) no(0):");
                    int op;
                    op=int.Parse(Console.ReadLine());
                    if(op==1||op==0)
                    Aula.modificar_parametro(sala,dia,hor,8,op);
                    else{Aula.modificar_parametro(sala,dia,hor,8,0);}
                }
                else{
                    Console.Write("No es posible realizar la operacion,debido a que  no esta disponible| \n");

                }
            }
            else{Console.Write("Parametro de entrada no valido");}
        }
        public void Consu_para(int sala,int dia, int hor, int minuto){
            estados Aula = new estados();
            Aula.Consultar_parametros(sala,dia,hor,minuto);
        }
        public int leer_para(int sala,int dia, int hor, int para){
            estados Aula = new estados();
            return Aula.leer_parametro(sala,dia,hor,para);
        }
        public void mod_cosa(int sala,int dia, int hor,int par,int remp){
            estados Aula = new estados();
            Aula.modificar_parametro(sala,dia, hor,par, remp);
        }
        public void Restart_Initial()
        {
            estados Aula = new estados();
            Aula.Restart();
        }
    }
}