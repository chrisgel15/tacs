using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Comandos;

namespace TelegramBot.Commands
{
    public class WalletCommand : Command
    {
        public WalletCommand()
        {
            commandName = "/wallet";
        }

        public override void ExecuteValidCommand(MessageEventArgs messageEvent)
        {
            var messageSender = new MessageSender();
            var chatId = messageEvent.Message.Chat.Id;
            var keyboard = messageSender.CreateMenuKeyboard();

            messageSender.SendMessage(chatId, " Bitcoin: 0.00000232 \nEthereum: 340.2 \n", keyboard);
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
