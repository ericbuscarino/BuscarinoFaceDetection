using System;
using BuscarinoFaceDetection.Services;
using Google.Cloud.Vision.V1;

namespace BuscarinoFaceDetection
{
    public class Application
    {
        private readonly IHelperServices _helper;

        public Application(IHelperServices helper)
        {
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
