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
    public class StartCommand
    {
        static string commandName = "/start";

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            var command = messageEvent.Message.Text;
            return command.Contains(commandName);
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
