using System;
using System.IO;
namespace Sistemare.Estado
{
    public class estados{   
        private const string FILE_NAME = "Edificio.data";
        //cs: cantidad de salones
        //ch: cantidad de parametros
        //cd: cantidad de dias
        //cierredia: Tiempo de apagado luces,aire  acondicionado despues de las 7pm
        //aperdia: Tiempo de apertura de luces,puertas,temperatura antes de las 7am
        public int cs=4;
        public int ch=9,cd=5,cierredia=5,aperdia=15;
        public void Restart()
        {
            if (File.Exists(FILE_NAME))
            {
                Console.WriteLine($"{FILE_NAME} existimos");
                //return;
            }
            int[][] Salon1 = new int[cs][];
            for(int k=0;k<cs;k++)
            {   
                Salon1[k] = new int[cd*ch*12+1];
                Salon1[k][0]=400+k;
                Random rnd = new Random();
                int anterior=0;
                for(int i=1;i<Salon1[0].Length;i++)
                {
                    //estado salon
                    int temp=0,co=0,res=rnd.Next(0, 2);
                    //comprovacion de uso
                    if(res==1){ co=1;}
                    if (anterior==0 && (i-1)%(12*ch)!=0){Salon1[k][i-ch+3]=15*co;}
                    Salon1[k][i++]=res;
                    //temperatura 
                    Salon1[k][i++]=23;
                    //inicipuertas
                    if(co==1||anterior==1){temp=1;}
                    Salon1[k][i++]=5*temp;
                    //fin puertas
                    Salon1[k][i++]=0;
                    //encendido luces
                    Salon1[k][i++]=5*co;
                    //apagado luces
                    Salon1[k][i++]=10*co;
                    //encendido aire acondicionado
                    Salon1[k][i++]=10*co;
                    //apagado aire acondicionado
                    Salon1[k][i++]=5*co;
                    //refrigerio
                    Salon1[k][i]=co*rnd.Next(0, 2);
                    anterior=res;       
                }
            }
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                { 
                    w.BaseStream.Position=0;
                    for(int i=0;i<cs;i++)
                    {

                        for(int j=0;j<Salon1[0].Length;j++){
                            w.Write(Salon1[i][j]);
                        }
                    }
                }
            }
        }
        
