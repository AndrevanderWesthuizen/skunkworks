using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsbaBank.Presentation.Shell.Commands.Account
{
    public class AddAccountShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <ClientID>", Key); } }
        public string Key { get { return "AddAccount"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            int clientId;
            if (!int.TryParse(args[0], out clientId))
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid client id...", args[0]));
            }

            return new AddAccount(clientId);
        }
    }
}
