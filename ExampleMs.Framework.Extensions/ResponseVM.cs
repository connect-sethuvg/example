namespace ExampleMs.Framework.Extensions
{
    public class ResponseVM
    {

        public string? ResponseCode { get; set; }

        public string? _ResponseMessage;

        public ResponseVM( string successCode)
        {
            ResponseCode = successCode;
        }

        public ResponseVM(string successCode, string responseMessage)
        {
            ResponseCode = successCode;
            ResponseMessage = responseMessage;
        }

        public string? ResponseMessage {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponseCode))
                {
                    return string.Empty;
                }
                return string.IsNullOrWhiteSpace(ResponseCodes.ResourceManager.GetString(ResponseCode)) 
                    ? _ResponseMessage : ResponseCodes.ResourceManager.GetString(ResponseCode);
            }

            set => _ResponseMessage = value;
        }

    }
}
