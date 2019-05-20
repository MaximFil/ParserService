using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using ParserService.Controllers;
using ParserService.ViewModels;
using AngleSharp;
using AngleSharp.Dom;

namespace ParserService
{
    public partial class Service1 : ServiceBase
    {
        Class1 crudArticles;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {           
            AddLog("start");
            crudArticles = new Class1();
            Thread crudArticlesThread = new Thread(new ThreadStart(crudArticles.Start));
            crudArticlesThread.Start();
        }

        protected override void OnStop()
        {
            AddLog("stop");
            crudArticles.Stop();
            Thread.Sleep(1000);
        }
        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("MyExampleService"))
                {
                    EventLog.CreateEventSource("MyExampleService", "MyExampleService");
                }
                eventLog1.Source = "MyExampleService";
                eventLog1.WriteEntry(log);
            }
            catch { }
        }
    }
    class Class1
    {
        static bool enabled;
        public Class1()
        {
            enabled = true;
        }
        public void Start()
        {
           
            display("\n" + DateTime.Now.ToString() + "Begin");
            while (enabled)
            {
                display("\n" + DateTime.Now.ToString() + "Start");
                System.Threading.Thread.Sleep(1000);
               // Create();
                display("\n" + DateTime.Now.ToString() + "End");
            }
        }
        public void Stop()
        {
            enabled = false;
        }

        public void display(string str)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@"D:\Text1.txt", true);
            writer.WriteLine(str);
            writer.Close();
        }

        //Дастаём новости
//        public List<ArticlesViewModel> GetTitlesHabr()
//        {
//            var i = 0;
//            string сontent;
//            //ListModels listModels = new ListModels();
//            List<ArticlesViewModel> listModels = new List<ArticlesViewModel>();
//            var config = Configuration.Default.WithDefaultLoader();
//            var context = BrowsingContext.New(config);
//            var watch = System.Diagnostics.Stopwatch.StartNew();
//            //1.7
//            var document = context.OpenAsync("https://habr.com/ru/news/").Result;
//            watch.Stop();
//            var elapsedMs = watch.ElapsedMilliseconds;

//            watch.Restart();
//            //0.033
//            var items = document.QuerySelectorAll("a.post__title_link");
//            elapsedMs = watch.ElapsedMilliseconds;


//            foreach (var item in items)
//            {
//                watch.Restart();
//                //0.002
//                var link = item.GetAttribute("href").ToString();
//                elapsedMs = watch.ElapsedMilliseconds;

//                watch.Restart();
//                //0.439
//                document = context.OpenAsync(link).Result;
//                elapsedMs = watch.ElapsedMilliseconds;

//                watch.Restart();
//                //0.002
//                сontent = document.QuerySelector("div.post__text").TextContent.ToString();
//                elapsedMs = watch.ElapsedMilliseconds;

//                watch.Restart();
//                //0.001
//                listModels.Add(new ArticlesViewModel
//                {
//                    Article = item.Text(),
//                    Link = item.GetAttribute("href"),
//                    FullContent = сontent,
//                    PartContent = GetContent(сontent)
//                });
//                elapsedMs = watch.ElapsedMilliseconds;
//                i++;
//            }
//            return listModels;
//        }

//        public List<ArticlesViewModel> GetTitlesTutBy()
//        {
//            var watch = System.Diagnostics.Stopwatch.StartNew();
//            var i = 0;
//            List<ArticlesViewModel> listModels = new List<ArticlesViewModel>();
//            //ListModels listModels = new ListModels();
//            var config = Configuration.Default.WithDefaultLoader();
//            var document = BrowsingContext.New(config).OpenAsync("https://news.tut.by/?sort=time").Result;

//            watch.Stop();
//            var elapsedMs = watch.ElapsedMilliseconds;

//            watch.Restart();
//            var Items = document.QuerySelectorAll("div.m-sorted");
//            elapsedMs = watch.ElapsedMilliseconds;

