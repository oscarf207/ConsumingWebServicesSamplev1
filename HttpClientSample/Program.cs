using System;
using System.Net;
using System.Net.Http;
////////using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
//using System.IO;
////////using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
//using System.Text.Json.Serialization;

namespace HttpClientSample
{

    ///////////////////////////////////////////////////  REQUEST MODEL::

    //[Serializable]    
    public class AuthRequestModel
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }
        [JsonPropertyName("clientpassword")]
        public string ClientPassword { get; set; }

    }

    public class GetDataRequestModel
    {
        [JsonPropertyName("customerId")]
        public String CustomerId { get; set; }
    }

    public class SendDataRequestModel
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("useremail")]
        public string UserEmail { get; set; }

        [JsonPropertyName("userkeyword")]
        public string UserKeyword { get; set; }
    }

    public class GenerateSmsRequestModel
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }
    }

    public class ValidateSmsRequestModel
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    public class CloseOperationRequestModel
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("acceptTermsData")]
        public int AcceptTermsData { get; set; }
    }

    /////////////////////////////////////////////////// RESPONSE MODEL::

    public class BaseResponseModel                  /////   BaseResponseModel 
    {
        [JsonPropertyName("responsecode")]
        public int ResponseCode { get; set; }
        [JsonPropertyName("responsemessage")]
        public string ResponseMessage { get; set; }
    }

    public class AuthResponseModel : BaseResponseModel
    {
        [JsonPropertyName("authkey")]
        public String AuthKey { get; set; }

        public AuthResponseModel(){}

    }


    public class GetDataResponseModel : BaseResponseModel
    {
        [JsonPropertyName("customerId")]
        public String CustomerId { get; set; }

        [JsonPropertyName("transactionId")]
        public String TransactionId { get; set; }

        [JsonPropertyName("currentservice")]
        public String CurreentService { get; set; }

        [JsonPropertyName("proposedservice")]
        public String ProposedService { get; set; }

        [JsonPropertyName("additionalservice")]
        public String AdditionalService { get; set; }

        [JsonPropertyName("termsandconditions")]
        public String TermsAndConditions { get; set; }

        [JsonPropertyName("userfullname")]
        public String UserFullName { get; set; }

        [JsonPropertyName("userservicenumber")]
        public String UserServiceNumber { get; set; }

        
    }

    public class SendDataResponseModel : BaseResponseModel
    {
        [JsonPropertyName("transactionId")]
        public String TransactionId { get; set; }

    }

    public class GenerateSmsResponseModel : BaseResponseModel
    {
        [JsonPropertyName("transactionId")]
        public String TransactionId { get; set; }

    }

    public class ValidateSmsResponseModel : BaseResponseModel
    {
        [JsonPropertyName("transactionId")]
        public String TransactionId { get; set; }
    }

    public class CloseOperationResponseModel : BaseResponseModel
    {
        [JsonPropertyName("transactionId")]
        public String TransactionId { get; set; }

        [JsonPropertyName("transactionOperation")]
        public String TransactionOperation { get; set; }

        [JsonPropertyName("transactionDetail")]
        public String TransactionDetail { get; set; }
    }


    /////////////////////////////////////////////////// CLASS PROGRAM

    class Program
    {




        //static HttpClient client = new HttpClient();
        //private static readonly IHttpClientFactory httpClientFactory;        
        //HttpClient httpClientFactory;
        //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
        private  static async Task<AuthResponseModel> GetResponseAsync(string requestname , AuthRequestModel parameter)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(parameter);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); 
            
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new AuthResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }                        
        }


        private static async Task<GetDataResponseModel> GetDataResponseAsync(string requestname, GetDataRequestModel requestname2, AuthResponseModel token)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(requestname2);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //request.Headers = new HttpRequestHeaders()
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService");          
            //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AuthKey);
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<GetDataResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new GetDataResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }
        }


        private static async Task<SendDataResponseModel> SendDataResponseAsync(string requestname, SendDataRequestModel requestname2, string token)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(requestname2);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //request.Headers = new HttpRequestHeaders()
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService");          
            //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<SendDataResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new SendDataResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }
        }



        private static async Task<GenerateSmsResponseModel> GenerateSMSResponseAsync(string requestname, GenerateSmsRequestModel requestname2, string token)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(requestname2);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //request.Headers = new HttpRequestHeaders()
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService");          
            //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<GenerateSmsResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new GenerateSmsResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }
        }


        private static async Task<ValidateSmsResponseModel> ValidateSMSResponseAsync(string requestname, ValidateSmsRequestModel requestname2, string token)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(requestname2);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //request.Headers = new HttpRequestHeaders()
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService");          
            //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<ValidateSmsResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new ValidateSmsResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }
        }



        private static async Task<CloseOperationResponseModel> CloseOperationResponseAsync(string requestname, CloseOperationRequestModel requestname2, string token)
        {
            //var clint = HttpClientFactory;
            var request = new HttpRequestMessage(HttpMethod.Post, requestname);
            var serialize = JsonSerializer.Serialize(requestname2);
            //request.RequestUri = new Uri(Constants.URL_BASE_PARAM);
            //httpClientFactory cln = new Uri()

            request.Content = new StringContent(serialize, System.Text.Encoding.UTF8, "application/json");
            //request.Headers = new HttpRequestHeaders()
            //var clientIH = httpClientFactory.CreateClient("VerifyService"); //getClient
            //var clientIH = httpClientFactory.CreateClient("VerifyService");          
            //client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //clientIH.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //var response = await clientIH.SendAsync(request);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();//////////  INSPECTION POINT TO VISUALIZE THE VALUES OF INPUTS!!!!!!!
                var responseModel = await JsonSerializer.DeserializeAsync<CloseOperationResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                // using var responseStream2 = await response.Content.ReadAsStreamAsync();
                // var responseModel = await JsonSerializer.DeserializeAsync<AuthResponseModel>(responseStream2);
                // return responseModel;
                return new CloseOperationResponseModel() { ResponseCode = (int)response.StatusCode };
                //return <AuthResponseModel> ;
            }
        }



        //static HttpClient client = new HttpClient();
        static HttpClient client = new HttpClient();
        ////////////////////////////////////////////    GETTING URI
        private static HttpClient getPhoenixClient(string token)
        {
            client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != "a")
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
        



        /////// TEST A ONLY METHOD

        /*

        //////////////////////////////////////////// GET auth
        
        

        public static async Task<AuthResponseModel> authRequest(AuthRequestModel request)
        {
            //Console.WriteLine("authReq::");
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest1.PostAsJsonAsync<AuthRequestModel>("auth", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Console.WriteLine("StatusOk::");
                //string json = JsonConvert.SerializeObject(request);
                //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //AuthResponseModel r = await response.Content.ReadAsAsync<AuthResponseModel>(new[] { new JsonMediaTypeFormatter() });
                AuthResponseModel r = await response.Content.ReadAsAsync<AuthResponseModel>();
                Console.WriteLine("auth::");
                return r;
            }
            else
            {
                //Console.WriteLine("Status 500::");
                return null;
            }
        }
        
        //-------------
        //////////////////////////////////////////// GET getData
        public static async Task<GetDataResponseModel> getDataRequest(GetDataRequestModel request, string token)
        {
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.            
            cTest1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            HttpResponseMessage response = await cTest1.PostAsJsonAsync("getData", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //GetDataResponseModel r = await response.Content.ReadAsAsync<GetDataResponseModel>(new[] { new JsonMediaTypeFormatter() });
                GetDataResponseModel r = await response.Content.ReadAsAsync<GetDataResponseModel>();
                Console.WriteLine("GetData::"+token);
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
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest1.PostAsJsonAsync("sendData", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //SendDataResponseModel r = await response.Content.ReadAsAsync<SendDataResponseModel>(new[] { new JsonMediaTypeFormatter() });
                SendDataResponseModel r = await response.Content.ReadAsAsync<SendDataResponseModel>();
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
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest1.PostAsJsonAsync("generateSms", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //GenerateSmsResponseModel r = await response.Content.ReadAsAsync<GenerateSmsResponseModel>(new[] { new JsonMediaTypeFormatter() });
                GenerateSmsResponseModel r = await response.Content.ReadAsAsync<GenerateSmsResponseModel>();
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
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest1.PostAsJsonAsync("validateSms", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //ValidateSmsResponseModel r = await response.Content.ReadAsAsync<ValidateSmsResponseModel>(new[] { new JsonMediaTypeFormatter() });
                ValidateSmsResponseModel r = await response.Content.ReadAsAsync<ValidateSmsResponseModel>();
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
            //HttpClient cTest = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest1.PostAsJsonAsync("closeOperation", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //CloseOperationResponseModel r = await response.Content.ReadAsAsync<CloseOperationResponseModel>(new[] { new JsonMediaTypeFormatter() });
                CloseOperationResponseModel r = await response.Content.ReadAsAsync<CloseOperationResponseModel>();
                Console.WriteLine("CloseOperation::");
                return r;
            }
            else
            {
                return null;
            }

        }

        
        ////////////static HttpClient cTest1 = getPhoenixClient();
        //static HttpClient cTest2 = getPhoenixClient(test1.Tken);
        //static HttpClient cTest2;
        */
        //HttpClient cTest1 = getPhoenixClient();

        ////////////PRINTS
        static void ShowAuth(AuthResponseModel authT)
        {
           Console.WriteLine($"Auth credentials.\nkey: {authT.AuthKey}\nresponseCode: {authT.ResponseCode}\nresponseMessage: {authT.ResponseMessage}\n\n");
           //test1.Tken = authT.AuthKey;
            //cTest2 = getPhoenixClient(authT.AuthKey);
            
        }

        static void ShowGetData(GetDataResponseModel getDataT)
        {
            Console.WriteLine($"GetData credentials.\ncustomerId: {getDataT.CustomerId}\ntransactionId: {getDataT.TransactionId}\ncurrentService: {getDataT.CurreentService}\nproposedService: {getDataT.ProposedService}\nadditionalService: {getDataT.AdditionalService}" +
                $"\ntermsAndConditions: {getDataT.TermsAndConditions}\nuserFullName: {getDataT.UserFullName}\nuserServiceNumber: {getDataT.UserServiceNumber}\nresponseCode: {getDataT.ResponseCode}\nresponseMessage: {getDataT.ResponseMessage}\n\n");
        }
        static void ShowSendData(SendDataResponseModel sendDataT)
        {
            Console.WriteLine($"SendData credentials.\ntransActionId: {sendDataT.TransactionId}\nresponseCode: {sendDataT.ResponseCode}\nresponseMessage: {sendDataT.ResponseMessage}\n\n");
        }
        static void ShowGenerateSms(GenerateSmsResponseModel generateSmsT)
        {
            Console.WriteLine($"GenerateSMS credentials.\ntransActionId: {generateSmsT.TransactionId}\nresponseCode: {generateSmsT.ResponseCode}\nresponseMessage: {generateSmsT.ResponseMessage}\n\n");
        }

        static void ShowValidateSms(ValidateSmsResponseModel validateSmsT)
        {
            Console.WriteLine($"ValidateSMS credentials.\ntransActionId: {validateSmsT.TransactionId}\nresponseCode: {validateSmsT.ResponseCode}\nresponseMessage: {validateSmsT.ResponseMessage}\n\n");
        }

        static void ShowCloseOperation(CloseOperationResponseModel closeOperationT)
        {
            Console.WriteLine($"CloseOperation credentials.\ntransActionId: {closeOperationT.TransactionId}\ntransActionOperation: {closeOperationT.TransactionOperation}\ntransactionDetail: {closeOperationT.TransactionDetail}\nresponseCode: {closeOperationT.ResponseCode}\nresponseMessage: {closeOperationT.ResponseMessage}\n\n");
        }


        

        ///////////////////////////////////////////////////     MAIN
         static async Task Main()
        {

            client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);

            //RunAllAsync().GetAwaiter().GetResult();
            AuthRequestModel r1 = new AuthRequestModel() { ClientId = "Messenger", ClientPassword = "GNJHY!LASfa3FHLF55sdf_HH" };
            GetDataRequestModel r2 = new GetDataRequestModel() { CustomerId = "00000102" };
            
            
            CloseOperationRequestModel r6 = new CloseOperationRequestModel() { AcceptTermsData = 322322, TransactionId = "asdasdasdw" };

            try
            {                
                var abs = await GetResponseAsync("auth", r1); /////////// METHOD WITH JSONSERALIZER                
                //AuthResponseModel authTest = await authRequest(r1);
                ShowAuth(abs);
                
                
                GetDataResponseModel getDataT = await GetDataResponseAsync("GetData",r2, abs);
                ShowGetData(getDataT);
                SendDataRequestModel r3 = new SendDataRequestModel() { TransactionId = getDataT.TransactionId, UserEmail = "abc@hotmail.com", UserKeyword = "ABC" };


                SendDataResponseModel sendDataT = await SendDataResponseAsync("SendData",r3,abs.AuthKey);
                ShowSendData(sendDataT);

                GenerateSmsRequestModel r4 = new GenerateSmsRequestModel() { TransactionId = sendDataT.TransactionId };

                GenerateSmsResponseModel generateSmsT = await GenerateSMSResponseAsync("GenerateSMS",r4,abs.AuthKey);
                ShowGenerateSms(generateSmsT);

                ValidateSmsRequestModel r5 = new ValidateSmsRequestModel() { Code = "6666", TransactionId = generateSmsT.TransactionId };

                ValidateSmsResponseModel validateSmsT = await ValidateSMSResponseAsync("ValidateSMS",r5,abs.AuthKey);
                ShowValidateSms(validateSmsT);

                CloseOperationResponseModel closeOperationT = await CloseOperationResponseAsync("CloseOperation",r6,abs.AuthKey);
                ShowCloseOperation(closeOperationT);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);            
            }
            Console.ReadLine();
        }


    }

}