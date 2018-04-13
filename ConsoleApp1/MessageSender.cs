using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;

namespace ConsoleApp1
{
    public class MessageSender
    {
        static TelegramBotClient bot = new TelegramBotClient("561318443:AAHMmc3GYXoHpg4sx9XlwnpJutQMmkyy4yM");

        public ReplyKeyboardMarkup CreateMenuKeyboard()
        {
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup();
            keyboard.ResizeKeyboard = true;

            keyboard.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("/wallet"),
                    new KeyboardButton("/prices"),
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("/buy"),
                    new KeyboardButton("/sell")
                }
            };

            keyboard.ResizeKeyboard = true;
            keyboard.OneTimeKeyboard = true;

            return keyboard;
        }

        public async void  SendMessage(long chatId, string message, ReplyKeyboardMarkup keyboard)
        {
            await bot.SendTextMessageAsync(chatId, message, ParseMode.Html, false, false, 0, keyboard);
        }
    }
}
