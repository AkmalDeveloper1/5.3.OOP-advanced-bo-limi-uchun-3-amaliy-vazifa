using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutubxona
{
    class Kitob
    {
        public Kitob() { }
        public Kitob(Dictionary<int,List<string>> bookInfo,int id)
        {
            Id = id;
            Nomi = bookInfo[id][0];
            Avtori = bookInfo[id][1];
            Janri = bookInfo[id][2];
            JamiMiqdori =Convert.ToInt32(bookInfo[id][3]);
            QolganMiqdori = Convert.ToInt32(bookInfo[id][4]);
            Oqiyotganlar = bookInfo[id][5].Split(',').ToList();
        }

        public int Id { get; set; }
        public string Nomi { get; set; }
        public string Avtori { get; set; }
        public string Janri { get; set; }
        public int JamiMiqdori { get; set; }
        public int QolganMiqdori { get; set; }
        public List<string> Oqiyotganlar { get; set; }
        public void AllInfo()
        {
            Console.WriteLine("id." + Id);
            Console.WriteLine("Nomi:" + Nomi);
            Console.WriteLine("Avtori:" + Avtori);
            Console.WriteLine("Janri:" + Janri);
            Console.WriteLine("JamiMiqdori:" + JamiMiqdori);
            Console.WriteLine("QolganMiqdori:" + QolganMiqdori);
            Console.WriteLine("Oqiyotganlar:");
            foreach (var item in Oqiyotganlar)
            {
                Console.WriteLine(item);
            }
        }
        public string WriteFullInfo()
        {
            string fullInfo;
            if (Oqiyotganlar == null)
                fullInfo = $"{Id}:{Nomi},{Avtori},{Janri},{JamiMiqdori},{QolganMiqdori}::";
            else
                fullInfo = $"{Id}:{Nomi},{Avtori},{Janri},{JamiMiqdori},{QolganMiqdori}:{ string.Join(",", Oqiyotganlar)}:";
            return fullInfo;
        }

    }
}
