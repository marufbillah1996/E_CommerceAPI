using API.DTOs;
using Core.Entities;
using System.Runtime.CompilerServices;

namespace API.Extentions
{
    public static class AddressMappingExtentions
    {
        public static AddressDTO ToDto(this Address? address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return new AddressDTO
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                PostalCode = address.PostalCode,
                State = address.State,
                Country = address.Country,
            };
        }
        public static Address ToEntity(this AddressDTO addressDTO)
        {
            if (addressDTO == null) throw new ArgumentNullException(nameof(addressDTO));
            return new Address
            {
                Line1 = addressDTO.Line1,
                Line2 = addressDTO.Line2,
                City = addressDTO.City,
                PostalCode = addressDTO.PostalCode,
                State = addressDTO.State,
                Country = addressDTO.Country,
            };
        }
        public static void UpdateFromDTO(this Address address, AddressDTO addressDTO)
        {
            if (addressDTO == null) throw new ArgumentNullException(nameof(addressDTO));
            if (address == null) throw new ArgumentNullException(nameof(address));

            address.Line1 = addressDTO.Line1;
            address.Line2 = addressDTO.Line2;
            address.City = addressDTO.City;
            address.PostalCode = addressDTO.PostalCode;
            address.State = addressDTO.State;
            address.Country = addressDTO.Country;
            
        }
    }
}
