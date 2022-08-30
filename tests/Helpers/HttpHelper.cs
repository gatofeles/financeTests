using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tests.Helpers
{
    public class HttpHelper
    {
        public RestClient client { get; set; }       

        public HttpHelper()
        {
            var url = Environment.GetEnvironmentVariable("BACKEND").ToString();
            client = new RestClient(url);
        }

        public List<string> MakeGetRequest(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var result = client.Execute<List<String>>(request).Data;
            return result;
        }

        public RestResponse GetJwtToken()
        {

            var request = new RestRequest("users/login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            var credentials = new Credentials();
            credentials.email = Environment.GetEnvironmentVariable("EMAIL").ToString();
            credentials.password = Environment.GetEnvironmentVariable("PASSWORD").ToString();
            string credsJson = JsonConvert.SerializeObject(credentials);

            request.AddBody(credsJson);

            return client.Execute(request);
        }

        public RestResponse CreateTransactions(Dictionary<string, string> body, string jwt)
        {

            var request = new RestRequest("transactions/", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("x-auth-token", jwt);
            var transaction = new Transaction();
            transaction.title = body["title"];
            transaction.description = body["description"];
            transaction.cost = body["cost"];
            transaction.userId = body["userId"];

            string transJson = JsonConvert.SerializeObject(transaction);

            request.AddBody(transJson);

            return client.Execute(request);
        }

        public Transaction GetTransactionById(string id, string jwt)
        {

            var request = new RestRequest($"transactions/{id}", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("x-auth-token", jwt);
            var response = client.Execute(request);
            if(response != null || (int)response.StatusCode != 200)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            var transaction = JsonConvert.DeserializeObject<Transaction>(response.Content);
            
            return transaction;

            
        }

        public RestResponse DeleteTransaction(Transaction transaction, string jwt)
        {
            var request = new RestRequest("transactions/", Method.Delete);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("x-auth-token", jwt);

            string transJson = JsonConvert.SerializeObject(transaction);

            request.AddBody(transJson);

            return client.Execute(request);
        }

        public RestResponse UpdateTransaction(Transaction transaction, string jwt)
        {

            var request = new RestRequest("transactions/", Method.Put);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("x-auth-token", jwt);
            //var tran = new Transaction();
            //tran.title = transaction.title;
            //tran._id = transaction._id.ToString();
            //tran.description = transaction.description;
            //tran.cost = transaction.cost;

            string transJson = JsonConvert.SerializeObject(transaction);

            request.AddBody(transJson);

            return client.Execute(request);
        }



    }
}
