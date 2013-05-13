using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsbaBank.Presentation.Shell.Commands
{
    public class RegisterAccountShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <ClientId>", Key); } }
        public string Key { get { return "OpenAccount"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            return new RegisterClient(args[0], args[1], args[2]);
        }
    }
}
