using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg;

public class UserAddress : BaseEntity
{
    public UserAddress()
    {
    }
    public long UserId { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
    public bool ActiveAddress { get; set; }

    public UserAddress(string shire, string city, string postalCode, string postalAddress,
        PhoneNumber phoneNumber, string name, string family, string nationalCode)
    {
        Guard(shire, city, postalCode, postalAddress, phoneNumber, name, family, nationalCode);
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
        ActiveAddress = false;
    }

    public void Edit(string shire, string city, string postalCode, string postalAddress,
        PhoneNumber phoneNumber, string name, string family, string nationalCode)
    {
        Guard(shire, city, postalCode, postalAddress, phoneNumber, name, family, nationalCode);
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }

    public void SetAvtice()
    {
        ActiveAddress = true;
    }

    public void SetDeAvtice()
    {
        ActiveAddress = false;
    }

    public void Guard(string shire, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber,
        string name, string family, string nationalCode)
    {
        if (phoneNumber == null)
            throw new InvalidDomainDataException();

        NullOrEmptyDomainDataException.CheckString(shire, nameof(shire));
        NullOrEmptyDomainDataException.CheckString(city, nameof(city));
        NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
        NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalAddress));
        NullOrEmptyDomainDataException.CheckString(name, nameof(name));
        NullOrEmptyDomainDataException.CheckString(family, nameof(family));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

        //if (IranianNationalIdChecker.IsValid(nationalCode) == false)
        //    throw new InvalidDomainDataException("کد ملی نامعتبر است");
    }
}
