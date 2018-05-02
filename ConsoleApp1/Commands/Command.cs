using TelegramBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramBot.Comandos
{
     public abstract class Command
    {
        public MessageSender messageSender = new MessageSender();
        public CommandSyntaxChecker syntaxChecker = new CommandSyntaxChecker();
        public  string commandName;

        public bool IsCommand(MessageEventArgs messageEvent)
        {
            var command = messageEvent.Message.Text;
            var firstPart = command.Split(' ').First();
            return firstPart == commandName;
        }

        public void ExecuteCommand(MessageEventArgs messageEvent)
        {
            var valid = CheckCommandSyntax(messageEvent.Message.Text);

            if (valid)
                ExecuteValidCommand(messageEvent);
            else
                ExecuteNotValidCommand(messageEvent);
        }

        public abstract void ExecuteValidCommand(MessageEventArgs messageEvent);
        public abstract void ExecuteNotValidCommand(MessageEventArgs messageEvent);
        public abstract bool CheckCommandSyntax(string message);
    }
}
