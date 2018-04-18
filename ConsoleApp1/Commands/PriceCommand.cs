using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using ConsoleApp1.Comandos;

namespace ConsoleApp1.Commands
{
    public class PriceCommand : Command 
    {

        public PriceCommand()
        {
            commandName = "/price";
        }
            
        public override void ExecuteValidCommand(MessageEventArgs messageEvent)
        {
            var coinID = syntaxChecker.GetCoinId(messageEvent.Message.Text);
            var price = 420.23;

            var response = coinID + ":" + " US$ " + price;

            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, response, null);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To get the current price of a coin you must use a coinId, you can get Ids from the following URL: <a>https://coinmarketcap.com/es/api/</a> \n Use the command /price {{coinId}}", null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 2, false);
        }


    }
}
