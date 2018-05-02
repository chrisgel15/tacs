using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Commands
{
    public class CommandSyntaxChecker
    {
        public bool CheckSyntax(string message, int parts, bool checkAmount)
        {
            var messageSplit = message.Split(' ');
            int numberOfParts = messageSplit.Count();

            bool correctNumberOfParts = numberOfParts == parts;

            //TODO: check valid coin ID

            bool amountCorrect = true;
            if (checkAmount && correctNumberOfParts)
                amountCorrect = int.TryParse(messageSplit.Last(), out int n);

            return correctNumberOfParts && amountCorrect /*&& validId*/ ;
        }

        public string GetCoinId(string message)
        {
            var messageSplit = message.Split(' ');

            return messageSplit.GetValue(1).ToString();
        }
    }
}
