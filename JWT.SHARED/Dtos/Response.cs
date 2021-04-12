using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JWT.SHARED.Dtos
{
   public class Response<T> where T:class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDto Error { get; set; }
        [JsonIgnore]
        public bool IsSuccessfull { get; set; }
        public static Response<T> Success(T data,int StatusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = StatusCode,
                IsSuccessfull = true
                
            };
        }
        public static Response<T> Success( int StatusCode)
        {
            return new Response<T>
            {
                Data = default,
                StatusCode = StatusCode,
                IsSuccessfull = true
            };
        }
        public static Response<T> Fail(ErrorDto errorDto,int StatusCode)
        {
            return new Response<T>
            {
                Error = errorDto,
                StatusCode = StatusCode,
                IsSuccessfull = false
            };
        }
        public static Response<T> Fail(string errorMessage, int StatusCode,bool isShow)
        {
            var errorDtos = new ErrorDto(errorMessage, isShow);
            return new Response<T>
            {
                Error = errorDtos,
                StatusCode = StatusCode,
                IsSuccessfull = false
            };
        }

    }
}
