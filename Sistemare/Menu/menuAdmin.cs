using System;
using Sistemare.Salones;
namespace Sistemare.Menu
{
    public class menuAdmin : menu
    {
        string selec,selec2,selec3;
        int sala,dia,hora,minuto;
         public void imprimirAdmin()
        {
            //Console.WriteLine("1.Consultar Disponibilidad");
            //Console.WriteLine("2.Reservar salón");
            imprimirInvit();
            Console.WriteLine("3.Consultar estado");
            Console.WriteLine("4.Modificar luz,temperatura,cerradura");
            Console.WriteLine("5.Habilitar/deshabilitar Salón");
            Console.WriteLine("6.Resetiar sistema");
        }
        public void leerAdmin()
        {
            imprimirAdmin();
            //lola.Cargar();
            selec = Console.ReadLine();
            salon Disposa = new salon();
            if(selec=="1"){
                Disposa.imprimirsalas();
                Console.Write("Sala:");
                Disposa.imprimirdispo(int.Parse(Console.ReadLine()));
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerAdmin();
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
                    leerAdmin();
                }
            }
            else if(selec=="3"){
                Console.Write("Ingrese Sala:");
                sala=int.Parse(Console.ReadLine());
                Console.Write("Dia(lunes(0)martes(1)miercoles(2)jueves(3)viernes(4)):");
                dia=int.Parse(Console.ReadLine());
                Console.Write("ingrese tiempo(24hh,mm):\n");
                Console.Write("hora:");
                hora=int.Parse(Console.ReadLine());
                Console.Write("minuto:");
                minuto=int.Parse(Console.ReadLine());
                Disposa.Consu_para(sala,dia,hora,minuto);
                //consultar parametro
                //temperatura
                //estado puertas
                //estado luces
                //estado sala

                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerAdmin();
                }
            }
            else if(selec=="4"){
                Console.Write("Ingrese Sala:");
                sala=int.Parse(Console.ReadLine());
                Console.Write("Dia(lunes(0)martes(1)miercoles(2)jueves(3)viernes(4)):");
                dia=int.Parse(Console.ReadLine());
                Console.Write("ingrese intervalo hora:\n");
                for(int i=0;i<12;i++){
                Console.Write("|{0,2}| {1:D2}:00 hasta {2:D2}:00 |\n",i,i+7,i+8);}
                Console.Write("No hora:");
                hora=int.Parse(Console.ReadLine());
                Console.Clear();
                
                Console.WriteLine("(0)modificar encendido/apagado luz\n");
                Console.WriteLine("(1)Modificar apertura/cierres puertas\n");
                Console.WriteLine("(2)Modificar encendido/apagado aire acondicionado\n");
                Console.WriteLine("(3)Modificar nivel temperatura\n");
                Console.WriteLine("seleccion: ");
                selec3=Console.ReadLine();
                if (selec3=="0"){
                    int temp1,temp2;
                    Console.WriteLine("Estado actual\n");
                    Console.WriteLine("Las luces encienden {0} minutos antes, se apaga {1}minutos despues\n",Disposa.leer_para(sala,dia,hora,4),Disposa.leer_para(sala,dia,hora,5));
                    Console.WriteLine("-------nuevos valores-------");
                    Console.Write("Encendido:");
                    temp1=int.Parse(Console.ReadLine());
                    Console.Write("apagado:");
                     temp2=int.Parse(Console.ReadLine());
                    if(temp1>=0&&temp2>=0&&temp1<60&&temp2<60)
                    {
                    Disposa.mod_cosa(sala,dia,hora,4,temp1);
                    Disposa.mod_cosa(sala,dia,hora,5,temp2);
                    Console.Write("Editado correctamente\n");}
                    else
                    {
                        Console.Write("parametros incorrectos\n");
                    }
                    
                }
                else if (selec3=="1"){
                    int temp1,temp2;
                    Console.WriteLine("Estado actual\n");
                    Console.WriteLine("Las puertas abren {0} minutos antes, se cierran {1} minutos despues\n",Disposa.leer_para(sala,dia,hora,2),Disposa.leer_para(sala,dia,hora,3));
                    Console.WriteLine("-------nuevos valores-------");
                    Console.Write("apertura:");
                    temp1=int.Parse(Console.ReadLine());
                    Console.Write("cierre:");
                    temp2=int.Parse(Console.ReadLine());
                    if(temp1>=0&&temp2>=0&&temp1<60&&temp2<60)
                    {
                    Disposa.mod_cosa(sala,dia,hora,2,temp1);
                    Disposa.mod_cosa(sala,dia,hora,3,temp2);
                    Console.Write("Editado correctamente\n");}
                    else
                    {
                        Console.Write("parametros incorrectos\n");
                    }
                }
                else if (selec3=="2"){
                    int temp1,temp2;
                    Console.WriteLine("Estado actual");
                    Console.WriteLine("El aire acondicionado enciende {0} minutos antes, se apaga {1} minutos despues\n",Disposa.leer_para(sala,dia,hora,6),Disposa.leer_para(sala,dia,hora,7));
                    Console.WriteLine("-------nuevos valores-------");
                    Console.Write("encendido:");
                    temp1=int.Parse(Console.ReadLine());
                    Console.Write("apagado:");
                    temp2=int.Parse(Console.ReadLine());
                    if(temp1>=0&&temp2>=0&&temp1<60&&temp2<60)
                    {
                    Disposa.mod_cosa(sala,dia,hora,6,temp1);
                    Disposa.mod_cosa(sala,dia,hora,7,temp2);
                    Console.Write("Editado correctamente\n");}
                    else
                    {
                        Console.Write("parametros incorrectos\n");
                    }  
                }
                else if (selec3=="3"){
                    int temp1;
                    Console.WriteLine("Estado actual");
                    Console.WriteLine("La temperatura es:{0}\n",Disposa.leer_para(sala,dia,hora,1));
                    Console.WriteLine("-------nuevos valores-------");
                    Console.Write("Temperatura(14°-27°):");
                    temp1=int.Parse(Console.ReadLine());
                    if(temp1>=14&&temp1<28)
                    {
                    Disposa.mod_cosa(sala,dia,hora,1,temp1);
                    Console.Write("Editado correctamente\n");}
                    else
                    {
                        Console.Write("parametros incorrectos\n");
                    }  
                }
                else
                {
                    Console.Write("Opcion no valida\n");
                }

                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerAdmin();
                }
            }
            else if(selec=="5"){
                Console.Write("Ingrese Sala:");
                sala=int.Parse(Console.ReadLine());
                Console.Write("Dia(lunes(0)martes(1)miercoles(2)jueves(3)viernes(4)):");
                dia=int.Parse(Console.ReadLine());
                Console.Write("ingrese intervalo hora:\n");
                for(int i=0;i<12;i++){
                Console.Write("|{0,2}| {1:D2}:00 hasta {2:D2}:00 |",i,i+7,i+8);
                int value=Disposa.leer_para(sala,dia,i,0);
                string esta;
                if(value==0){esta="Disponible";}
                else if(value==1){esta="En clase";}
                else if(value==2){esta="Reservado";}
                else if(value==3){esta="Deshabilitado";}
                else{esta="nonas";}
                Console.Write(" {0,14} |\n",esta);
                }
                
                Console.Write("No hora:");

                hora=int.Parse(Console.ReadLine());
                Console.Write("Habilitar(0) desabilitar(1)");
                int selo=int.Parse(Console.ReadLine());
                Console.Clear();
                if (selo==0){Disposa.mod_cosa(sala,dia,hora,0,0);Console.Write("Operacion satisfactoria\n");}
                else if (selo==1){Disposa.mod_cosa(sala,dia,hora,0,3);Console.Write("Operacion satisfactoria\n");}
                else{Console.Write("Opcion invalida\n");}  
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerAdmin();
                }           
            }
            else if(selec=="6"){
                Disposa.Restart_Initial();
            }
            else{
                Console.Write("Opcion incorrecta");
                Console.Write("Volver menu(x) \n");
                selec2=Console.ReadLine();
                if(selec2=="x"){
                    leerAdmin();
                }
            }
        }
    }
}