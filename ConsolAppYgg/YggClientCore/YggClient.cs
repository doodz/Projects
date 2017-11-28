using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace YggClientCore
{
    public class YggClient
    {
        private CookieContainer _cookies;
        private HttpClientHandler _httpClientHandler;
        private readonly string _index = "https://yggtorrent.com";
        private readonly string _login = "https://yggtorrent.com/user/login";
        private readonly string _register = "https://yggtorrent.com/user/register";



        public YggClient()
        {
            _cookies = new CookieContainer();
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.CookieContainer = _cookies;
        }

        //public async Task<IActionResult> Index()
        //{
        //    HttpClient hc = new HttpClient();
        //    HttpResponseMessage result = await hc.GetAsync($"http://{HttpContext.Request.Host}/page.html");

        //    Stream stream = await result.Content.ReadAsStreamAsync();

        //    HtmlDocument doc = new HtmlDocument();

        //    doc.Load(stream);

        //    HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

        //    return View(links);
        //}


        public async Task Index()
        {
            var html = await DownloadPageAsync(_index);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            IsConnected(doc);
        }


        public async Task Seach()
        {
            var html = await DownloadPageAsync("https://yggtorrent.com/engine/search?q=toto&order=desc&sort=name");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var table = doc.DocumentNode.SelectSingleNode("//table//tbody");
            var torrents = table.ChildNodes.Where(n => n.Name == "tr");


            foreach (var torrent in torrents)
            {



            }
        }

        public async Task LoginPage()
        {
            //var html = await DownloadPageAsync(_login);
            await PostLogin();
            await Index();


        }
        public async Task PostLogin()
        {
            // id = doods & pass = *4teBFemJGb4EC8y625 % 25 & submit =
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.CookieContainer = _cookies;

            using (var client = new HttpClient(_httpClientHandler))
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", "doods"),
                    new KeyValuePair<string, string>("pass", "*4teBFemJGb4EC8y625%"),
                    new KeyValuePair<string, string>("submit", string.Empty)
                });
                using (var response = await client.PostAsync(_login, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Longin ok");
                        var responseCookies = _cookies.GetCookies(new Uri(_login)).Cast<Cookie>();
                    }

                }
            }
        }



        private bool IsConnected(HtmlDocument doc)
        {

            var link = doc.DocumentNode.SelectSingleNode("//a[@href='https://yggtorrent.com/user/login']");

            return link == null;
            //var links = doc.DocumentNode.SelectNodes("//a[@href]");
            //return links.All(l => l.InnerText != "Connexion");

        }

        private async Task<string> DownloadPageAsync(string targetPage)
        {

            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.CookieContainer = _cookies;
            // ... Use HttpClient.
            using (var client = new HttpClient(_httpClientHandler))
            using (var response = await client.GetAsync(targetPage))
            using (var content = response.Content)
            {


                // ... Read the string.
                var result = await content.ReadAsStringAsync();

                // ... Display the result.
                if (result != null &&
                    result.Length >= 50)
                {
                    Console.WriteLine(result.Substring(0, 50) + "...");
                }
                return result;
            }
        }
    }
}
