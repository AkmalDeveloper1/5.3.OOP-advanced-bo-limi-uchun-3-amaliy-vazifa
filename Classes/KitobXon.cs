using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutubxona
{
    class KitobXon
    {
        public KitobXon()
        {

        }
        public KitobXon(Dictionary<int, List<string>> personInfo, int id)
        {
            Login = personInfo[id][0];
            Parol = personInfo[id][1];
            Id = id;
            Ismi = personInfo[id][2];
            Familyasi = personInfo[id][3];
            Yoshi = Convert.ToInt32(personInfo[id][4]);
            QandayKitoblar = personInfo[id][6].Split(',').ToList();
        }
        public string Login { get; set; }
        public string Parol { get; set; }
        public int Id { get; set; }
        public string Ismi { get; set; }
        public string Familyasi { get; set; }
        public int Yoshi { get; set; }
        public int KitoblarSoni { get { return QandayKitoblar?.Count-1??0; } }
        public List<string> QandayKitoblar { get; set; }
        public string WriteFullInfo()
        {
            string fullInfo;
            if (QandayKitoblar==null)
                fullInfo = $"{Id}:{Login},{Parol},{Ismi},{Familyasi},{Yoshi},{KitoblarSoni}::";
            else
                fullInfo = $"{Id}:{Login},{Parol},{Ismi},{Familyasi},{Yoshi},{KitoblarSoni}:{string.Join(",",QandayKitoblar)}:";
            return fullInfo;

           
        }
        public void AllInfo()
        {
            Console.WriteLine("Login:" + Login);
            Console.WriteLine("Parol:" + Parol);
            Console.WriteLine("id." + Id);
            Console.WriteLine("Ism:" + Ismi);
            Console.WriteLine("Familya:" + Familyasi);
            Console.WriteLine("Yoshi:" + Yoshi);
            Console.WriteLine("Kitoblar soni:" + KitoblarSoni);
            Console.WriteLine("Qanday kitoblar:");
            foreach (var item in QandayKitoblar)
            {
                Console.WriteLine(item);
            }
        }

    }
}
