using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ReadleApp.Infrastructure.Services
{
    public class MappToOffline : IMapToOffline
    {
        private readonly HttpClient _http;
        private readonly BookServerServices _bookServer;
        public MappToOffline(HttpClient http, BookServerServices bookServer)
        {
            _http = http;
            _bookServer = bookServer;
        }


         public async Task<string?> ImageBase64(int? coverkey)
         {
            string? data = null;
            if (!coverkey.HasValue) return null;
            for (int i = 0; i < 3; i++)
            {
                try
            {           
                   data = await _bookServer.GetBase64(coverkey!.Value);
                    break;
                }
                catch (HttpRequestException) when (i < 2)
                {
                    await Task.Delay(1000);
                }
            }    
            return data;
        }


        public async Task<OfflineReadingModel> GetReadingModelAsync(OpenLibraryDoc doc)
        {

            var workstring = doc.WorkKey != null ? doc.WorkKey.Replace("/works/", "") ?? "" : null;
            var responsework = await _bookServer.GetDetails(workstring!);


            var description = responsework!.DescriptionRaw;
            string? descip = null;
            if (description is not null && description is JsonElement element)
            {
                if (element.ValueKind == JsonValueKind.String) descip = element.GetString();
                if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var val))
                {
                    descip = val.GetString();
                }

            }
            var FirstIa = doc.IA!.FirstOrDefault();
            string? FullPlainText = null;
            if (!string.IsNullOrEmpty(FirstIa))
            {
                FullPlainText = await _bookServer.GetFulltext(FirstIa);
            }
            return new OfflineReadingModel
            {
                _Workkey = doc.WorkKey,
                _IA = FirstIa,
                _AuthorName = doc.AuthorName,
                _CoverBase64 = await ImageBase64(doc.CoverKey),
                _Title = doc.Title,
                _FullText = FullPlainText,
                _Description = descip,

            };
        }




    }
}


/*  public async Task<string?> ImageBase64(int? coverkey)
        {
            byte[]? image = null;
            if (!coverkey.HasValue) return null;
            var fetch = $"https://covers.openlibrary.org/b/id/{coverkey}-L.jpg";
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    image = await _http.GetByteArrayAsync(fetch);
                    break;
                }catch(HttpRequestException) when ( i < 2)
                {
                    await Task.Delay(1000);
                }
            }
            return Convert.ToBase64String(image!);
        }*/





/*  public async Task<List<OfflineReadingModel>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=romance&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OfflineReadingModel>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _toOffline.GetReadingModelAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }*/