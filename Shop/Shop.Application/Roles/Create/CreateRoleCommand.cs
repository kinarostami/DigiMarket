using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Domain.RoleAgg.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;

public class CreateRoleCommandHanlder : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHanlder(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permission = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            permission.Add(new RolePermission(x));
        });

        var role = new Role(request.Title);
        _roleRepository.Add(role);
        await _roleRepository.Save();
        return OperationResult.Success();
    }
}
