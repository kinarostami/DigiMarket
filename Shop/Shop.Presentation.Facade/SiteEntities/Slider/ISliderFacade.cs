using Common.Application;
using Shop.Application.SiteEntities.Slider.Create;
using Shop.Application.SiteEntities.Slider.Edit;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Slider;

public interface ISliderFacade
{
    Task<OperationResult> CreateSlider(CreateSliderCommand command);
    Task<OperationResult> EditSlider(EditSliderCommand command);
    Task<OperationResult> DeleteSlider(long sliderId);

    Task<SliderDto?> GetSliderById(long id);
    Task<List<SliderDto>> GetSliders();
}