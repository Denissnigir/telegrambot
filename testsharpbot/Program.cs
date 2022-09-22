using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace testsharpbot
{
    internal class Program
    {
        static TelegramBotClient _bot = new TelegramBotClient
            ("5626853306:AAGO7MPYAAED3hSJSleDXmGZzCqpA-ee9jg");
        private static ReceiverOptions receiverOptions;

        static void Main(string[] args)
        {
            receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new Telegram.Bot.Types.Enums.UpdateType[]
                {
                    Telegram.Bot.Types.Enums.UpdateType.Message,
                    Telegram.Bot.Types.Enums.UpdateType.EditedMessage
                }
            };

            _bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);

            Console.ReadLine();
        }

        private static async Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine(arg2.Message);
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            if (update.Message.Text == "Хочу увидеть задачи")
            {
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Ну на");
            }

            if (update.Message.Text == "Не хочу увидеть задачи")
            {
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Ну и не надо");
            }

            if (update.Message.Text == "/start")
            {
                KeyboardButton[] keyboardButtons = new KeyboardButton[]
                {
                    new KeyboardButton("Привет"),
                    new KeyboardButton("Пока"),
                };

                var rkm = new ReplyKeyboardMarkup(keyboardButtons);
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Здарова заебал", replyMarkup: rkm);
            }

            if(update.Message.Text == "Привет")
            {
                var path = @"C:\Users\snigi\Downloads\168774-priroda-visgun_socialnaya_set-voda-atmosfera-luna-3840x2160.jpg";
                using(var fileStream = new FileStream(path, FileMode.Open))
                {
                    await _bot.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream));
                }
            }
        }
    }
}
