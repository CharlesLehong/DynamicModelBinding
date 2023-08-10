using DynamicModelBinding.Model;
using DynamicModelBinding.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DynamicModelBinding.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BlobStorageService blobStorageService;

        public IndexModel(BlobStorageService blobStorageService)
        {
            this.blobStorageService = blobStorageService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetContentFileMarkUp()
        {
            return this.Partial("_ContentFile");
        }


        public async Task<IActionResult> OnGetImage([FromQuery]int contentFileId)
        {
            //Assume these are files in the database 
            var files = new List<PostFile>()
            {
                new PostFile()
                {
                    ContentFileId = 1,
                    BlobId = new Guid("58a04815-4c56-407c-985f-9e3f2f3e089f"),
                    ContentType = "image/jpeg",
                },
                new PostFile()
                {
                    ContentFileId = 2,
                    BlobId = new Guid("79664a97-d936-4e94-a366-a5940c5e1d2f"),
                    ContentType = "image/jpeg",
                }
            };

            //Assume this is database call
            var file = files.FirstOrDefault(f => f.ContentFileId == contentFileId); 


            var image = await this.blobStorageService.DownloadBlobAsyc(file.BlobId.ToString());
            return new JsonResult(new { contentType = file.ContentType, content = Convert.ToBase64String(image) });
        }
    }
}