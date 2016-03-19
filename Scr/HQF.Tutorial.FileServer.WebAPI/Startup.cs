using System.Threading.Tasks;
using System.Web.Http;
using HQF.Tutorial.FileServer.WebAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(HQF.Tutorial.FileServer.WebAPI.Startup))]

namespace HQF.Tutorial.FileServer.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

           

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            WebApiConfig.Register(httpConfiguration);


            SwaggerConfig.Register(httpConfiguration);

            //Allow Cors
            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CustomCorsPolicyProvider()
            };
            app.UseCors(corsOptions);


            app.UseWebApi(httpConfiguration);


            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}