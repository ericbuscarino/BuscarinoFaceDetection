using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuscarinoFaceDetection.Models;
using BuscarinoFaceDetection.Services;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Options;

namespace BuscarinoFaceDetection
{
    public class Application
    {
        private readonly IFileSystem _fileSystem;
        private readonly IHelperServices _helper;

        public Application(IFileSystem fileSystem, IHelperServices helper)
        {
            _fileSystem = fileSystem;
            _helper = helper;
        }
        public void Run(){
            try
            {
                var client = ImageAnnotatorClient.Create();

                var sourceDirectory =
                    @$"{AppContext.BaseDirectory}\Images";

                var files = _helper.GetImageFilePaths(sourceDirectory);

                var faceResults = _helper.GetFaceResults(files, client);

                _helper.WriteJsonResults(faceResults);
            }
            catch (AnnotateImageException e)
            {
                AnnotateImageResponse response = e.Response;
                Console.WriteLine(response.Error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
