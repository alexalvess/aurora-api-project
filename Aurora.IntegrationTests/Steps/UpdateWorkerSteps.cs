using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Aurora.IntegrationTests.Drivers;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Aurora.IntegrationTests.Steps
{
    [Binding, Scope(Tag = "updateWorker")]
    public class UpdateWorkerSteps
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IServiceWorker _serviceWorker;
        private CreateWorkerModel _createWorkerModel;
        private WorkerModel _existsWorkerModel;
        private WorkerModel _recoverWorkerModel;

        public UpdateWorkerSteps()
        {
            _server = ServerSetup.Setup();
            _client = _server.CreateClient();
            _serviceWorker = (IServiceWorker)_server.Services.GetService(typeof(IServiceWorker));
        }

        [Given(@"an worker's data:")]
        public void GivenAnWorkersData(Table table) =>
            _createWorkerModel = table.CreateInstance<CreateWorkerModel>();

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
        public void ThenVerifyTheWorkerSName() =>
            Assert.Equal(_existsWorkerModel.Name, _recoverWorkerModel.Name);

        [Then(@"verify the worker's nin")]
        public void ThenVerifyTheWorkerSNin() =>
            Assert.Equal(_existsWorkerModel.Nin, _recoverWorkerModel.Nin);
    }
}