        public int consutadispo(int sala, int dia, int hor){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {   
                    //r.PeekChar();
                    r.BaseStream.Position = 4*(1+hor*ch+12*ch*dia+ (1+12*ch*cd)*sala);
                    return r.ReadInt32();
                }
            }
        }
        public void modificar_parametro(int sala,int dia, int hor, int para,int repla){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                { 
                    w.BaseStream.Position=4*(1+para+hor*ch+12*ch*dia+ (1+12*ch*cd)*sala);
                    w.Write(repla);
                }
            }
        }
        public int leer_parametro(int sala,int dia, int hor, int para){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                { 
                    r.BaseStream.Position=4*(1+para+hor*ch+12*ch*dia+ (1+12*ch*cd)*sala);
                    return r.ReadInt32();
                }
            }
        }
        
        private int Consultar_puertas(int sala,int dia, int hor, int minuto){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {   //Apuertas: Apertura puertas en el intervalo actual,Cpuertas Cierre de fin intervalo actual
                    int Apuertas,Cpuertas,Estatus;
                    if(hor==-1){
                        Estatus=leer_parametro(sala,dia,hor+1,0);
                        //Console.WriteLine("pop:{0}",Estatus);
                        if((Estatus==1||Estatus==2)&&minuto>(60-aperdia)){return 1;}
                    }
                    if(hor==12){
                        Estatus=leer_parametro(sala,dia,hor-1,0);
                        if((Estatus==1||Estatus==2)&&minuto<cierredia){return 1;}
                    }
                    if(hor<12&&hor>-1){
                        Cpuertas=leer_parametro(sala,dia,hor,3);
                        Apuertas=leer_parametro(sala,dia,hor,2);
                        //Console.Write("hungry: {0},{1}\n",Cpuertas,Apuertas);
                        //Puertas: 0 cerradaas 1 abiertas
                        if(Apuertas>minuto||minuto>(60-Cpuertas)){
                            return 1;
                        } 
                    }
                    return 0;
                }
            }
        }
        private int Consultar_luces(int sala,int dia, int hor, int minuto, int Estatus){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {   //Eluces: encendido del intervalo siguiente, Aluces: apagado intervalo anterior
                    //0 apagadas 1 encendidaas
                    int Eluces,Aluces;
                    
                    Eluces=leer_parametro(sala,dia,hor+1,4);
                    Aluces=leer_parametro(sala,dia,hor-1,5);
                    
                    //Puertas: 0 cerradaas 1 abiertas
                    if(hor==-1&&minuto>(60-Eluces)){return 1;}
                    if(hor==12&&Aluces>minuto){return 1;}
                    if(hor>-1&&hor<12&&(Estatus==1||Estatus==2)){return 1;}
                    if(hor==0&&Estatus==0){
                       
                        if(minuto>(60-Eluces)){
                            return 1;
                        }
                    }
                    if(hor==11&&Estatus==0){
                    
                        if(Aluces>minuto){
                            return 1;
                        }
                    }
                    else if(hor>-1&&hor<12&&Estatus==0){
                        
                        if( Aluces>minuto||minuto>(60-Eluces)){
                            return 1;
                        }
                    }
                    return 0;

                    
                }
            }
        }
        private int Consultar_temp(int sala,int dia, int hor, int minuto, int Estatus){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {   //Etemp: encendido del intervalo siguiente, Atemp: apagado intervalo anterior
                    //0 apagadas 1 encendidaas
                    int Etemp,Atemp,temp;
                    Etemp=leer_parametro(sala,dia,hor+1,6);
                    Atemp=leer_parametro(sala,dia,hor-1,7);
                    temp=leer_parametro(sala,dia,hor,1);
                    //Puertas: 0 cerradaas 1 abiertas
                    if(hor==-1&&minuto>(60-Etemp)){return temp;}
                    if(hor==12&&Atemp>minuto){return temp;}
                    if(hor>-1&&hor<12&&(Estatus==1||Estatus==2)){return temp;}
                    //Console.Write("verini: {0},{1},{2}\n",Atemp,Etemp,Estatus);
                    if(hor==0&&Estatus==0){
                        if(minuto>(60-Etemp)){
                            return temp;
                        }
                    }
                    if(hor==11&&Estatus==0){
                        if(Atemp>minuto){
                            return temp;
                        }
                    }
                    else if(hor>-1&&hor<12&&Estatus==0){
                        if( Atemp>minuto||minuto>(60-Etemp)){
                            return temp;
                        }
                    }
                    return 0;

                    
                }
            }
        }
        public void Consultar_parametros(int sala,int dia, int hora, int minuto){
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {   int hor=(hora-7),Estatu,tempe;
                    //r.PeekChar();
                    //estado
                    Estatu=leer_parametro(sala,dia,hor,0);
                    //Puertas
                    Console.Write("Las puertas estan: {0}\n",Consultar_puertas(sala,dia,hor,minuto));  
                    Console.Write("Las luices estan: {0}\n",Consultar_luces(sala,dia,hor,minuto,Estatu));
                    tempe=Consultar_temp(sala,dia,hor,minuto,Estatu);
                    if(tempe!=0){
                    Console.Write("La temperatura es: {0}\n",tempe);}
                    else{Console.Write("El aire acondicionado esta desactivado\n");}

                    if(leer_parametro(sala,dia,hor,8)==1){Console.Write("Tiene refrigerio\n");}
                    else{Console.Write("NO Tiene refrigerio\n");}
                }
            }
        }
    }
}