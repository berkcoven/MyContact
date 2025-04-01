using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Core.Models
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public BaseResponse(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        
        public static BaseResponse<T> SuccessResponse(T data, string message = "Request was successful")
        {
            return new BaseResponse<T>(true, message, data);
        }

        public static BaseResponse<T> ErrorResponse(string message)
        {
            return new BaseResponse<T>(false, message);
        }
    }

}
