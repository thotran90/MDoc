using System;
using System.ComponentModel.DataAnnotations;

namespace MDoc.Services.Contract.DataContracts
{
    public class ChecklistModel
    {
        public byte Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Label { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public int LoggedUserId { get; set; }
        public bool IsChecked { get; set; }
        public bool IsUpdate => Id > 0;
        public DateTime? LastUpdatedDate { get; set; }
        public int? LastUpdatedById { get; set; }
        public string LastUpdatedUsername { get; set; }
    }
}