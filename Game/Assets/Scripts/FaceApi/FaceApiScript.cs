using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;
using Newtonsoft.Json;

namespace FaceApi
{
    [RequireComponent(typeof(WebCamScript))]
    public class FaceApiScript : MonoBehaviour
    {
        public delegate void OnRequestHandler(FaceResult result);
        
        private WebCamScript webCamScript;

        public static event OnRequestHandler OnRequest;

        public static bool used;
        
        public IEnumerator AnalyzePicture()
        {
            yield return new WaitWhile(() => used);

            used = true;
            
            Debug.Log("Позируйте!");
            StartCoroutine(webCamScript.WriteTextureToFile());
            yield return new WaitForSeconds(1);
            try
            {
                MakeAnalysisRequest(webCamScript.PathToFile);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            used = false;
        }
        // Start is called before the first frame update
        void Start()
        {
            webCamScript = GetComponent<WebCamScript>();
//            StartCoroutine(AnalyzePicture());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        private const string subscriptionKey = "161c22e8d1344d208cafe68180738ac3";
        private const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";
    
        static async void MakeAnalysisRequest(string filePath)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                                       "&returnFaceAttributes=emotion";

            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            var data = GetImageAsByteArray(filePath);
            using (var content = new ByteArrayContent(data))
            {
                try
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                    
                    var contentString = await response.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<List<FaceResult>>(contentString);

                    OnRequest?.Invoke(json[0]);

                    Debug.Log(json[0]);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        static byte[] GetImageAsByteArray(string imageFile)
        {
            using (FileStream fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int) fileStream.Length);
            }
        }
    }
}
