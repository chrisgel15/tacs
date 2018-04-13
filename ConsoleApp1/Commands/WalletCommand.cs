using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class WalletCommand : ICommand
    {
        static string commandName = "/wallet";

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            return messageEvent.Message.Text == commandName;
        }

        public void ExecuteCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, " Bitcoin: 0.00000232 \nEthereum: 340.2 \n", null);
        }
    }
}
