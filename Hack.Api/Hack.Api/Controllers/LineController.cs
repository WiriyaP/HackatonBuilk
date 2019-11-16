using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hack.Api.Controllers
{
    public class LineController : Controller
    {
        // GET: /<controller>/
        public async Task lineNotifyAsync(string msg)
        {
            string token = "f9yXB77b4XUAhnxGC6rG9LBKET12fIKWARPTJDUaZTu";
            try
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "message", msg },
                });
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.PostAsync("https://notify-api.line.me/api/notify", content);
                if (response.IsSuccessStatusCode)
                {
                    var a = response;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        
    }
}
