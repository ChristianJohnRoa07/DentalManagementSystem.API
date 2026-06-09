using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Responses
{
    public class BaseApiResponse<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? ErrorMessage { get; init; }
        public int StatusCode { get; init; }

        public BaseApiResponse() { }

        public BaseApiResponse(T? data, int statusCode)
        {
            Success = true;
            StatusCode = statusCode;
            Data = data;
        }
    }
}
