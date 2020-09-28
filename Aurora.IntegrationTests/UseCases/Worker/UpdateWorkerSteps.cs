using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Aurora.IntegrationTests.Configurations;
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
    public class UpdateWorkerSteps
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IServiceWorker _serviceWorker;
        private readonly CreateWorkerModel _createWorkerModel;
        private WorkerModel _existsWorkerModel;
        private WorkerModel _recoverWorkerModel;
        private int _workerId;

        public UpdateWorkerSteps()
        {
            _server = ServerSetup.Setup();
            _client = _server.CreateClient();
            _serviceWorker = (IServiceWorker)_server.Services.GetService(typeof(IServiceWorker));
            _createWorkerModel = new CreateWorkerModel();
        }

        [Given(@"an worker called '(.*)'")]
        public void GivenAnExistsWorkerCalled(string name) =>
            _createWorkerModel.Name = name;

        [Given(@"his birth date is '(.*)'")]
        public void GivenHisBirthDateIs(string birthDate) =>
            _createWorkerModel.BirthDate = Convert.ToDateTime(birthDate);

        [Given(@"his Password is '(.*)'")]
        public void GivenHisPasswordIs(string password) =>
            _createWorkerModel.Password = password;

        [Given(@"his Nin is '(.*)'")]
        public void GivenHisNinIs(string nin) =>
            _createWorkerModel.Nin = nin;

        [Then(@"i insert this woker on database")]
        public void ThenIInsertThisWorkOnDatabase() =>
            _existsWorkerModel = _serviceWorker.Insert(_createWorkerModel);
        
        [When(@"i update this worker's name to '(.*)'")]
        public async Task WhenIUpdateThisWorkerSName(string name)
        {
            var updateWorker = new UpdateWorkerModel
            {
                Id = _existsWorkerModel.Id,
                BirthDate = _existsWorkerModel.BirthDate,
                Nin = _existsWorkerModel.Nin,
                Name = name
            };

            try
            {
                var data = JsonConvert.SerializeObject(updateWorker);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"api/users/{_existsWorkerModel.Id}", content);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                _existsWorkerModel.Name = name;
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
        
        [When(@"i update this worker's nin to '(.*)'")]
        public async Task WhenIUpdateThisWorkerSNin(string nin)
        {
            var updateWorker = new UpdateWorkerModel
            {
                Id = _existsWorkerModel.Id,
                BirthDate = _existsWorkerModel.BirthDate,
                Nin = nin,
                Name = _existsWorkerModel.Name
            };

            try
            {
                var data = JsonConvert.SerializeObject(updateWorker);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"api/users/{_existsWorkerModel.Id}", content);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                _existsWorkerModel.Nin = nin;
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
        
        [Then(@"i recover this worker")]
        public async Task ThenIRecoverThisWorker()
        {
            try
            {
                var response = await _client.GetAsync($"api/users/{_existsWorkerModel.Id}");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var result = await response.Content.ReadAsStringAsync();
                _recoverWorkerModel = JsonConvert.DeserializeObject<WorkerModel>(result);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
        
        [Then(@"verify the worker's name")]
        public void ThenVerifyTheWorkerSName()
        {
            Assert.Equal(_existsWorkerModel.Name, _recoverWorkerModel.Name);
        }
        
        [Then(@"verify the worker's nin")]
        public void ThenVerifyTheWorkerSNin()
        {
            Assert.Equal(_existsWorkerModel.Nin, _recoverWorkerModel.Nin);
        }
    }
}
