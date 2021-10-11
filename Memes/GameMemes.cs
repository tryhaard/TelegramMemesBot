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
    public class GameMemes
    {
        private List<Memas> _memes = new List<Memas>();

        public TelegramBot Bot { get; set; }



        public GameMemes(TelegramBot bot)
        {
            Bot = bot;
            InitMemes();
        }

        private void InitMemes()
        {
            try
            {
                var images = DirSearch("images/games");
                
                foreach (var image in images)
                {
                    FileInfo fi = new(image);
                    string tag = new DirectoryInfo(image).Parent.Name;

                    _memes.Add(new Memas(Path.GetDirectoryName(image), fi.Name, tag));
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
                if (_memes.Count < 5)
                    InitMemes();

                Thread.Sleep(rand.Next(15, 45) * 60000);
            }
        }

        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
            }

            return files;
        }
    }
}
