using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReadleApp.Client;
using TG.Blazor.IndexedDB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<BookApi>();
builder.Services.AddScoped<IndexDb>();
builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "ReadleDb";
    dbStore.Version = 3;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Books",
        PrimaryKey = new IndexSpec
        {
            Name = "Id",
            KeyPath = "Id",
            Auto = true
        },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec { Name = "BookId",        KeyPath = "BookId",        Auto = false },
            new IndexSpec { Name = "Content",       KeyPath = "Content",       Auto = false },
            new IndexSpec { Name = "DownloadCount", KeyPath = "DownloadCount", Auto = false },
            new IndexSpec { Name = "BookShelves",   KeyPath = "BookShelves",   Auto = false },
            new IndexSpec { Name = "Subjects",      KeyPath = "Subjects",      Auto = false },
            new IndexSpec { Name = "Title",         KeyPath = "Title",         Auto = false },
            new IndexSpec { Name = "Authors",       KeyPath = "Authors",       Auto = false },
            new IndexSpec { Name = "Languages",     KeyPath = "Languages",     Auto = false },
            new IndexSpec { Name = "Summaries",     KeyPath = "Summaries",     Auto = false },
            new IndexSpec { Name = "Formats",       KeyPath = "Formats",       Auto = false },
            new IndexSpec { Name = "CoverUrl",      KeyPath = "CoverUrl",      Auto = false }
        }
    });
});

builder.Services.AddScoped(sp => new HttpClient
{
    Timeout = TimeSpan.FromSeconds(30)
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
