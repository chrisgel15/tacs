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
using ConsoleApp1.Comandos;
using ConsoleApp1.Commands;
using ConsoleApp1;

namespace TelegramBot
{
    class Program
    {

        static TelegramBotClient bot = new TelegramBotClient("561318443:AAHMmc3GYXoHpg4sx9XlwnpJutQMmkyy4yM");
        static List<Command> commands = new List<Command> { new BuyCommand(), new SellCommand(), new WalletCommand(), new StartCommand(), new PriceCommand() };

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
                CheckForCommands(e);
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

        private static void CheckForCommands(MessageEventArgs messageEvent)
        {
            var command = commands.FirstOrDefault(x => x.IsCommand(messageEvent));

            if (command == null)
                CommandNotFound(messageEvent);
            else
                command.ExecuteCommand(messageEvent);
                    
        }

        private static void CommandNotFound(MessageEventArgs messageEvent)
        {
            MessageSender messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            messageSender.SendMessage(chatId, "I didnt quite catch that, what do you want to do?.", keyboard);
        }

    }
}
