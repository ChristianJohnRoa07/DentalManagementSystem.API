using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.DTO.Responses
{
    public class BaseApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; }
        public string? ErrorMessage { get; set; }

        public BaseApiResponse(T? data) 
        { 
            Success = true;
            Data = data;
        }

        public BaseApiResponse(bool success, T? data, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Data = default;
            
        }
    }
}
