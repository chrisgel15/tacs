using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;
using TelegramBot;

namespace ConsoleApp1.Comandos
{
    public class StartCommand : ICommand
    {
        static string commandName = "/start";

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            return messageEvent.Message.Text == commandName;
        }

        public void ExecuteCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            messageSender.SendMessage(chatId, "Welcome to CryptoTacs, how can I help you?", keyboard);
        }


    }
}
