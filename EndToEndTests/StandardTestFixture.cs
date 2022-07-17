using Example.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EndToEndTests
{
    public class StandardTestFixture :IDisposable
    {
        public StandardTestFixture()
        {
            WebFactory = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                    //this will be called inside builder.Build() in Program.cs
                   builder.ConfigureServices((services) => {

                   });
               });

        }

        public HttpClient CreateClient() => WebFactory.CreateClient();

        public void Dispose()
        {
            WebFactory.Dispose();
        }

        public WebApplicationFactory<Program> WebFactory { get; }
    }
    
}