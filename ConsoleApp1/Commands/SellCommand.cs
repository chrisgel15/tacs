using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class SellCommand : ICommand
    {
        static string commandName = "/sell";

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            return messageEvent.Message.Text == commandName;
        }

        public void ExecuteCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "Transaction completed.", null);
        }
    }
}
