using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using System.Net.Http;
using System.Net.Http.Headers;
using TelegramBot.Comandos;
using TelegramBot.ViewModels;

namespace TelegramBot.Commands
{
    public class WalletCommand : Command
    {
        public WalletCommand()
        {
            commandName = "/wallet";
        }

        public async override void ExecuteValidCommand(MessageEventArgs messageEvent, int userId)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            ApiDataAccess apiDataAccess = new ApiDataAccess();

            UserWallet  wallet = await apiDataAccess.GetWallet(userId);

            string message = "";
            foreach(var coinWallet in wallet.coinWallets)
            {
                message = message + coinWallet.NombreMoneda + ": " + coinWallet.Balance + " \n";
            }

            messageSender.SendMessage(chatId, message, keyboard);
        }

        public override void ExecuteNotValidCommand(MessageEventArgs messageEvent)
        {
            var chatId = messageEvent.Message.Chat.Id;

            messageSender.SendMessage(chatId, "To see your wallet send \"/wallet\"", null);
        }

        public override bool CheckCommandSyntax(string message)
        {
            return syntaxChecker.CheckSyntax(message, 1, false);
        }

    }
}
