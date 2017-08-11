namespace MDoc.Services.Contract.DataContracts
{
    public class DocumentChecklistModel
    {
        public int DocumentId { get; set; }
        public byte ChecklistId { get; set; }
        public bool IsChecked { get; set; }
        public int LoggedUserId { get; set; }
    }
}