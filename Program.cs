using Kutubxona;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using _5._3.OOP_advanced_bo_limi_uchun_3_amaliy_vazifa.Classes;
class Program
{
    private static string PathPerson = "KitobxonMalumotlari.txt";
    private static string PathBook = "KitobList.txt";
    public static void Main(string[] args)
    {
        KirishMenyu();
    }
    private static void KirishMenyu()
    {
        while (true)
        {
            Actions.SetColorLine("KITOBXON HUSH KELIBSIZ !!!");
            Console.WriteLine("\t1. Kirish");
            Console.WriteLine("\t2. Registratsiya");
            Console.WriteLine("\t3. Tugatish");
            Console.Write("Tanlang(raqamni kiriting): ");
            int.TryParse(Console.ReadLine(), out int choose);

            switch (choose)
            {
                case 1: Kirish(PathPerson); break;
                case 2: Registratsiya(PathPerson); break;
                case 3:; break;
                default: Actions.SetColor("Qayta Kiriting", ConsoleColor.Red); break;
            }
            if (choose == 3)
            {
                Actions.SetColorLine("Dasturimizdan foydalanganingiz uchun raxmat !!!");
                break;
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
    private static void TanlashMenyu(int id)
    {
        while (true)
        {
            Actions.SetColorLine("Menyudan Birini tanlang !!!");
            Console.WriteLine("\t1. Profil Haqida");
            Console.WriteLine("\t2. Menyu");
            Console.WriteLine("\t3. Kitob qaytarish");
            Console.WriteLine("\t4. Ortga");
            Actions.SetColor("Tanlanganingiz nomini yozing: ", ConsoleColor.Yellow);
            string choose = Console.ReadLine().ToLower();

            switch (choose)
            {
                case "profil haqida": ProfilHaqida(id); break;
                case "menyu": KitobMenyu(PathBook, id); break;
                case "kitob qaytarish": KitobQaytarish(id); break;
                case "ortga":; break;
                default: Actions.SetColor("Qayta Kiriting", ConsoleColor.Red); break;
            }
            if (choose == "ortga")
                break;
            Console.Clear();
        }
    }
    private static void Kirish(string path)
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Parol: ");
        string parol = Console.ReadLine();
        int id = 0;

        var dictioanryList = DictList(path);
        bool isThere = false;
        foreach (var dict in dictioanryList)
        {
            if (dict.Value[0] == login && dict.Value[1] == parol)
            {
                isThere = true;
                id = dict.Key;
                break;
            }
        }
        if (isThere)
        {
            Actions.SetColorLine("Kirish muvaffaqqiyatli yakunlandi !!!");
            Console.ReadLine();
            TanlashMenyu(id);
        }
        else
            Actions.SetColorLine("Bunday Foydalanuvchi mavjud emas.\nIltimos ro'yxattan o'ting !!!", ConsoleColor.Red);
    }
    private static void Registratsiya(string path)
    {
        KitobXon kitobXon = new KitobXon();
        Actions.SetColorLine("Ma'lumotlarni to'ldiring. ");

        Console.Write("Login: ");
        kitobXon.Login = Console.ReadLine();

        Console.Write("Parol: ");
        kitobXon.Parol = Console.ReadLine();

        Console.Write("Ism: ");
        kitobXon.Ismi = Console.ReadLine();

        Console.Write("Familya: ");
        kitobXon.Familyasi = Console.ReadLine();

        Console.Write("Yoshi: ");
        kitobXon.Yoshi = int.Parse(Console.ReadLine());
        kitobXon.Id = GetId(path);

        using (var sw = new StreamWriter(path, append: true))
        {
            sw.WriteLine(kitobXon.WriteFullInfo());
        }
        TanlashMenyu(kitobXon.Id);
    }
    private static int GetId(string path)
    {
        int id;

        if (!File.Exists(path))
            using (var sw = new StreamWriter(path, append: true)) { }

        using (var sr = new StreamReader(path))
        {
            List<string> list = new List<string>();
            string line;
            if (sr.Peek() == -1)
                id = 1;
            else
            {
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line.Substring(0, line.IndexOf(':')));
                }
                id = list.Count + 1;
            }
        }

        return id;
    }
    private static Dictionary<int, List<string>> DictList(string path)
    {
        var dict = new Dictionary<int, List<string>>();

        if (!File.Exists(path))
            using (var sw = new StreamWriter(path, append: true)) { }

        using (var sr = new StreamReader(path))
        {
            List<string> list = new List<string>();
            string line;
            string[] arrayLine = new string[3];

            while ((line = sr.ReadLine()) != null)
            {
                arrayLine = line.Split(':');

                list.AddRange(arrayLine[1].Split(','));
                list.Add(arrayLine[2] ?? "Unknown");

                dict.Add(key: Convert.ToInt32(arrayLine[0]), new List<string>(list));
                list.Clear();
            }
            return dict;
        }
    }
    private static void ProfilHaqida(int id)
    {
        var dict = DictList(PathPerson);

        Console.WriteLine($"Ism: {dict[id][2] ?? "Unknown"}\nFamilya: {dict[id][3] ?? "Unknown"}\nYoshi: {dict[id][4] ?? "Unknown"}");
        Console.WriteLine($"Nechta kitob olgan: {dict[id][5]}");
        Console.WriteLine($"Qanday kitoblar olgan: {dict[id][6]}");
        Console.ReadLine();
    }
    private static void KitobMenyu(string path, int idPerson)
    {
        while (true)
        {
            int bookId = GetId(path);
            var bookDict = DictList(PathBook);
            int end = bookDict.Count + 1;
            foreach (var item in bookDict)
            {
                Console.WriteLine($"{item.Key}. Nomi: {item.Value[0]}, Muallifi: {item.Value[1]}, Janri: {item.Value[2]}");
            }
            Actions.SetColorLine($"{end}. Ortga qaytish ", ConsoleColor.Red);
            Actions.SetColor("Istagan kitobingiz tartib raqamini kiriting: ", ConsoleColor.Yellow);
            int.TryParse(Console.ReadLine(), out int choose);

            if (0 < choose && choose < end)
                TanlanganKitob(idBook: choose, idPerson: idPerson);
            if (choose == end)
                break;
        }

    }
    private static void TanlanganKitob(int idBook, int idPerson)
    {
        var bookDict = DictList(PathBook);
        var personDict = DictList(PathPerson);
        while (true)
        {
            Console.WriteLine($"Nomi: {bookDict[idBook][0]}");
            Console.WriteLine($"Avtori: {bookDict[idBook][1]}");
            Console.WriteLine($"Janri: {bookDict[idBook][2]}");
            Console.WriteLine($"Jami Miqdori: {bookDict[idBook][3]} ta");
            Console.WriteLine($"Qolgan Miqdori: {bookDict[idBook][4]} ta");
            Console.WriteLine($"O'qiyotganlar: {bookDict[idBook][5] ?? "Hech kim"}");

            Actions.SetColorLine("kitobni olishni istaysizmi?");
            Console.WriteLine("\t1. Ha\n\t2. Yo'q");
            Actions.SetColor("Tartib raqam kiriting: ", ConsoleColor.Yellow);
            int.TryParse(Console.ReadLine(), out int tartib);

            if (tartib == 1)
            {
                KitobOlindi(idBook, idPerson);
                break;
            }
            else if (tartib == 2) break;
            else
            {
                Actions.SetColorLine("Tartib raqamni to'gri kiriting !!!", ConsoleColor.Red);
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
    private static void KitobOlindi(int idBook, int idPerson)
    {
        var bookDict = DictList(PathBook);
        var personDict = DictList(PathPerson);

        Kitob kitob = new Kitob(bookDict, idBook);
        KitobXon kitobxon = new KitobXon(personDict, idPerson);

        if (!kitobxon.QandayKitoblar.Contains(kitob.Nomi))
            kitobxon.QandayKitoblar.Add(kitob.Nomi);

        kitob.QolganMiqdori = kitob.QolganMiqdori - 1;

        if (!kitob.Oqiyotganlar.Contains($"{kitobxon.Ismi} {kitobxon.Familyasi}"))
            kitob.Oqiyotganlar.Add($"{kitobxon.Ismi} {kitobxon.Familyasi},");

        Actions.SetColorLine("Kitob muvoffaqqiyatli Topshirildi !!!");

        GetNewList(kitob, kitobxon);

    }
    private static void GetNewList(Kitob kitob, KitobXon kitobxon)
    {
        var bookDict = DictList(PathBook);
        var personDict = DictList(PathPerson);

        string oqiyotganlar = string.Join(",", kitob.Oqiyotganlar);
        string qandaykitoblar = string.Join(",", kitobxon.QandayKitoblar);

        bookDict[kitob.Id] = new List<string>
        {
            kitob.Nomi,
            kitob.Avtori,
            kitob.Janri,
            Convert.ToString(kitob.JamiMiqdori),
            Convert.ToString(kitob.QolganMiqdori),
            oqiyotganlar
        };
        personDict[kitobxon.Id] = new List<string>
        {
            kitobxon.Login,
            kitobxon.Parol,
            kitobxon.Ismi,
            kitobxon.Familyasi,
            Convert.ToString(kitobxon.Yoshi),
            Convert.ToString(kitobxon.KitoblarSoni),
            qandaykitoblar
        };
        using (var sw = new StreamWriter(PathBook, append: false))
        {
            foreach (var item in bookDict)
                sw.WriteLine($"{item.Key}:{item.Value[0]},{item.Value[1]},{item.Value[2]},{item.Value[3]},{item.Value[4]}:{item.Value[5]}:");
        }
        using (var sw = new StreamWriter(PathPerson, append: false))
        {
            foreach (var item in personDict)
                sw.WriteLine($"{item.Key}:{item.Value[0]},{item.Value[1]},{item.Value[2]},{item.Value[3]},{item.Value[4]},{item.Value[5]}:{item.Value[6]}");
        }
    }
    private static void KitobQaytarish(int id)
    {
        while (true)
        {
            Actions.SetColorLine("Qaysi kitobni qaytarmoqchisiz !!!");
            var bookDict = DictList(PathBook);
            foreach (var item in bookDict)
            {
                Console.WriteLine($"{item.Key}. Nomi: {item.Value[0]}, Muallifi: {item.Value[1]}, Janri: {item.Value[2]}");
            }
            int end = bookDict.Count + 1;
            Actions.SetColorLine($"{end}. Ortga qaytish ", ConsoleColor.Red);
            Actions.SetColor("Topshirmoqchi bo'lgan kitobingizni\nTartibini kiriting:", ConsoleColor.Yellow);
            int.TryParse(Console.ReadLine(), out int choose);

            if (0 < choose && choose < end)
            {
                KitobChiqarish(idBook: choose, idPerson: id);
                Actions.SetColorLine("Kitobni muvaffaqiyatli topshirdingiz !!!");
                Console.ReadLine();
                break;
            }
            if (choose == end)
                break;
        }

    }
    private static void KitobChiqarish(int idBook, int idPerson)
    {
        var bookDict = DictList(PathBook);
        var personDict = DictList(PathPerson);

        Kitob kitob = new Kitob(bookDict, idBook);
        KitobXon kitobxon = new KitobXon(personDict, idPerson);

        if (kitobxon.QandayKitoblar.Contains(kitob.Nomi))
            kitobxon.QandayKitoblar.Remove(kitob.Nomi);

        kitob.QolganMiqdori = kitob.QolganMiqdori + 1;

        if (kitob.Oqiyotganlar.Contains($"{kitobxon.Ismi} {kitobxon.Familyasi}"))
            kitob.Oqiyotganlar.Remove($"{kitobxon.Ismi} {kitobxon.Familyasi}");

        GetNewList(kitob, kitobxon);

    }

}