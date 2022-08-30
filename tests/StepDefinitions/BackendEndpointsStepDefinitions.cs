using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using tests.Helpers;

namespace tests.StepDefinitions
{
    

    [Binding]

    public class BackendEndpointsStepDefinitions
    {
        public ScenarioContext _scenarioContext;
        public BackendEndpointsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I'm logged in the backend rest api")]
        public void GivenImLoggedInTheBackendRestApi()
        {
            var http = new HttpHelper();
            var result = http.GetJwtToken();
            Assert.IsTrue((int)result.StatusCode == 200);
            _scenarioContext["jwt"] = result.Content;
        }

        [When(@"I create a positive transaction with the following values")]
        public void WhenICreateAPositiveTransactionWithTheFollowingValues(Table table)
        {
            var tableDic = table.Rows.ToDictionary(r => r["field"], r => r["value"]);
            var http = new HttpHelper();
            var result = http.CreateTransactions(tableDic, _scenarioContext["jwt"].ToString());

            _scenarioContext["resultStatus"] = (int)result.StatusCode;
            _scenarioContext["content"] = result.Content;


        }

        [Then(@"I should get a (.*) response")]
        public void ThenIShouldGetAResponse(int status)
        {
            Assert.True((int)_scenarioContext["resultStatus"] == status);
        }

        [Then(@"the data should be available in the database")]
        public void ThenTheDataShouldBeAvailableInTheDatabase()
        {
            throw new PendingStepException();
        }

        [Given(@"I've created a transaction with the following values")]
        public void GivenIveCreatedATransactionWithTheFollowingValues(Table table)
        {
            var tableDic = table.Rows.ToDictionary(r => r["field"], r => r["value"]);
            var http = new HttpHelper();
            var result = http.CreateTransactions(tableDic, _scenarioContext["jwt"].ToString());
            
            var transaction = JsonConvert.DeserializeObject<Transaction>(result.Content);
            _scenarioContext["resultStatus"] = (int)result.StatusCode;
            _scenarioContext["content"] = result.Content;
            _scenarioContext["transaction"] = transaction;

        }

        [When(@"I change the description (.*)")]
        public void WhenIChangeTheDescription(string description)
        {
            var http = new HttpHelper();
            var tran = (Transaction)_scenarioContext["transaction"];
            tran.description = description;
            var result = http.UpdateTransaction(tran, _scenarioContext["jwt"].ToString());

            _scenarioContext["resultStatus"] = (int)result.StatusCode;
            _scenarioContext["content"] = result.Content;
        }



        [When(@"I delete that transaction")]
        public void WhenIDeleteThatTransaction()
        {
            var http = new HttpHelper();
            var tran = (Transaction)_scenarioContext["transaction"];
            var result = http.DeleteTransaction(tran, _scenarioContext["jwt"].ToString());

            _scenarioContext["resultStatus"] = (int)result.StatusCode;
            _scenarioContext["content"] = result.Content;
        }

    }
}
