namespace MDoc.Services.Contract.DataContracts
{
    public class ListDocumentArgument
    {
        public ListDocumentArgument()
        {
            Query = "";
            IsAdmin = false;
        }
        public int UserId { get; set; }
        public string Code { get; set; }
        public bool IsAdmin { get; set; }
        public string Query { get; set; }
    }
}