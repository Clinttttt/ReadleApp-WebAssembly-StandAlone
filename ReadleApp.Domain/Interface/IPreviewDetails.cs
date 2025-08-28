using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Interface
{
    public interface IPreviewDetails
    {
        Task<OpenLibraryPreviewDetails> PreviewAsync(OpenLibraryDoc doc);
    }
}
