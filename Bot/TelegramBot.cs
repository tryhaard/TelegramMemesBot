using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramMemesBot.Models;

namespace TelegramMemesBot.Bot
{
    public class TelegramBot
    {
        private string _apiKey;
        private string _channelName;
        private List<BotMarkupsModel> _markups = new List<BotMarkupsModel>();

        private TelegramBotClient _bot = null;

        public TelegramBot(string apiKey, string channelName)
        {
            _apiKey = apiKey;
            _channelName = channelName;
            InitialBot();
        }


        private void InitialBot()
        {
            _bot = new TelegramBotClient(_apiKey);
            _bot.OnCallbackQuery += Bot_Callback;
            _bot.StartReceiving();
        }



        public void SendMeme(Memas memas)
        {
            
            var inlineKeyboard = UpdateMarkup();
            using (var stream = System.IO.File.Open(memas.ImagePath, FileMode.Open))
            {
                InputOnlineFile fts = new InputOnlineFile(stream, memas.filename);
                _bot.SendPhotoAsync(_channelName, fts, caption:$"{memas.Tag}", replyMarkup:inlineKeyboard).GetAwaiter().GetResult();
            }
            


        }
        
        private InlineKeyboardMarkup UpdateMarkup(int likes = 0, int dislikes = 0)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData($"👍{likes}", $"Like"),
                    InlineKeyboardButton.WithCallbackData($"👎{dislikes}", $"Dislike")
                }
            });
            return inlineKeyboard;
        }

        private void Bot_Callback(object? sender, CallbackQueryEventArgs e)
        {
            long senderId = e.CallbackQuery.From.Id;
            long messageId = e.CallbackQuery.Message.MessageId;


            BotMarkupsModel bmm = _markups.Find(x => x.MessageId == messageId);

            if (bmm == null)
            {
                _markups.Add(new BotMarkupsModel(messageId));
            }
            BotMarkupsModel markup = _markups.Find(x => x.MessageId == messageId);

            if (markup.CheckUser(senderId))
            {
                markup.AddSended(senderId);

                if (e.CallbackQuery.Data == "Like")
                {
                    markup.AddLike();
                }
                else
                {
                    markup.AddDislike();
                }
                _bot.EditMessageReplyMarkupAsync($"@{e.CallbackQuery.Message.Chat.Username}", e.CallbackQuery.Message.MessageId, UpdateMarkup(markup.Likes, markup.Dislikes));
            }
        }
    }
}
