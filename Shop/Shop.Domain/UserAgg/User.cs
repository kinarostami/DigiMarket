using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    public User(string name, string family, string email, string phoneNumeber, string passeword, Gender gender,IUserDomainService service)
    {
        Name = name;
        Family = family;
        Email = email;
        PhoneNumber = phoneNumeber;
        Password = passeword;
        IsAvtive = true;
        Gender = gender;
        AvatarImage = "avatar.png";
    }

    public string Name { get; set; }
    public string Family { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsAvtive { get; set; }
    public Gender Gender { get; set; }
    public string AvatarImage { get; set; }
    public List<UserAddress> Addresses { get; set; }
    public List<UserRole> Roles { get; set; }
    public List<UserToken> Tokens { get; set; }
    public List<Wallet> Wallets { get; set; }

    public void Edit(string name, string family, string email, string phoneNumeber, Gender gender)
    {
        Name = name;
        Family = family;
        Email = email;
        PhoneNumber = phoneNumeber;
        Gender = gender;
    }

    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword,nameof(newPassword));
        Password = newPassword;
    }

    public static User RegisterUser(string phoneNumber, string password, IUserDomainService service)
    {
        return new User("", "", null, phoneNumber, password, Gender.None, service);
    }

    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
            imageName = "avatar.png";

        AvatarImage = imageName;
    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var oldAddresss = Addresses.FirstOrDefault(x => x.UserId == Id);
        if (oldAddresss == null)
            throw new InvalidDomainDataException("Address not found");

        Addresses.Remove(oldAddresss);
    }

    public void EditAddress(UserAddress userAddress, long addressId)
    {
        var address = Addresses.FirstOrDefault(x => x.Id == addressId);
        if (address == null)
            throw new InvalidDomainDataException("Address not found");

        address.Edit(userAddress.Shire, userAddress.City, userAddress.PostalCode, userAddress.PostalAddress, userAddress.PhoneNumber, userAddress.Name, userAddress.Family, userAddress.NationalCode);
    }

    public void SetActiveAddress(long addressId)
    {
        var currentAddress = Addresses.FirstOrDefault(x => x.Id == addressId);
        if (currentAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not found");

        foreach (var address in Addresses)
        {
            address.SetAvtice();
        }
        currentAddress.SetAvtice();
    }

    public void ChangeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRole(List<UserRole> roles)
    {
        roles.ForEach(x => x.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activTokenCount = Tokens.Count(x => x.RefreshTokenExpireDate > DateTime.Now);
        if (activTokenCount == 3)
            throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");
        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }
    public string RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
        if (token == null)
            throw new InvalidDomainDataException("invalid TokenId");

        Tokens.Remove(token);
        return token.HashJwtToken;
    }

    public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

            //if (!string.IsNullOrWhiteSpace(email))
            //    if (email.IsValidEmail() == false)
            //        throw new InvalidDomainDataException(" ایمیل  نامعتبر است");

        if (phoneNumber != PhoneNumber)
            if (userDomainService.PhoneNumberIsExist(phoneNumber))
                throw new InvalidDomainDataException("شماره موبایل تکراری است");

        if (email != Email)
            if (userDomainService.IsEmailExist(email))
                throw new InvalidDomainDataException("ایمیل تکراری است");

    }
}
