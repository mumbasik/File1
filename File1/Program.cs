using System.Runtime.CompilerServices;
using System.IO;
using System.Xml.Serialization;
using System;
using System.Xml;

using System.Text.Json;
namespace File1
{


    public class MusicAlbom
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int Year { get; set; }
        public double Duration { get; set; }
        public string Studio { get; set; }
        public int Count { get; set; }
        public MusicAlbom() { }
        public MusicAlbom(string alb, string name, int year, double dur, string st, int c)
        {

            AlbumName = alb;
            ArtistName = name;
            Year = year;
            Duration = dur;
            Studio = st;
            Count = c;


        }
        public void AddInfo(List<MusicAlbom> list, MusicAlbom alb)
        {

            Console.WriteLine("Enter album name:");
            alb.AlbumName = Console.ReadLine();

            Console.WriteLine("Enter artist name:");
            alb.ArtistName = Console.ReadLine();

            Console.WriteLine("Enter year:");
            alb.Year = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter duration:");
            alb.Duration = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter studio name:");
            alb.Studio = Console.ReadLine();

            Console.WriteLine("Enter count:");
            alb.Count = Convert.ToInt32(Console.ReadLine());

            list.Add(alb);

        }
        public void Print()
        {

            Console.WriteLine("Album Name: " + AlbumName);
            Console.WriteLine("Artist Name: " + ArtistName);
            Console.WriteLine("Year: " + Year);
            Console.WriteLine("Duration: " + Duration);
            Console.WriteLine("Studio Name: " + Studio);
            Console.WriteLine("Count: " + Count);

        }

        public void SaveToFile(List<MusicAlbom> list)
        {

            foreach (MusicAlbom alb in list)
            {
                string albumInfo = $"Album Name: {alb.AlbumName}, Artist Name: {alb.ArtistName}, Year: {alb.Year}, Duration: {alb.Duration}, Studio: {alb.Studio}, Count: {alb.Count}";


                File.AppendAllText("example.txt", albumInfo);
            }
        }

    }


    namespace JournalApp
    {
      
        public class Article
        {
            public string Title { get; set; }
            public int CharacterCount { get; set; }
            public string Preview { get; set; }

            public Article() { }

            public Article(string title, int charCount, string preview)
            {
                Title = title;
                CharacterCount = charCount;
                Preview = preview;
            }

            public void Print()
            {
                Console.WriteLine($"Article Title: {Title}");
                Console.WriteLine($"Character Count: {CharacterCount}");
                Console.WriteLine($"Preview: {Preview}");
            }

            public void Input() {
                Console.WriteLine($"Article Title: ");
                Title = Console.ReadLine();
                Console.WriteLine($"Character Count: ");
                CharacterCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Preview: ");
                Preview = Console.ReadLine();
                Article article = new Article(Title, CharacterCount, Preview);
                

            }
        }

        
        public class Journal
        {
            public string JournalTitle { get; set; }
            public string Publisher { get; set; }
            public DateTime PublishDate { get; set; }
            public int PageCount { get; set; }
            public List<Article> Articles { get; set; }

            public Journal()
            {
                Articles = new List<Article>();
            }

            public Journal(string title, string publisher, DateTime publishDate, int pageCount, List<Article> articles)
            {
                JournalTitle = title;
                Publisher = publisher;
                PublishDate = publishDate;
                PageCount = pageCount;
                Articles = articles;
            }

            public void AddArticle(Article article)
            {
                Articles.Add(article);
            }

            public void Print()
            {
                Console.WriteLine($"Journal Title: {JournalTitle}");
                Console.WriteLine($"Publisher: {Publisher}");
                Console.WriteLine($"Publish Date: {PublishDate.ToShortDateString()}");
                Console.WriteLine($"Page Count: {PageCount}");
                Console.WriteLine("Articles:");
                foreach (var article in Articles)
                {
                    article.Print();
                    Console.WriteLine();
                }
            }
            public void Input()
            {
                Console.WriteLine($"Journal Title: ");
                JournalTitle = Console.ReadLine();
                Console.WriteLine($"Publisher: ");
                Publisher = Console.ReadLine();
                Console.WriteLine($"Publish Date: ");
                PublishDate = Convert.ToDateTime( Console.ReadLine() );
                Console.WriteLine($"Page Count: ");
                PageCount = Convert.ToInt32( Console.ReadLine() );

            }
        }



        internal class Program
        {
            static void Main(string[] args)
            {
                //N1
                List<MusicAlbom> list = new List<MusicAlbom>();
                MusicAlbom alb = new MusicAlbom();
                MusicAlbom obj = new MusicAlbom("Name", "AstistName", 432432, 20.20, "Studio", 0);
                alb.AddInfo(list, alb);
                obj.Print();
                alb.Print();

                XmlSerializer formatter = new XmlSerializer(typeof(List<MusicAlbom>));

                using (FileStream fs = new FileStream("newlist.xml", FileMode.Create))
                {
                    formatter.Serialize(fs, list);
                    Console.WriteLine("Albums have been serialized to 'newlist.xml'.");
                }


                using (FileStream fs = new FileStream("newlist.xml", FileMode.OpenOrCreate))
                {
                    List<MusicAlbom>? newlist = formatter.Deserialize(fs) as List<MusicAlbom>;

                    if (newlist != null)
                    {
                        Console.WriteLine("Albums have been deserialized:");
                        foreach (MusicAlbom person in newlist)
                        {
                            Console.WriteLine($"Album Name: {person.AlbumName}, Artist Name: {person.ArtistName}, Year: {person.Year}, Duration: {person.Duration}, Studio: {person.Studio}, Count: {person.Count}");
                        }
                    }
                }
                //N2
                Journal journal = new Journal();
                Article article = new Article();
                journal.Input();
                article.Input();
                journal.AddArticle(article);
                journal.Print();
               
                string json = JsonSerializer.Serialize(journal, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("journal.json", json);
                Console.WriteLine("\nJournal has been serialized to 'journal.json'.");

                
                string loadedJson = File.ReadAllText("journal.json");
                Journal deserializedJournal = JsonSerializer.Deserialize<Journal>(loadedJson);

                
                Console.WriteLine("\nDeserialized Journal Information:");
                deserializedJournal.Print();



            }
        }
    }
}
