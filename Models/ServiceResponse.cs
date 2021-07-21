namespace dotnet5_WebAPI.Models
{
    // Very handy in terms of big applications.
    // Response from server to diagnose data, suucees or failure and response message
    // Wrapping everything in the service interface 
    // T is generic for type. Type is going to be the object that will be passed inside <>
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string message { get; set; } = null;
    }
}