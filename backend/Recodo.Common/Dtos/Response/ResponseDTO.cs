namespace Recodo.Common.Dtos.Response
{
    public class ResponseDTO
    {
        public object Data { get; set; } = "";
        public string ExceptionMessage { get; set; } = "";
        public bool IsError { get; set; } = false;
    }
}
