using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg;

public class UserToken : BaseEntity
{
    public UserToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
        Guard();
    }

    public long UserId { get; internal set; }
    public string HashJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }
    public void Guard()
    {
        NullOrEmptyDomainDataException.CheckString(HashJwtToken, nameof(HashJwtToken));
        NullOrEmptyDomainDataException.CheckString(HashRefreshToken, nameof(HashRefreshToken));

        if (TokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Invalid Token Expiredate");

        if (RefreshTokenExpireDate < TokenExpireDate)
            throw new InvalidDomainDataException("Invalid Refresh Expiredate");

    }
}
