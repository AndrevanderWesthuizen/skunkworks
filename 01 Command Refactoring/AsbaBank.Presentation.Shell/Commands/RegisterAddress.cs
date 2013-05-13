using System;
using AsbaBank.Domain;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands
{
    public class RegisterAddress : ICommand
    {
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public RegisterAddress(string streetNumber, string street, string postalCode, string city)
        {
            if (String.IsNullOrWhiteSpace(streetNumber))
            {
                throw new ArgumentException(String.Format("Please provide a valid Street Number."));
            }

            if (String.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException(String.Format("Please provide a valid Street."));
            }

            if (String.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException(String.Format("Please provide a valid PostalCode."));
            }

            if (String.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException(String.Format("Please provide a valid City."));
            }

            StreetNumber = streetNumber;
            Street = street;
            PostalCode = postalCode;
            City = city;
        }
        
        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var addressRepository = unitOfWork.GetRepository<Address>();

            try
            {
                var address = new Address(StreetNumber, Street, PostalCode, City);
                addressRepository.Add(address);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Registered address Street Number: {0} Street: {1} Postal Code: {2} City: {3}", address.StreetNumber, address.Street, address.PostalCode, address.City);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
