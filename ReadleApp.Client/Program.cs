using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReadleApp.Client;
using ReadleApp.Client.Services;
using ReadleApp.Domain.Interface;
using ReadleApp.Infrastructure.Services;
using ReadleApp.Infrastructure.Services.IndexDb;
using TG.Blazor.IndexedDB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<BookStateService>();
builder.Services.AddScoped<BookClientServices>();
builder.Services.AddScoped<IBookRespository, BookRespository>();
builder.Services.AddScoped<BookApiServices>();
builder.Services.AddScoped<PageState>();

builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "ReadleDb";
    dbStore.Version = 15;

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
            new IndexSpec {Name = "WorkKey",         KeyPath = "WorkKey",        Auto = false},
            new IndexSpec {Name = "Title",           KeyPath = "Title",          Auto = false},
            new IndexSpec {Name = "CoverIdAt",       KeyPath = "CoverIdAt",      Auto = false},
            new IndexSpec {Name = "EditionCount",    KeyPath = "EditionCount",   Auto = false},
            new IndexSpec {Name = "Category",        KeyPath = "Category",       Auto = false},
            new IndexSpec {Name = "Isbn13",          KeyPath = "Isbn13",         Auto = false},
            new IndexSpec {Name = "languages",       KeyPath = "languages",      Auto = false},
            new IndexSpec {Name = "DescriptionRaw",  KeyPath = "DescriptionRaw", Auto = false},
            new IndexSpec {Name = "Subjects",        KeyPath = "Subjects",       Auto = false},
            new IndexSpec {Name = "Ebooks",          KeyPath = "Ebooks",         Auto = false},
            new IndexSpec {Name =  "Authors",        KeyPath = "Authors",        Auto = false},
            new IndexSpec {Name =  "CoverIdAlt",     KeyPath = "CoverIdAlt",     Auto = false},
            new IndexSpec {Name =  "PublishDate",    KeyPath = "PublishDate",    Auto = false},
            new IndexSpec {Name =  "Formats",        KeyPath = "Formats",        Auto = false},
            new IndexSpec {Name =  "Availability",   KeyPath = "Availability",   Auto = false},
            new IndexSpec {Name =  "Identifier",     KeyPath = "Identifier",     Auto = false},         
            new IndexSpec {Name =  "WorkKeys",       KeyPath = "WorkKeys",       Auto = false},
            new IndexSpec {Name = "GetBookshelves",  KeyPath = "GetBookshelves", Auto = false},


        }
    });
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7033")
});

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/

await builder.Build().RunAsync();
