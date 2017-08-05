using System;
using System.Collections.Generic;

namespace MDoc.Entities
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            DocumentResponsibles = new HashSet<DocumentResponsible>();
        }

        public int ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public bool IsDisabled { get; set; }
        public string Avatar { get; set; }
        public DateTime? RegisterDate { get; set; }
        public virtual ICollection<DocumentResponsible> DocumentResponsibles { get; set; }
        public virtual ICollection<Company> AdministrateCompanies { get; set; }
    }
}