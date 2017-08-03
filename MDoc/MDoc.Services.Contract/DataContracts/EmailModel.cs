namespace MDoc.Services.Contract.DataContracts
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToAddress { get; set; }
    }
}