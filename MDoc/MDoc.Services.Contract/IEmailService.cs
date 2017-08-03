using System.Collections.Generic;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract
{
    public interface IEmailService
    {
        void SendEmailToUser(EmailModel email);
        void SendEmailToListOfUser(List<EmailModel> emails);
    }
}