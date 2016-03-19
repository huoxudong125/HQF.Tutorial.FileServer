#HQF.Tutorial.FileServer

##How To Host The Server
The File Server two way host:
 1. `IIS` 
 2. SelfHost using `Owin` 

##Using `MulitPart` to upload the files



##Async  
Update the files using asynchonizition


##How Deal With The Big Files

``` csharp
var config = new HttpSelfHostConfiguration("http://localhost:8080");

//make the MaxReceivedMessageSize to MaxValue of int.
config.MaxReceivedMessageSize = int.MaxValue;

//Enable the Buffered TransferMode,make it possible to write and receive file at the same time.
config.TransferMode = TransferMode.Buffered;

config.Routes.MapHttpRoute(
    name: "DefaultApi",
    routeTemplate: "api/{controller}/{action}"
);

HttpSelfHostServer server = new HttpSelfHostServer(config);

server.OpenAsync().Wait();

Console.WriteLine("Server Started..");
```


##Resources
[HTML5 drag and drop asynchronous multi file upload with ASP.NET WebAPI](http://www.strathweb.com/2012/04/html5-drag-and-drop-asynchronous-multi-file-upload-with-asp-net-webapi/)   
[Troubleshooting HTTP 405 Errors after Publishing Web API 2 Applications](http://www.asp.net/web-api/overview/testing-and-debugging/troubleshooting-http-405-errors-after-publishing-web-api-applications)   
[How to get PUT and DELETE verbs to work with WebAPI on IIS](https://stackoverflow.com/questions/28579073/how-to-get-put-and-delete-verbs-to-work-with-webapi-on-iis)  
[IIS 7.5 + enable PUT and DELETE for RESTFul service, extensionless](https://stackoverflow.com/questions/6739124/iis-7-5-enable-put-and-delete-for-restful-service-extensionless)     