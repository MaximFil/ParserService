//using ParserService.Controller;
//using ParserService.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ParserService.Controllers
//{
//    class CrudArticles
//    {
//        static bool enabled;
//        ParserContext db = new ParserContext();
//        public CrudArticles()
//        {
//            enabled = true;
//        }
//        public void Create()
//        {
//            var parse = new Parse();
//            var listModels = new List<List<ArticlesViewModel>>();
//            var articles=new List<Article>();
//            var model = parse.GetTitlesTutBy();
//            listModels.Add(model);
//            model = parse.GetTitlesTutBy();
//            listModels.Add(model);
//            model = parse.GetTitlesBelta();
//            listModels.Add(model);
//            for (int i = 0; i < listModels.Count; i++)
//            {
//                for(int j = 0; j < listModels[i].Count; i++)
//                {
//                    var article = new Article { SiteId=1, Url=listModels[i][j].Link, Title=listModels[i][j].Article,PartContent=listModels[i][j].PartContent,Content=listModels[i][j].FullContent};
//                    articles.Add(article);
//                }
//            }
//            db.Articles.AddRange(articles);
//        }

//        public void Start()
//        {
//            System.IO.StreamWriter writer = new System.IO.StreamWriter(@"D:\Text1.txt", true);
//            writer.WriteLine("\n" + DateTime.Now.ToString() + "Begin");
//            while (enabled)
//            {               
//                writer.WriteLine("\n" + DateTime.Now.ToString() + "Start");
//                Create();
//                System.Threading.Thread.Sleep(1000);              
//                writer.WriteLine("\n" + DateTime.Now.ToString()+"End");
//                writer.Close();

//            }
//        }
//        public void Stop()
//        {
//            enabled=false;
//        }
//    }
//}
