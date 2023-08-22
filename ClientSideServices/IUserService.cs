using ITValetFrontEnd.HelperClasses;
using ITValetFrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ITValetFrontEnd.ClientSideServices
{
    public interface IUserService
    {
        Task<ResponseDto> UpdateProfile(PostUpdateUserDto user, string jwtToken);
        Task<ResponseDto> AddRequestService(PostAddRequestServices postAddRequestServices, string jwtToken);
        Task<ResponseDto> GetRequestServiceById(string RequestServiceId, string jwtToken);
        Task<ResponseDto> GetOrderById(string orderId, string jwtToken);
        Task<ResponseDto> GetUserSkillsByUserId(int UserId, string jwtToken);
        Task<ResponseDto> ChargeStripePayment(CheckOutDTO Data, string jwtToken);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = ProjectVariables.BaseUrl;
        }

        public async Task<ResponseDto> UpdateProfile(PostUpdateUserDto user, string jwtToken)
        {
            string apiEndpoint = "User/PostUpdateUser";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.PutAsJsonAsync(_baseUrl + apiEndpoint, user);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }

        #region RequestedService
        public async Task<ResponseDto> AddRequestService(PostAddRequestServices postAddRequestServices, string jwtToken)
        {
            string apiEndpoint = "Customer/PostAddRequestService";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + apiEndpoint, postAddRequestServices);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }

        public async Task<ResponseDto> GetRequestServiceById(string RequestServiceId, string jwtToken)
        {
            string apiEndpoint = "User/GetRequestServiceById?requestServiceId=" + RequestServiceId;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.GetAsync(_baseUrl + apiEndpoint);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }

        public async Task<ResponseDto> GetOrderById(string orderId, string jwtToken)
        {
            string apiEndpoint = "User/GetOrderById?orderId=" + orderId;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.GetAsync(_baseUrl + apiEndpoint);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }

        public async Task<ResponseDto> GetUserSkillsByUserId(int UserId,string jwtToken)
        {
            string apiEndpoint = "User/GetUserSkillByUserId?userId=" + UserId;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.GetAsync(_baseUrl + apiEndpoint);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }
        #endregion

        #region StripePayment
        

        public async Task<ResponseDto> ChargeStripePayment(CheckOutDTO Data, string jwtToken)
        {
            string apiEndpoint = "StripePayment/CreateStripeCharge";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + apiEndpoint, Data);
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return responseDto;
        }


        #endregion
    }
}
