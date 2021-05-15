using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using BuscarinoFaceDetection.Models;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;

namespace BuscarinoFaceDetection.Services
{
    public  class HelperServices : IHelperServices
    {
        private readonly IFileSystem _fileSystem;

        public HelperServices(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        /// <summary>
        /// Get file paths for all files in a directory
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <returns></returns>
        public string[] GetImageFilePaths(string sourceDirectory)
        {
            var files = _fileSystem.Directory.GetFiles(sourceDirectory);
            return files;
        }

        /// <summary>
        /// From files use cloud vision face detector to generate a list showing how many faces
        /// were detected in each file with at least 70% confidence
        /// </summary>
        /// <param name="files"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<FaceResult> GetFaceResults(string[] files, ImageAnnotatorClient client)
        {
            var faceResults = new List<FaceResult>();

            foreach (var file in files)
            {
                var response = GetFaceAnnotationResponse(client, file);

                var faceResult = new FaceResult
                {
                    Name = Path.GetFileName(file),
                    Count = response.Count(a => a.DetectionConfidence >= .7)
                };
                faceResults.Add(faceResult);
            }

            return faceResults;
        }

        /// <summary>
        /// Generate and send request for a single image to Google cloud vision for detecting faces
        /// </summary>
        /// <param name="client"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<FaceAnnotation> GetFaceAnnotationResponse(ImageAnnotatorClient client, string file)
        {
            var image = Image.FromFile($"{file}");
            var response = client.DetectFaces(image);
            return response;
        }

        /// <summary>
        /// Create json file from list of face results
        /// </summary>
        /// <param name="faceResults"></param>
        public void WriteJsonResults(List<FaceResult> faceResults)
        {
            var directory = VerifyDirectory(@$"{Path.GetTempPath()}\Buscarino");

            var fileName =
                @$"{directory}/{DateTime.UtcNow:yyyyMMddHHmmss}.txt";

            using StreamWriter file = File.CreateText(fileName);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, faceResults);
        }

        public string VerifyDirectory(string path)
        {
            string directory = path;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
