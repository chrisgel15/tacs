using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class BuyCommand : ICommand
    {
        static string commandName = "/buy";

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            return messageEvent.Message.Text == commandName;
        }

        public void ExecuteCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To buy a coin you must use the command /buy {{coinId}} \n Here you can check out all of the coins ID: <a>https://coinmarketcap.com/es/api/</a>", null);
        }
    }
}
