namespace MDoc.Extensions.Options
{
    public class ModalOption
    {
        public ModalOption()
        {
            TargetId = "#modal";
            ButtonStyle = "btn btn-default";
            Show = true;
            BackDrop = true;
        }

        public string TargetId { get; set; }
        public string ButtonStyle { get; set; }
        public bool Show { get; set; }
        public bool BackDrop { get; set; }
    }
}