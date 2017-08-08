namespace MDoc.Services.Contract.DataContracts
{
    public class ChecklistModel
    {
        public byte Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int LoggedUserId { get; set; }
        public bool? IsChecked { get; set; }
    }
}