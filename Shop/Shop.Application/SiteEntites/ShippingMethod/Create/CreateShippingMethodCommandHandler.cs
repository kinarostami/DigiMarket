using Common.Application;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.ShippingMethod.Create;

public class CreateShippingMethodCommandHandler : IBaseCommandHandler<CreateShippingMethodCommand>
{
     readonly IShippingMethodRepository _repository;

    public CreateShippingMethodCommandHandler(IShippingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shipping = new Domain.SiteEntities.ShippingMethod(request.Cost,request.Title);
        _repository.Add(shipping);
        await _repository.Save();
        return OperationResult.Success();

    }
}
