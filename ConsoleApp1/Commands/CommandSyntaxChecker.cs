using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    public class CommandSyntaxChecker
    {
        public bool CheckSyntax(string message)
        {
            var messageSplit = message.Split(' ');

            var twoParts = messageSplit.Count() == 2;

            //TODO: check valid coin ID

            return twoParts;
        }

        public string GetCoinId(string message)
        {
            var messageSplit = message.Split(' ');

            return messageSplit.GetValue(1).ToString();
        }
    }
}
