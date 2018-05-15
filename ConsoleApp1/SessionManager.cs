using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class SessionManager
    {
        Dictionary<long, string> sesiones;

        public SessionManager(){
            sesiones = new Dictionary<long, string>();
        }

        public void AddSesion(long id, string token)
        {
            sesiones.Add(id, token);
        }

        public string GetToken(long id)
        {
            return sesiones.FirstOrDefault(x => x.Key == id).Value;
        }

    }
}
