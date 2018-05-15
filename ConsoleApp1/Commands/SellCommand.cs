using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Comandos;

namespace TelegramBot.Commands
{
    public class SellCommand : Command
    {
        public SellCommand()
        {
            commandName = "/sell";
        }

        //TODO: tratar mejor los errores
        public override void ExecuteValidCommand(MessageEventArgs messageEvent, string token)
        {
            var coinID = syntaxChecker.GetCoinId(messageEvent.Message.Text);
            var amount = syntaxChecker.GetAmount(messageEvent.Message.Text);
            ApiDataAccess apiDataAccess = new ApiDataAccess();
            string response;
            try
            {
                apiDataAccess.MakeTransaction("venta", token, coinID, amount);
                response = "Transaction Completed.";
            }
            catch
            {
                response = "You don't have enough coins";
            }
         
            var chatId = messageEvent.Message.Chat.Id;
            messageSender.SendMessage(chatId, response, null);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;
            var message = "To sell a coin you must use a coinId and the amount you want to sell, you can get Ids from the following URL: <a>https://coinmarketcap.com/es/api/</a> \n Use the command /sell {{coinId}} {{amount}}";

            messageSender.SendMessage(chatId, message, null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 3, true);
        }
    }
}
