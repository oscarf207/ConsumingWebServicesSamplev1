using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{

    ///////////////////////////////////////////////////  REQUEST MODEL::

    public class AuthRequestModel
    {
        public String ClientId { get; set; }

        public String ClientPassword { get; set; }

    }

    public class GetDataRequestModel
    {
        public String CustomerId { get; set; }
    }

    public class SendDataRequestModel
    {
        public String TransactionId { get; set; }

        public String UserEmail { get; set; }

        public String UserKeyword { get; set; }
    }

    public class GenerateSmsRequestModel
    {
        public String TransactionId { get; set; }
    }

    public class ValidateSmsRequestModel
    {
        public String TransactionId { get; set; }
        public String Code { get; set; }
    }

    public class CloseOperationRequestModel
    {
        public String TransactionId { get; set; }

        public int AcceptTermsData { get; set; }
    }

    /////////////////////////////////////////////////// RESPONSE MODEL::

    public class BaseResponseModel                  /////   BaseResponseModel 
    {
        public int ResponseCode { get; set; }

        public String ResponseMessage { get; set; }
    }

    public class AuthResponseModel : BaseResponseModel
    {
        public String AuthKey { get; set; }

    }


    public class GetDataResponseModel : BaseResponseModel
    {
        public String CustomerId { get; set; }

        public String TransactionId { get; set; }

        public String CurreentService { get; set; }

        public String ProposedService { get; set; }

        public String AdditionalService { get; set; }

        public String TermsAndConditions { get; set; }

        public String UserFullName { get; set; }

        public String UserServiceNumber { get; set; }

        
    }

    public class SendDataResponseModel : BaseResponseModel
    {

        public String TransactionId { get; set; }

    }

    public class GenerateSmsResponseModel : BaseResponseModel
    {
        public String TransactionId { get; set; }

    }

    public class ValidateSmsResponseModel : BaseResponseModel
    {
        public String TransactionId { get; set; }
    }

    public class CloseOperationResponseModel : BaseResponseModel
    {
        public String TransactionId { get; set; }

        public String TransactionOperation { get; set; }

        public String TransactionDetail { get; set; }
    }


    /////////////////////////////////////////////////// CLASS PROGRAM

    class Program
    {
        static HttpClient client = new HttpClient();        

        ////////////////////////////////////////////    GETTING URI
        private static HttpClient getPhoenixClient(string token)
        {
            client.BaseAddress = new Uri(Constants.URL_BASE_PARAM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if(token != null)
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



        //////////////////////////////////////////// GET auth
        public static async Task<AuthResponseModel> authRequest(AuthRequestModel request)
        {
             HttpClient cTest = getPhoenixClient();

            HttpResponseMessage response = await cTest.PostAsJsonAsync("auth", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //AuthResponseModel r = await response.Content.ReadAsAsync<AuthResponseModel>(new[] { new JsonMediaTypeFormatter() });
                AuthResponseModel r = await response.Content.ReadAsAsync<AuthResponseModel>();
                Console.WriteLine("Auth::");
                return r;
            }
            else
            {
                return null;
            }
            
        }



        //-------------
        //////////////////////////////////////////// GET getData
        public static async Task<GetDataResponseModel> getDataRequest(GetDataRequestModel request)
        {
            //HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest.PostAsJsonAsync("getData", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //GetDataResponseModel r = await response.Content.ReadAsAsync<GetDataResponseModel>(new[] { new JsonMediaTypeFormatter() });
                GetDataResponseModel r = await response.Content.ReadAsAsync<GetDataResponseModel>();
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
            //HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest.PostAsJsonAsync("sendData", request);
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
            //HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest.PostAsJsonAsync("generateSms", request);
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
            //HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest.PostAsJsonAsync("validateSms", request);
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
            //HttpClient c = getPhoenixClient();// fixing with change its position get out this method.
            HttpResponseMessage response = await cTest.PostAsJsonAsync("closeOperation", request);
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


        static HttpClient cTest = getPhoenixClient();


        ////////////PRINTS
        static void ShowAuth(AuthResponseModel authT)
        {
           Console.WriteLine($"key: {authT.AuthKey}\tresponseCode: {authT.ResponseCode}\tresponseMessage: {authT.ResponseMessage}");
        }

        static void ShowGetData(GetDataResponseModel getDataT)
        {
            Console.WriteLine($"customerId: {getDataT.CustomerId}\ttransactionId: {getDataT.TransactionId}\tcurrentService: {getDataT.CurreentService}\tproposedService: {getDataT.ProposedService}\tadditionalService: {getDataT.AdditionalService}" +
                $"\ntermsAndConditions: {getDataT.TermsAndConditions}\tuserFullName: {getDataT.UserFullName}\tuserServiceNumber: {getDataT.UserServiceNumber}\tresponseCode: {getDataT.ResponseCode}\tresponseMessage: {getDataT.ResponseMessage}");
        }
        static void ShowSendData(SendDataResponseModel sendDataT)
        {
            Console.WriteLine($"transActionId: {sendDataT.TransactionId}\tresponseCode: {sendDataT.ResponseCode}\tresponseMessage: {sendDataT.ResponseMessage}");
        }
        static void ShowGenerateSms(GenerateSmsResponseModel generateSmsT)
        {
            Console.WriteLine($"transActionId: {generateSmsT.TransactionId}\tresponseCode: {generateSmsT.ResponseCode}\tresponseMessage: {generateSmsT.ResponseMessage}");
        }

        static void ShowValidateSms(ValidateSmsResponseModel validateSmsT)
        {
            Console.WriteLine($"transActionId: {validateSmsT.TransactionId}\tresponseCode: {validateSmsT.ResponseCode}\tresponseMessage: {validateSmsT.ResponseMessage}");
        }

        static void ShowCloseOperation(CloseOperationResponseModel closeOperationT)
        {
            Console.WriteLine($"transActionId: {closeOperationT.TransactionId}\ttransActionOperation: {closeOperationT.TransactionOperation}\ttransactionDetail: {closeOperationT.TransactionDetail}\tresponseCode: {closeOperationT.ResponseCode}\tresponseMessage: {closeOperationT.ResponseMessage}");
        }


        

        ///////////////////////////////////////////////////     MAIN
         static async Task Main()
        {

            //RunAllAsync().GetAwaiter().GetResult();


            AuthRequestModel r1 = new AuthRequestModel() { ClientId = "clientId", ClientPassword = "dqwdqwdqd"};

            GetDataRequestModel r2 = new GetDataRequestModel() { CustomerId = "71010245" };

            SendDataRequestModel r3 = new SendDataRequestModel() { TransactionId = "10012102120", UserEmail = "asdasdasd", UserKeyword = "asdasdww" };

            GenerateSmsRequestModel r4 = new GenerateSmsRequestModel() { TransactionId = "10012102120" };

            ValidateSmsRequestModel r5 = new ValidateSmsRequestModel() { Code = "10012102120", TransactionId = "assddasdw" };

            CloseOperationRequestModel r6 = new CloseOperationRequestModel() { AcceptTermsData = 322322, TransactionId = "asdasdasdw" };


            try
            {



                AuthResponseModel authTest = await authRequest(r1);


               ShowAuth(authTest);
                
                GetDataResponseModel getDataT = await getDataRequest(r2);
                ShowGetData(getDataT);

                SendDataResponseModel sendDataT = await sendDataRequest(r3);
                ShowSendData(sendDataT);

                GenerateSmsResponseModel generateSmsT = await generateSmsRequest(r4);
                ShowGenerateSms(generateSmsT);

                ValidateSmsResponseModel validateSmsT = await validateSmsRequest(r5);
                ShowValidateSms(validateSmsT);

                CloseOperationResponseModel closeOperationT = await closeOperationRequest(r6);
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