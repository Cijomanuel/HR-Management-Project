namespace HrManagementSystem.Core.HttpClients
{
    public class ApiClientConfiguration
    {
        public string BaseApiUri { get; set; }
        public int BaseApiTimeoutSeconds { get; set; } = 30;
        public string AuthenticationType { get; set; }
        public string BaseUserName { get; set; }
        public string BasePassword { get; set; }
    }
}
