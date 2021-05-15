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
        public void Run()
        {
            try
            {
                Console.WriteLine("Hold on, we're finding faces...");

                var client = ImageAnnotatorClient.Create();

                var sourceDirectory =
                    @$"{AppContext.BaseDirectory}\Images";

                var files = _helper.GetImageFilePaths(sourceDirectory);

                Console.WriteLine("Analyzing faces");

                var faceResults = _helper.GetFaceResults(files, client);

                _helper.WriteJsonResults(faceResults);

                Console.WriteLine("All done, the faces have been analyzed");
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
