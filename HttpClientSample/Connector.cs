using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{

    //public class BusinessLogicClass : Singleton<BusinessLogicClass> {
   
public sealed class  Connector
    {

        private static readonly Connector instance = new Connector();

        public static HttpClient client = new HttpClient();
        private static object cTest;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Connector()
        {
        }

        private Connector()
        {
        }

        public static Connector Instance
        {
            get
            {
                return instance;
            }
        }

        private static HttpClient getPhoenixClient(string token)
        {
            client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private static HttpClient getPhoenixClient()
        {
            client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }


        public void abc()
        {

        }

        //////////////////////////////////////////// GET auth
        //public  async Task<AuthResponseModel> authRequest(AuthRequestModel request)
        //{
        //    Connector q = Connector.Instance.;


        //    HttpResponseMessage response = await q. PostAsJsonAsync("auth", request);
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        AuthResponseModel r = await response.Content.ReadAsAsync<AuthResponseModel>(new[] { new JsonMediaTypeFormatter() });
        //        Console.WriteLine("Auth::");
        //        return r;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}



        //-------------
        //////////////////////////////////////////// GET getData
        public static async Task<GetDataResponseModel> getDataRequest(GetDataRequestModel request)
        {
            HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await c.PostAsJsonAsync("getData", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                GetDataResponseModel r = await response.Content.ReadAsAsync<GetDataResponseModel>(new[] { new JsonMediaTypeFormatter() });
                Console.WriteLine("GetData::");
                return r;
            }
            else
            {
                return null;
            }

        }


        //-------------
        //////////////////////////////////////////// GET sendData
        public static async Task<SendDataResponseModel> sendDataRequest(SendDataRequestModel request)
        {
            HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await c.PostAsJsonAsync("sendData", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                SendDataResponseModel r = await response.Content.ReadAsAsync<SendDataResponseModel>(new[] { new JsonMediaTypeFormatter() });
                Console.WriteLine("SendData::");
                return r;
            }
            else
            {
                return null;
            }

        }


        //-------------
        //////////////////////////////////////////// GET generateSms
        public static async Task<GenerateSmsResponseModel> generateSmsRequest(GenerateSmsRequestModel request)
        {
            HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await c.PostAsJsonAsync("generateSms", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                GenerateSmsResponseModel r = await response.Content.ReadAsAsync<GenerateSmsResponseModel>(new[] { new JsonMediaTypeFormatter() });
                Console.WriteLine("GenerateSms::");
                return r;
            }
            else
            {
                return null;
            }

        }

        //-------------
        //////////////////////////////////////////// GET validateSms
        public static async Task<ValidateSmsResponseModel> validateSmsRequest(ValidateSmsRequestModel request)
        {
            HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await c.PostAsJsonAsync("validateSms", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ValidateSmsResponseModel r = await response.Content.ReadAsAsync<ValidateSmsResponseModel>(new[] { new JsonMediaTypeFormatter() });
                Console.WriteLine("ValidateSms::");
                return r;
            }
            else
            {
                return null;
            }

        }

        //-------------
        //////////////////////////////////////////// GET closeOperation
        public static async Task<CloseOperationResponseModel> closeOperationRequest(CloseOperationRequestModel request)
        {
            HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await c.PostAsJsonAsync("closeOperation", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                CloseOperationResponseModel r = await response.Content.ReadAsAsync<CloseOperationResponseModel>(new[] { new JsonMediaTypeFormatter() });
                Console.WriteLine("CloseOperation::");
                return r;
            }
            else
            {
                return null;
            }

        }


















    }


    
}
