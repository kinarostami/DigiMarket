using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.RoleAgg;

public class Role : AggregateRoot
{
    public Role()
    {
    }
    public string Title { get; set; }
    public List<RolePermission> Permissions { get; set; }

    public Role(string title, List<RolePermission> permissions)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Permissions = permissions;
    }

    public Role(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Permissions = new List<RolePermission>();
    }

    public void Edit(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
    }

    public void SetPermission(List<RolePermission> permission)
    {
        Permissions = permission;
    }
}
