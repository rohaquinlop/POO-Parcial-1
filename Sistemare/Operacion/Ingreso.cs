
using System;
using Sistemare.Usuarios;
using Sistemare.Menu;
namespace Sistemare.Operacion
{
    public class Ingreso
    {
        //instacio usuario
        usuario users = new usuario();
        #region Properties
        
        public bool permiso { get; set; }
        string selection,user,contraseña;
        #endregion Properties

        #region Methods
        public void IngresoSistema()
        {
            Console.WriteLine("Bienvenido al Sistema de reservas javerianacali\nDesea entrar como Administrador(1) o invitado(2): ");
            selection = Console.ReadLine();
            Console.Clear();
            if(selection=="1"||selection=="2"){
                Pedirdatos();
            }
            else{
            IngresoSistema();
            }
        }

        

        private void Pedirdatos()
        {
            
            Console.WriteLine("Ingrese usuario: ");
            user = Console.ReadLine();
            Console.WriteLine("Ingrese contraseña: ");
            contraseña = Console.ReadLine();
            if (!validar()){
                Console.Clear();
                Console.WriteLine("Usuario y/o contraseña incorrectos,  Ingrese de nuevo:\n");

                Pedirdatos();
            }
            else{
                if (selection == "1"){
                    Console.Clear();
                    Console.WriteLine("Acceso concedido");
                    //MenuAdmin();
                    menuAdmin nuevoMenuE = new menuAdmin();
                    nuevoMenuE.leerAdmin();
                    
                }
                else{
                    Console.Clear();
                    Console.WriteLine("Acceso concedido");
                     //MenuInvit();
                    menuInvitado nuevoMenu = new menuInvitado();
                    nuevoMenu.leerinv();
                }
            }

        }
        private bool validar()
        {
            if (selection == "1"){
                return users.ValidarAdmin(user,contraseña);
            }
            return users.ValidarInvit(user,contraseña);
          
        }

        #endregion Methods
    }
}