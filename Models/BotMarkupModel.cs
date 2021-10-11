using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramMemesBot.Models
{
    public class BotMarkupsModel
    {
        public long MessageId { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public List<long> Senders = new List<long>();

        public BotMarkupsModel(long messageId)
        {
            MessageId = messageId;
        }


        public void AddLike()
        {
            Likes++;
        }

        public void AddDislike()
        {
            Dislikes++;
        }

        public void AddSended(long id)
        {
            Senders.Add(id);
        }

        public bool CheckUser(long id)
        {
            long userId = Senders.Find(x => x == id);
            if (userId != 0)
                return false;

            return true;
        }
    }
}
