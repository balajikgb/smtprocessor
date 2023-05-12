using System.Collections.Generic;
using PriceUpdateRepository.DataModels;
using Armstrong.Services.Web.UserDefaultMeasuringUnit.RepositoryModels;

namespace PriceUpdate.ModelMappers
{
    public interface IPriceUpdateMapper
    {
        IEnumerable<MeasuringStandardModel> RepositoryToModel(IEnumerable<MeasuringStandardRepositoryModel> repository);
        IEnumerable<MeasuringCategoryModel> RepositoryToModel(IEnumerable<MeasuringCategoryRepositoryModel> repository);

        IEnumerable<DefaultMeasuringUnitModel> RepositoryToModel(
            IEnumerable<DefaultMeasuringUnitRepositoryModel> repository);

        UserDefaultMeasuringInfoModel RepositoryToModel(UserDefaultMeasuringInfoRepositoryModel repository);

        UserDefaultMeasuringInfoRepositoryModel ModelToRepository(
            UserDefaultMeasuringInfoModel userDefaultMeasuringInfoModel);
    }
}
