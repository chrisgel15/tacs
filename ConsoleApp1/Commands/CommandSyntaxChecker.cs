using System;
using System.Collections.Generic;
using System.Globalization;
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
            var amount = messageSplit.Last();
            amount = amount.Replace(",", ".");
            if (checkAmount && correctNumberOfParts)
                amountCorrect = decimal.TryParse(amount,NumberStyles.Any, CultureInfo.InvariantCulture, out decimal n);

            return correctNumberOfParts && amountCorrect /*&& validId*/ ;
        }

        public string GetCoinId(string message)
        {
            var messageSplit = message.Split(' ');

            return messageSplit.GetValue(1).ToString();
        }

        public Decimal GetAmount(string message)
        {
            var messageSplit = message.Split(' ');
            var amountString = messageSplit.Last();
            amountString = amountString.Replace(",", ".");
            var amount = decimal.Parse(amountString,CultureInfo.InvariantCulture);

            return amount;
        }
    }
}
