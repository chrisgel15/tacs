using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class BuyCommand : Command
    {
        public BuyCommand()
        {
            commandName = "/buy";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To buy a coin coin you must use a coinId and the amount you want to buy, you can get Ids from the following URL:: <a>https://coinmarketcap.com/es/api/</a> Use the command /buy {{coinId}} {{amount}}", null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 1, false);
        }
    }
}
