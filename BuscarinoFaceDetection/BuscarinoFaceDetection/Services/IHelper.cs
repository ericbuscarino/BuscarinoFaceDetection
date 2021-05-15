using System.Collections.Generic;
using BuscarinoFaceDetection.Models;
using Google.Cloud.Vision.V1;

namespace BuscarinoFaceDetection.Services
{
    public interface IHelperServices
    {
        public string[] GetImageFilePaths(string sourceDirectory);
        public List<FaceResult> GetFaceResults(string[] files, ImageAnnotatorClient client);
        public IReadOnlyList<FaceAnnotation> GetFaceAnnotationResponse(ImageAnnotatorClient client, string file);
        public void WriteJsonResults(List<FaceResult> faceResults);
    }
}
