using System;
namespace Sistemare.Usuarios
{
    public class usuario
    {
        #region Properties
        public bool Alowed { get; set; }
        Tuple<string, string>[] Admins = 
                    { new Tuple<string, string>("Fernando", "nightmare"),
                    new Tuple<string, string>("POO", "helpme"),
                    new Tuple<string, string>("Luis", "popo")};
        Tuple<string, string>[] Invits = 
                    { new Tuple<string, string>("Leonardo", "POO"),
                    new Tuple<string, string>("donatelo", "PINPON"),
                    new Tuple<string, string>("caesar", "guau")};
        
        #endregion Properties

        #region Methods
        public bool ValidarAdmin(string user,string contra)
        {   
            foreach(var admin in Admins){    
                if (admin.Item1==user && admin.Item2==contra){
                    return true;
                }
            }
            return false;
        }
        public bool ValidarInvit(string user,string contra)
        {   
            foreach(var invi in Invits){    
                if (invi.Item1==user && invi.Item2==contra){
                    return true;
                }
            }
            return false;
        }
        #endregion Methods
        #region Initialize
        #endregion Initialize
    }
}