﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;
using TelegramBot;

namespace ConsoleApp1.Comandos
{
    public class StartCommand : Command
    {
        public StartCommand()
        {
            commandName = "/start";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent)
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
