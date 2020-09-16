using System;
namespace Sistemare.Menu
{
    public class menu 
    {
        #region Methods
        public void bienvenida()
        {
        Console.Clear();
        Console.WriteLine("Bienvenido al menú, ¿ Qué podemos hacer por ti ?");
        
        }
        public void imprimirInvit()
        {
            bienvenida();
            Console.WriteLine("1.Consultar Disponibilidad");
            Console.WriteLine("2.Reservar salón");
        }
        
        #endregion Methods
    }
}