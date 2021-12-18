namespace PhoneBook.API.Dto
{
    public class ReturnDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
