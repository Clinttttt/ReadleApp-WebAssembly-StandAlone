using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReadleApp.Client;
using ReadleApp.Client.Services;
using ReadleApp.Domain.Interface;
using ReadleApp.Infrastructure.Services;
using ReadleApp.Infrastructure.Services.IndexDb;
using TG.Blazor.IndexedDB;
using ReadleApp.Infrastructure;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<BookStateService>();
builder.Services.AddScoped<BookClientServices>();
builder.Services.AddScoped<IBookRespository, BookRespository>();
builder.Services.AddScoped<BookApiServices>();
builder.Services.AddScoped<PageState>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "ReadleDb";
    dbStore.Version = 23;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Books",
        PrimaryKey = new IndexSpec
        {
            Name = "_Id",
            KeyPath = "_Id",
            Auto = true
        },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec{Name = "_Workkey",        KeyPath = "_Workkey",     Auto = false},
            new IndexSpec{Name = "_Title",          KeyPath = "_Title",       Auto = false},
            new IndexSpec{Name = "_AuthorName",     KeyPath = "_AuthorName",  Auto = false},
            new IndexSpec{Name = "_Description",    KeyPath = "_Description", Auto = false},
            new IndexSpec{Name = "_IA",             KeyPath = "_IA",          Auto = false}


        }
    });
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7033")
});

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/

await builder.Build().RunAsync();
