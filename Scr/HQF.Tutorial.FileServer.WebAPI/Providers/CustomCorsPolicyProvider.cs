using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

namespace HQF.Tutorial.FileServer.WebAPI.Providers
{
    public class CustomCorsPolicyProvider : ICorsPolicyProvider
    {
        public const string CorsOriginsSettingKey = "WebApi.CorsOrgins";
        public Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
        {
            var corsPolicy = new CorsPolicy();


            if (request.Uri.PathAndQuery.StartsWith("/api/Files"))
            {
                corsPolicy.AllowAnyHeader = true;
                corsPolicy.AllowAnyMethod = true;
                corsPolicy.AllowAnyOrigin = true;
            }

            // Add allowed origins.
            //corsPolicy.Origins.Add("http://www.itelite.cn");

            var origins = ConfigurationManager.AppSettings[CorsOriginsSettingKey];

            if (!string.IsNullOrEmpty(origins))
            {
                foreach (var origin in origins.Split(';'))
                {
                    corsPolicy.Origins.Add(origin);
                }
            }
            else
            {
                corsPolicy.AllowAnyOrigin = true;
            }
           

            var tsc = new TaskCompletionSource<CorsPolicy>();
            tsc.SetResult(corsPolicy);
            return tsc.Task;
        }

       
    }
}
