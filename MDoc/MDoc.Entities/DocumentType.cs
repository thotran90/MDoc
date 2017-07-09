namespace MDoc.Entities
{
    public class DocumentType
    {
        public byte DocumentTypeId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
    }
}