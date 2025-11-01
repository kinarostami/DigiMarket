using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Edit;

public record EditRoleCommand(long Id,string Title, List<Permission> Permissions) : IBaseCommand;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public EditRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetTracking(request.Id);
        if (role == null)
            return OperationResult.NotFound();

        role.Edit(request.Title);

        var permission = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            permission.Add(new RolePermission(x));
        });
        role.SetPermission(permission);

        await _roleRepository.Save();
        return OperationResult.Success();

    }
}
