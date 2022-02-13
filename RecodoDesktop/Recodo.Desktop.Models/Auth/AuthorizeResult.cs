namespace Recodo.Desktop.Models.Auth
{
    public class AuthorizeResult
    {
        public string Code { get; set; }

        //it's temporary state format can override in future
        public string State { get; set; }
    }
}
