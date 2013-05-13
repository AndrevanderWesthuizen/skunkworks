using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account
{
    public class AddAccount : ICommand
    {
        private readonly int clientId;

        public AddAccount(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            this.clientId = clientId;
        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var clientRepository = unitOfWork.GetRepository<Client>();
            var accountRepository = unitOfWork.GetRepository<Domain.Models.Account>();

            try
            {
                var client = clientRepository.Get(clientId);
                if (client == null)
                {
                    throw new ArgumentException(string.Format("A Client with ID {0}, does not exist", clientId));
                }

                var account = Domain.Models.Account.OpenAccount(clientId);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Added account [{3}] for client {1} {2} [Client ID: {0}]", client.Id, client.Name, client.Surname , account.AccountNumber);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
