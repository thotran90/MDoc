using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MDoc.Services.Contract.DataContracts.User;

namespace MDoc.Services.Contract.DataContracts
{
    public class DocumentModel
    {
        public DocumentModel()
        {
            Customer = new CustomerModel();
        }

        public int DocumentId { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Document Type")]
        [Required]
        public string DocumentTypeId { get; set; }
        public byte? DocumentStatusId { get; set; }
        public string Code { get; set; }
        [Display(Name = "Reference Country")]
        [Required]
        public int? ReferenceCountryId { get; set; }
        [Display(Name = "Reference School")]
        public int? ReferenceSchoolId { get; set; }
        [Display(Name = "Reference Program")]
        public int? ReferenceProgramId { get; set; }
        [Display(Name = "Final School")]
        public int? FinalSchoolId { get; set; }
        [Display(Name = "Final Program")]
        public int? FinalProgramId { get; set; }
        public int LoggedUserId { get; set; }
        public CustomerModel Customer { get; set; }
        public string DocumentType { get; set; }
        public string DocumentStatus { get; set; }
        public string ReferenceCountry { get; set; }
        public string ReferenceSchool { get; set; }
        [Display(Name = "Country")]
        public string FinalCountry { get; set; }
        [Display(Name = "School")]
        public string FinalSchool { get; set; }
        public bool IsUpdate => DocumentId > 0;
        public string Country { get; set; }
        [Display(Name = "Main Responsible User(s)")]
        public string MainResponsibleIds { get; set; }
        [Display(Name = "Sub Responsible User(s)")]
        public string SubResponsibleIds { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool CanEdit { get; set; }
        [Display(Name = "Is Need Contract?")]
        public bool IsNeedContract { get; set; }
        [Display(Name = "Created Contract?")]
        public bool IsCreatedContract { get; set; }
        public IEnumerable<UserModel> ResponsibleUsers { get; set; }

        public string FormatedResponsebileUsers
            => ResponsibleUsers.Any() ? string.Join(", ", ResponsibleUsers.Select(e => e.UserName).ToArray()) : "";
    }
}