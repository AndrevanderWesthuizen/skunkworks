using System;

namespace AsbaBank.Presentation.Shell.Commands
{
    public class RegisterAddressShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <StreetNumber> <Street> <PostalCode> <City>", Key); } }
        public string Key { get { return "RegisterAddress"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 4)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            return new RegisterAddress(args[0], args[1], args[2], args[3]);
        }
    }
}
