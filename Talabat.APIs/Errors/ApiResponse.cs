
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Messsage { get; set; }

        public ApiResponse(int statusCode, string? messsage=null) 
        {
            StatusCode = statusCode;
            Messsage = messsage?? GetDefaultMeesageForStatusCode(statusCode);
        }

        private string? GetDefaultMeesageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request",
                401 => "Authorized ,you are not",
                404 => "Resource Not Found",
                500=>  "Server Error",
                _  =>  null
            };  
        }
    }
}
