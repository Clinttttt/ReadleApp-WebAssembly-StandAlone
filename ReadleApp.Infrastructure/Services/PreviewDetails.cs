using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Infrastructure.Services
{
    public class PreviewDetails : IPreviewDetails
    {
        private readonly HttpClient _http;
        private readonly BookServerServices _bookServer;
        private readonly IMapToOffline _info;
        public PreviewDetails(HttpClient http, BookServerServices bookServer, IMapToOffline info)
        {
            _http = http;
            _bookServer = bookServer;
            _info = info;
        }
        public async Task<OpenLibraryPreviewDetails> PreviewAsync(OpenLibraryDoc doc)
        {
            var workstring = doc.WorkKey != null ? doc.WorkKey.Replace("/works/", "") ?? "" : null;
            var responsework = await _bookServer.GetWorkDetails(workstring!);
            var details = new OpenLibraryPreviewDetails
            {
                _Workkey = doc.WorkKey,
                _coverkey = doc.CoverKey,
                
            };
            return details;
        }
    }

}
