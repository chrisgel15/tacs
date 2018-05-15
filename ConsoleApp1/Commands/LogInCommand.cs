using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;
using TelegramBot.Comandos;
using TelegramBot.Commands;
using TelegramBot;

namespace TelegramBot.Commands
{
    public class LogInCommand : Command
    {
        public LogInCommand(SessionManager sessionMng)
        {
            commandName = "/login";
            sessionManager = sessionMng;
        }
        private SessionManager sessionManager;

        public override void ExecuteValidCommand(MessageEventArgs messageEvent, string token)
        {
            var messageSplit = messageEvent.Message.Text.Split(' ');

            var username = messageSplit.ElementAt(1);
            var password = messageSplit.ElementAt(2);
            ApiDataAccess apiDataAccess = new ApiDataAccess();
            var newToken =  apiDataAccess.Login(username, password);

            string response;
            if (newToken != null)
            {
                sessionManager.AddSesion(messageEvent.Message.Chat.Id, newToken);
                response = "You are now logged in, what do you want to do?";
            }
            else
                response = "Incorrect username or password";

            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();
            messageSender.SendMessage(chatId, response, keyboard);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;
            var message = "Invalid parameters, use the command: /login {{username}} {{password}}";
            messageSender.SendMessage(chatId, message, null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 3, false);
        }
    }
}
