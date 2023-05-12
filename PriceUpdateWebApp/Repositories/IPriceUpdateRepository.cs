using System.Collections.Generic;
using Armstrong.Services.Web.UserDefaultMeasuringUnit.Models;

namespace PriceUpdate.Repositories
{
    public interface IPriceUpdateRepository
    {
        IEnumerable<MeasuringCategoryModel> GetMeasuringCategories();
    }
}