namespace HrManagementSystem.Core.DTO
{
    public class Common<T>
    {

        public Common()
        {
            errors = new List<Errors>();
        }
        public List<T> response { get; set; }
        public object warningMessage { get; set; }
        public List<Errors> errors { get; set; }
        public bool isError { get; set; }
    }
}