//            string parttext = "";
//            if (Items != null)
//            {
//                for (int k = 0; k < Items.Length; k++)
//                {
//                    var item_link = Items[k].QuerySelectorAll("a.entry__link");
//                    for (int j = 0; j < item_link.Length; j += +2)
//                    {
//                        watch.Restart();
//                        //переходит на статью по ссылке
//                        document = BrowsingContext.New(config).OpenAsync(item_link[j].GetAttribute("href").ToString()).Result;
//                        //получает DOM объект в котором хранится заголовок
//                        elapsedMs = watch.ElapsedMilliseconds;
//                        watch.Restart();
//                        var article = document.QuerySelector("h1");
//                        var blockContent = document.QuerySelector("div.js-mediator-article");
//                        if (blockContent != null)
//                        {
//                            var full = blockContent.QuerySelectorAll("p");
//                            var h = 0;
//                            var fulltext = "";
//                            foreach (var text in full)
//                            {
//                                if (h == 0) { parttext = text.Text(); h++; }
//                                fulltext += (text.Text() + "\n");
//                            }
//                            listModels.Add(new ArticlesViewModel
//                            {
//                                Article = article.Text(),
//                                Link = item_link[j].GetAttribute("href"),
//                                FullContent = fulltext,
//                                PartContent = GetContent(parttext)
//                            });
//                            i++;
//                            if (i == 20) { return listModels; }
//                        }
//                        elapsedMs = watch.ElapsedMilliseconds;
//                    }
//                }
//            }
//            return listModels;
//        }

//        public List<ArticlesViewModel> GetTitlesBelta()
//        {
//            List<ArticlesViewModel> listModels = new List<ArticlesViewModel>();
//            //ListModels listModels = new ListModels();
//            var config = Configuration.Default.WithDefaultLoader();
//            var document = BrowsingContext.New(config).OpenAsync("https://www.belta.by/all_news/").Result;
//            var items = document.QuerySelectorAll("div.lenta_info");
//            string partContent, link, content;
//            for (int i = 0; i < 20; i++)
//            {
//                //ссылка
//                link = items[i].QuerySelector("a.lenta_info_title").GetAttribute("href");
//                if (!link.Contains("https://www.belta.by"))
//                {
//                    link = "https://www.belta.by" + link;
//                }
//                document = BrowsingContext.New(config).OpenAsync(link).Result;
//                //содеражание статьи
//                try
//                {
//                    content = document.QuerySelector("div.js-mediator-article").TextContent;
//                }
//                catch (Exception ex)
//                {
//                    continue;
//                }
//                try
//                {
//                    //краткое описание
//                    items[i].QuerySelector("div.lenta_textsmall").Text();
//                    partContent = items[i].QuerySelector("div.lenta_textsmall").Text();
//                }
//                catch (System.ArgumentNullException)
//                {
//                    partContent = content;
//                }
//                listModels.Add(new ArticlesViewModel
//                {
//                    Article = items[i].QuerySelector("a.lenta_info_title").Text(),
//                    Link = link,
//                    FullContent = document.QuerySelector("div.js-mediator-article").TextContent,
//                    PartContent = GetContent(partContent)
//                });
//            }
//            return listModels;
//        }
//        private string GetContent(string Content)
//        {
//            int count = 100;
//            string str;
//            if (Content.Length > 100)
//            {
//                while (Content.Substring(count, 1) != " " && Content.Substring(count, 1) != ".")
//                {
//                    count++;
//                }

//                str = Content.Substring(0, count);
//            }
//            else
//            {
//                str = Content;
//            }
//            return str;
//        }

//        ///запихиваем в базу
//        ParserContext db = new ParserContext();
//        public void Create()
//        {
//            int count = 0;
//        var listModels = new List<List<ArticlesViewModel>>();
//        var articles = new List<Article>();
//        var model = GetTitlesHabr();
//        listModels.Add(model);
//            model = GetTitlesTutBy();
//            listModels.Add(model);
//            model = GetTitlesBelta();
//            listModels.Add(model);
//            for (int i = 0; i<listModels.Count; i++)
//            {
//                for(int j = 0; j<listModels[i].Count; j++)
//                {
//                    //display(Convert.ToString(count));
//                    //display(Convert.ToString(listModels[i].Count));
//                    var article = new Article { SiteId = 1, Url = listModels[i][j].Link, Title = listModels[i][j].Article, PartContent = listModels[i][j].PartContent, Content = listModels[i][j].FullContent };
//                    articles.Add(article);
//                    //db.Articles.Add(article);
//                                //string str = listModels[i][j].Link + " " + listModels[i][j].Article + " " + listModels[i][j].PartContent + " " + listModels[i][j].FullContent;
//                                //display(str);
//                 // count++;
//                }
//}
//            //var sites = db.Sites.ToList();
//            //foreach (var site in sites)
//            //{
//            //    string fer = site.Domain + site.Name + site.Id.ToString();
//            //    display(fer);
//            //}
//        }

    }
}
