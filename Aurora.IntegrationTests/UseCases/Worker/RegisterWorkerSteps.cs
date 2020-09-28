using Aurora.Application;
using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Aurora.IntegrationTests.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Aurora.IntegrationTests.UseCases.Worker
{
    [Binding]
    public class RegisterWorkerSteps
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly CreateWorkerModel _createWorkerModel;
        private WorkerModel _workerModel;
        private int _workerId;

        public RegisterWorkerSteps()
        {
            _server = ServerSetup.Setup();
            _client = _server.CreateClient();
            _createWorkerModel = new CreateWorkerModel();
        }

        [Given(@"the worker which the name is '(.*)'")]
        public void GivenTheWorkerWhichTheNameIs(string name) =>
            _createWorkerModel.Name = name;

        [Given(@"the bith date is '(.*)'")]
        public void GivenTheBithDateIs(string birthDate) =>
            _createWorkerModel.BirthDate = Convert.ToDateTime(birthDate);

        [Given(@"the NIN is '(.*)'")]
        public void GivenTheNINIs(string nin) =>
            _createWorkerModel.Nin = nin;

        [Given(@"the Password is '(.*)'")]
        public void GivenThePasswordIs(string password) =>
            _createWorkerModel.Password = password;
        
        [When(@"the register is done")]
        public async Task WhenTheRegisterIsDone()
        {
            try
            {
                var temp = (IRepositoryWorker)_server.Services.GetService(typeof(IRepositoryWorker));
                

                var data = JsonConvert.SerializeObject(_createWorkerModel);
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                
                var response = await _client.PostAsync("api/users", stringContent);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var result = await response.Content.ReadAsStringAsync();
                _workerId = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        [When(@"i recover the worker by id")]
        public async Task GivenIRecoverTheWorkerById()
        {
            var response = await _client.GetAsync($"api/users/{_workerId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            _workerModel = JsonConvert.DeserializeObject<WorkerModel>(result);
        }
        
        [Then(@"the worker exists, returned, on the database")]
        public void ThenTheIdExistsReturnedOnTheDatabase()
        {
            

            Assert.Equal(_createWorkerModel.Name, _workerModel.Name);
            Assert.Equal(_createWorkerModel.BirthDate, _workerModel.BirthDate);
            Assert.Equal(_createWorkerModel.Nin, _workerModel.Nin);
        }
    }
}
