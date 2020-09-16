using System;
using Sistemare.Salones;
namespace Sistemare.Menu
{
    public class menuInvitado : menu
    {
        string selec,selec2;
        int sala,hora,dia;
        public void leerinv()
        {
            //lola.Cargar();
            imprimirInvit();
            selec = Console.ReadLine();
            salon Disposa = new salon();
            if(selec=="1"){
                Disposa.imprimirsalas();
                Console.Write("Sala:");
                Disposa.imprimirdispo(int.Parse(Console.ReadLine()));
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerinv();
                }
            }
            else if(selec=="2"){
                //Disposa.imprimirsalas();
                //Disposa.imprimirdispo(int.Parse(Console.ReadLine()));
                Console.Write("Ingrese numero de sala y horario(No) \n");
                Console.Write("Sala:");
                sala=int.Parse(Console.ReadLine());
                Disposa.imprimirdispo(sala);
                Console.Write("Dia:");
                dia=int.Parse(Console.ReadLine());
                Console.Write("Horario(No):");
                hora=int.Parse(Console.ReadLine());
                Disposa.modi_dispo(sala,dia,hora,2);
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerinv();
                }
            }
            else{
                Console.Write("Opcion incorrecta");
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerinv();
                }
            }
        }
    }
}