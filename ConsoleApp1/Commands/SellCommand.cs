using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class SellCommand : Command
    {
        public SellCommand()
        {
            commandName = "/sell";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent)
        {
            var coinID = syntaxChecker.GetCoinId(messageEvent.Message.Text);

            var response = coinID + ", Transaction Completed." ;

            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, response, null);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To sell a coin you must use a coinId and the amount you want to sell, you can get Ids from the following URL: <a>https://coinmarketcap.com/es/api/</a> \n Use the command /sell {{coinId}} {{amount}}", null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 3, true);
        }
    }
}
