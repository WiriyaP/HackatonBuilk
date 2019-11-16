using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hack.api.Controllers
{
    [Route("api/[controller]")]
    public class FaceController : Controller
    {
        [HttpGet("CreateCol")]
        public async void CreateCol()
        {
            string accessKey = "AKIAST4HFDODRNXMOAPJ";
            string secretKey = "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";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(accessKey, secretKey, Amazon.RegionEndpoint.APSoutheast1);

            String collectionId = "faceCollection";
            Console.WriteLine("Creating collection: " + collectionId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                CollectionId = collectionId
            };

            CreateCollectionResponse createCollectionResponse = await rekognitionClient.CreateCollectionAsync(createCollectionRequest);
            Console.WriteLine("CollectionArn : " + createCollectionResponse.CollectionArn);
            Console.WriteLine("Status code : " + createCollectionResponse.StatusCode);

        }

        [HttpGet("Addface")]
        public async void Addface()
        {
            String collectionId = "faceCollection";
            String bucket = "face-identify";
            String photo = "BrotherP.jpg";

            string accessKey = "AKIAST4HFDODRNXMOAPJ";
            string secretKey = "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";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(accessKey, secretKey, Amazon.RegionEndpoint.APSoutheast1);

            Image image = new Image()
            {
                S3Object = new S3Object()
                {
                    Bucket = bucket,
                    Name = photo
                }
            };

            IndexFacesRequest indexFacesRequest = new IndexFacesRequest()
            {
                Image = image,
                CollectionId = collectionId,
                ExternalImageId = photo,
                DetectionAttributes = new List<String>() { "ALL" }
            };

            IndexFacesResponse indexFacesResponse = await rekognitionClient.IndexFacesAsync(indexFacesRequest);

            Console.WriteLine(photo + " added");
            foreach (FaceRecord faceRecord in indexFacesResponse.FaceRecords)
                Console.WriteLine("Face detected: Faceid is " +
                   faceRecord.Face.FaceId);
        }

        [HttpGet("SearchFace")]
        public async void SearchFace()
        {
            String collectionId = "faceCollection";
            String bucket = "face-identify";
            String photo = "BrotherP.jpg";

            string accessKey = "AKIAST4HFDODRNXMOAPJ";
            string secretKey = "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";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(accessKey, secretKey, Amazon.RegionEndpoint.APSoutheast1);

            // Get an image object from S3 bucket.
            Image image = new Image()
            {
                S3Object = new S3Object()
                {
                    Bucket = bucket,
                    Name = photo
                }
            };

            SearchFacesByImageRequest searchFacesByImageRequest = new SearchFacesByImageRequest()
            {
                CollectionId = collectionId,
                Image = image,
                FaceMatchThreshold = 70F,
                MaxFaces = 2
            };

            SearchFacesByImageResponse searchFacesByImageResponse = await rekognitionClient.SearchFacesByImageAsync(searchFacesByImageRequest);

            Console.WriteLine("Faces matching largest face in image from " + photo);
            foreach (FaceMatch face in searchFacesByImageResponse.FaceMatches)
                Console.WriteLine("FaceId: " + face.Face.FaceId + ", Similarity: " + face.Similarity);
        }
    }
}
