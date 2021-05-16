# BuscarinoFaceDetection
## Summary
This is a basic proof of concept console app using .net core 5. It takes supplied test images, uses the Google Computer Vision API to detect faces, and outputs a json file indicating the count of faces detected with at least 70% accuracy

## Notes on Implementation
* API Key - The value for the private key and private key are missing from the versioned code and are included in my email sharing the link to this repository.
* JSON Output - A file is output to %appdata%\Local\Temp\Buscarino. The directory will be created if it does not currently exist

## Expanding this app for production, other devs, greater than 2 hours of work
* Complete unit testing - Expand unit testing to cover mocking responses from the google api and file creation.
* Validation that all files are acceptable image files
* Update to web user interface for easy UI in supplying images and providing result file for download
* Implementation of secrets and using configuration to inject the api key and id into the ImmageAnnotatorClientBuilder
* Implement async version of api calls
* Potentially implement batch annotation calls instead of doing one call per image
* Refactor HelperServices
  * Refactor GetImageFilePaths into a broader repository for accessing files and allowing for other sources of images
  * Refactor GetFaceResults to allow for user input of confidence and move it into its own factory
  * Refactor GetFaceAnnotations to allow for it to inherited by our own class and wrapped in an interface to make app agnostic on source of data and make the response mockable
  * Refactor VerifyDirectory and WriteJsonResults to their own service

## Test Results
### Summary
Group Name: BuscarinoFaceDetection.Test
Duration: 0:00:00.349
0 test(s) failed
0 test(s) skipped
5 test(s) passed

### Unit Tests
Written
* GetImageFilePath correctly returns an empty array if no images exist in directory
* GetImageFilePath returns an array of paths for each image in the directory
* VerifyDirectory creates directory if it does not exist in the supplied path and returns path
* VerifyDirectory returns existing directory path if it exists in supplied path

Minimum needed
* GetFaceResults correctly returns face results for empty file array and variety of file arrays supplied with mock response from google api. Currently the results response is to coupled to the api call
* GetFaceAnnotations needs testing for a variety of responses from the api as well as invalid credentials and no response from the api.

