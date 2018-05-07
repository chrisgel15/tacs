using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Comandos;

namespace TelegramBot.Commands
{
    public class BuyCommand : Command
    {
        public BuyCommand()
        {
            commandName = "/buy";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent, int userId)
        {
            var coinID = syntaxChecker.GetCoinId(messageEvent.Message.Text);
            var amount = syntaxChecker.GetAmount(messageEvent.Message.Text);
            ApiDataAccess apiDataAccess = new ApiDataAccess();
            apiDataAccess.MakeTransaction("compra", userId, coinID, amount);


            var response = "Transaction Completed." ;
            var chatId = messageEvent.Message.Chat.Id;
            messageSender.SendMessage(chatId, response, null);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;
            var message = "To buy a coin coin you must use a coinId and the amount you want to buy, you can get Ids from the following URL:: <a>https://coinmarketcap.com/es/api/</a> Use the command /buy {{coinId}} {{amount}}";
            messageSender.SendMessage(chatId, message , null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 3, true);
        }
    }
}
