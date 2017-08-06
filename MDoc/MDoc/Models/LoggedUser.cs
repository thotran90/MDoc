using System;
using System.Security.Claims;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Models
{
    public class LoggedUser : ClaimsPrincipal
    {
        public LoggedUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string Name => FindFirst(ClaimTypes.Name).Value;
        public string Email => FindFirst(ClaimTypes.Email).Value;
        public string LoginId => FindFirst(ClaimTypes.NameIdentifier).Value;
        public int UserId=> Convert.ToInt32(FindFirst(ClaimTypes.PrimarySid).Value);

        public int? CompanyId
            =>
                string.IsNullOrEmpty(FindFirst("CompanyId").Value)
                    ? (int?)null
                    : Convert.ToInt32(FindFirst("CompanyId").Value);

        public string Avatar => FindFirst("Avatar").Value;
        public bool IsSuperAdmin => bool.Parse(FindFirst("IsSuperAdmin").Value);
        public bool IsCompanyAdmin => bool.Parse(FindFirst("IsCompanyAdmin").Value);
    }
}