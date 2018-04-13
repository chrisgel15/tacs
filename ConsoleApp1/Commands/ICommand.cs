using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace ConsoleApp1.Comandos
{
     public interface ICommand
    {
        void ExecuteCommand(MessageEventArgs messageEvent);

        bool IsCommand(MessageEventArgs messageEvent);
    }
}
