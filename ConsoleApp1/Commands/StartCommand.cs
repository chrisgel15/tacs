using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Comandos;
using TelegramBot;

namespace TelegramBot.Comandos
{
    public class StartCommand : Command
    {
        public StartCommand()
        {
            commandName = "/start";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent, string token)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            messageSender.SendMessage(chatId, "Welcome to CryptoTacs, how can I help you?", keyboard);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To start using the bot you must send \"/start\"", null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 1, false);
        }
    }
}
