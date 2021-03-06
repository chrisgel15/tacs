﻿using System;
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

namespace TelegramBot
{
    class Program
    {

        static TelegramBotClient bot = new TelegramBotClient("561318443:AAHMmc3GYXoHpg4sx9XlwnpJutQMmkyy4yM");
        static SessionManager sesiones = new SessionManager();
        static List<Command> commands = new List<Command> { new BuyCommand(), new SellCommand(), new WalletCommand(), new StartCommand(), new PriceCommand(), new LogInCommand(sesiones) };
        

        static void Main(string[] args)
        {
            bot.StartReceiving();
            bot.OnMessage += OnMessage;
            Console.ReadLine();
        }

        private static void OnMessage(Object sender, MessageEventArgs e)
        {
            if(e.Message.Type == MessageType.TextMessage)
            {
                LogMessage(e);

                var token = sesiones.GetToken(e.Message.Chat.Id);

                if (token == null && !e.Message.Text.Contains("/login"))
                    UserNotLoggedIn(e);
                else
                    CheckForCommands(e, token);
            }
        }

        private static void LogMessage(MessageEventArgs e)
        {
            var txt = e.Message.Text;
            var cid = e.Message.Chat.Id;
            var name = e.Message.From.FirstName + " " + e.Message.From.LastName;
            var uid = e.Message.From.Id;

            Console.WriteLine(" {0} - {1} - {2}: {3}", uid, cid, name, txt);
        }

        private static void CheckForCommands(MessageEventArgs messageEvent, string token)
        {            
            var command = commands.FirstOrDefault(x => x.IsCommand(messageEvent));

            if (command == null)
                CommandNotFound(messageEvent);
            else
                command.ExecuteCommand(messageEvent, token);                    
        }

        private static void CommandNotFound(MessageEventArgs messageEvent)
        {
            MessageSender messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            messageSender.SendMessage(chatId, "I couldn't understand that, what do you want to do?.", keyboard);
        }

        private static void UserNotLoggedIn(MessageEventArgs messageEvent)
        {
            MessageSender messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            var message = "You must log in first, use the command: /login {{username}} {{password}}";

            messageSender.SendMessage(chatId, message, keyboard);
        }
    }
}
