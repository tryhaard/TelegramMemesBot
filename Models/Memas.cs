namespace TelegramMemesBot.Models
{
    public class Memas
    {
        private readonly string _folder;
        private string _tag;
        public string filename;
        public string ImagePath => $"{_folder}/{filename}";
        public string SendedImagePath => $"{_folder}/sended/{filename}";


        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if (value == "")
                    _tag = "";
                else
                {
                    _tag = $"#{value}";
                }
            }
        }




        public Memas(string folder, string filename, string tag)
        {
            _folder = folder;
            this.filename = filename;
            Tag = tag;
        }
    }
}
