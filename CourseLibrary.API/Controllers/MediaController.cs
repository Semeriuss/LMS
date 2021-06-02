using CourseLibrary.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[Route("api/videos")]
public class VideosController : ControllerBase
{
    [HttpGet]
    public HttpResponseMessage Get(string filename, string ext)
    {
        var video = new VideoStream(filename, ext);

        var result = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new PushStreamContent(video.WriteToStream, new MediaTypeHeaderValue("application/octet-stream" + ext)),
        };

        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return result;

    }

    [HttpGet]
    [Route("Download")]
    public IActionResult Download()
    {
        string path = @"C:\Users\semer\Downloads\Video\1.mp4";
        //var memory = new MemoryStream();
        //using (FileStream stream = new FileStream(@"C:\Users\semer\Downloads\Video\1.mp4", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan))
        //{
        //    await stream.CopyToAsync(memory);
        //}
        //memory.Position = 0;
        //return File(memory, "application/octet-stream", Path.GetFileName(path), true); //enableRangeProcessing = true
        return PhysicalFile(path, "application/octet-stream", enableRangeProcessing: true);
    }


}