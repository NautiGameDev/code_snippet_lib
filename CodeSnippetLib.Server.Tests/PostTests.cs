using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using CodeSnippetLib.Server.DTOs.Account;
using CodeSnippetLib.Server.DTOs.Post;
using CodeSnippetLib.Server.Tests.Helpers;
using CodeSnippetLib.Server.Tests.TestData;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit.Abstractions;

namespace CodeSnippetLib.Server.Tests
{
    public class PostTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PostTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost")
            });
            _output = output;
            _factory = factory;
        }

        private async Task<HttpResponseMessage> HandleLogin(string username, string password, string fromTest)
        {
            var expectedStatusCode = HttpStatusCode.OK;

            LoginDto loginPayload = new LoginDto()
            {
                UserName = username,
                Password = password
            };

            var response = await _client.PostAsJsonAsync("/api/account/login", loginPayload);

            if (!response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                string testResult = expectedStatusCode == response.StatusCode ? "Passed" : "Failed";

                string output = EntryLogHandler.GetEntryString(
                "Login to test posts",
                $"Attempted login from test {fromTest}",
                $"{(int)expectedStatusCode} {expectedStatusCode}",
                $"{(int)response.StatusCode} {response.StatusCode}",
                $"{responseBody}",
                $"{testResult}"
                );
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        [Fact]
        public async Task CreatePost_ValidAuthorization_Returns201Created()
        {
            var expectedStatusCode = HttpStatusCode.Created;

            await HandleLogin(AdminUser.UserName, AdminUser.Password, "CreatePost_ValidAuthorization");

            NewPostDto newPostPayload = new NewPostDto()
            {
                Title = "Test post",
                Content = "This is a test post",
                Tags = ["tag1", "tag2"]
            };

            var response = await _client.PostAsJsonAsync("/api/post", newPostPayload);
            string responseBody = await response.Content.ReadAsStringAsync();

            string testResult = expectedStatusCode == response.StatusCode ? "Passed" : "Failed";

            string output = EntryLogHandler.GetEntryString(
                "CreatePost",
                "Create new post with valid authorization",
                $"{(int)expectedStatusCode} {expectedStatusCode}",
                $"{(int)response.StatusCode} {response.StatusCode}",
                $"{responseBody}",
                $"{testResult}"
                );

            WriteTestResults.AppendData(output);

            _output.WriteLine(output);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task CreatePost_InvalidAuthorization_Returns403Forbidden()
        {
            var expectedStatusCode = HttpStatusCode.Forbidden;

            await HandleLogin(TestUser.UserName, TestUser.Password, "CreatePost_InvalidAuthorization");

            NewPostDto newPostPayload = new NewPostDto()
            {
                Title = "Test post from non admin",
                Content = "This is a test post without authorization",
                Tags = ["tag1", "tag2"]
            };

            var response = await _client.PostAsJsonAsync("/api/post", newPostPayload);
            string responseBody = await response.Content.ReadAsStringAsync();

            string testResult = expectedStatusCode == response.StatusCode ? "Passed" : "Failed";

            string output = EntryLogHandler.GetEntryString(
                "CreatePost",
                "Create new post with invalid authorization",
                $"{(int)expectedStatusCode} {expectedStatusCode}",
                $"{(int)response.StatusCode} {response.StatusCode}",
                $"{responseBody}",
                $"{testResult}"
                );

            WriteTestResults.AppendData(output);

            _output.WriteLine(output);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task GetPost_ValidSlug_Returns200Ok()
        {
            var expectedStatusCode = HttpStatusCode.OK;

            await HandleLogin(AdminUser.UserName, AdminUser.Password, "GetPost_ValidSlug");

            NewPostDto newPostPayload = new NewPostDto()
            {
                Title = "My new post",
                Content = "This is a test post",
                Tags = ["tag1", "tag2"]
            };

            var response = await _client.PostAsJsonAsync("/api/post", newPostPayload);

            var getResponse = await _client.GetAsync($"/api/post/by-slug/my-new-post");
            string responseBody = await getResponse.Content.ReadAsStringAsync();

            string testResult = expectedStatusCode == getResponse.StatusCode ? "Passed" : "Failed";

            string output = EntryLogHandler.GetEntryString(
                "GetPost",
                "Get post with valid slug",
                $"{(int)expectedStatusCode} {expectedStatusCode}",
                $"{(int)getResponse.StatusCode} {getResponse.StatusCode}",
                $"{responseBody}",
                $"{testResult}"
                );

            WriteTestResults.AppendData(output);

            _output.WriteLine(output);

            Assert.Equal(expectedStatusCode, getResponse.StatusCode);
        }

        [Fact]
        public async Task GetPost_InvalidSlug_Returns404NotFound()
        {
            var expectedStatusCode = HttpStatusCode.NotFound;

            var getResponse = await _client.GetAsync($"/api/post/by-slug/non-existent-post");
            string responseBody = await getResponse.Content.ReadAsStringAsync();

            string testResult = expectedStatusCode == getResponse.StatusCode ? "Passed" : "Failed";

            string output = EntryLogHandler.GetEntryString(
                "GetPost",
                "Get post with invalid slug",
                $"{(int)expectedStatusCode} {expectedStatusCode}",
                $"{(int)getResponse.StatusCode} {getResponse.StatusCode}",
                $"{responseBody}",
                $"{testResult}"
                );

            WriteTestResults.AppendData(output);

            _output.WriteLine(output);

            Assert.Equal(expectedStatusCode, getResponse.StatusCode);
        }
    }
}
