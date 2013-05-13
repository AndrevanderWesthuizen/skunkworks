using System;
using AsbaBank.Domain;
using AsbaBank.Domain.Models;


namespace AsbaBank.Presentation.Shell.Commands
{
    public class RegisterAccount : ICommand
    {
        public int ClientId { get; private set; }

        public RegisterAccount(int clientId)
        {
            if (clientId <= 0)
            {
                throw new ArgumentException("Please provide a valid client Id.");
            }

            ClientId = clientId;
        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var accountRepository = unitOfWork.GetRepository<Account>();

            try
            {
                var account = new RegisterAccount(ClientId);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Registered account {0}", account.ClientId);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
