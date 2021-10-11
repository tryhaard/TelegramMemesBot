using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramMemesBot.Memes;

namespace TelegramMemesBot.Bot
{
    public class TelegramBotController
    {
        private List<TelegramBot> _bots = new List<TelegramBot>();

        public TelegramBotController()
        {
            InitialBots();
        }

        private void InitialBots()
        {
            _bots.Add(new TelegramBot("апи ключ", "@ваш канал"));
            _bots.Add(new TelegramBot("апи ключ", "@ваш канал"));
        }


        public void Start()
        {
            List<Task> tasks = new();

            Task task = new Task((() =>
            {
                SchoolMemes rofloman = new(_bots[0]);
                rofloman.StartSending();
            }));
            Task task1 = new Task((() =>
            {
                GameMemes igroCringe = new(_bots[1]);
                igroCringe.StartSending();
            }));

            tasks.Add(task);
            tasks.Add(task1);

            tasks.ForEach(x =>
            {
                x.Start();
            });
            Task.WaitAll(tasks.ToArray());
        }

    }
}
