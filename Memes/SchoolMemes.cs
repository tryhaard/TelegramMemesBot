using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramMemesBot.Bot;
using TelegramMemesBot.Models;

namespace TelegramMemesBot.Memes
{
    public class SchoolMemes
    {
        private List<Memas> _memes = new List<Memas>();

        public TelegramBot Bot { get; set; }

        

        public SchoolMemes(TelegramBot bot)
        {
            Bot = bot;
            InitMemes();
        }

        private void InitMemes()
        {
            try
            {
                string[] images = Directory.GetFiles("images/memes");

                foreach (var image in images)
                {
                    FileInfo fi = new(image);
                    _memes.Add(new Memas(Path.GetDirectoryName(image), fi.Name, ""));
                }
            }
            catch (Exception e)
            {
                
            }
        }

        public void StartSending()
        {
            while (_memes.Count != 0)
            {

                Random rand = new();
                int index = rand.Next(0, _memes.Count - 1);
                Memas memas = _memes[index];
                Bot.SendMeme(memas);

                try
                {
                    _memes.Remove(memas);
                    File.Move(_memes[0].ImagePath, _memes[0].SendedImagePath);
                }
                catch (Exception e)
                {

                }
                if(_memes.Count < 5)
                    InitMemes();

                int delay = rand.Next(20, 40);
                Thread.Sleep(delay * 60000);
            }
        }
    }

}
