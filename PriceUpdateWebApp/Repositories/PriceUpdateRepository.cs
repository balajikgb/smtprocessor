using System.Collections.Generic;
using System.Linq;
using ArasPLMRepository;
using PriceUpdate.ModelMappers;
using PriceUpdateRepository.DataModels;

namespace PriceUpdate.Repositories
{
    public class PriceUpdateRepsitory : IPriceUpdateRepository
    {
        private readonly UserDefaultMeasuringUnitContext _userDefaultMeasuringUnitContext;
        private readonly IPriceUpdateMapper _userDefaultMeasuringUnitMapper;

        public MeasuringCategoryRepository(UserDefaultMeasuringUnitContext uerDefaultMeasuringUnitContext, IPriceUpdateMapper userDefaultMeasuringUnitMapper)
        {
            _userDefaultMeasuringUnitContext = uerDefaultMeasuringUnitContext;
            _userDefaultMeasuringUnitMapper = userDefaultMeasuringUnitMapper;
        }

        public IEnumerable<MeasuringCategoryModel> GetMeasuringCategories()
        {
            var measuringCategories = _userDefaultMeasuringUnitContext.MeasuringCategory.AsEnumerable();

            foreach (var measuringCategory in measuringCategories)
            {
                _userDefaultMeasuringUnitContext.Entry(measuringCategory).State = EntityState.Detached;
            }
            measuringCategories = _userDefaultMeasuringUnitContext.MeasuringCategory.OrderBy(t => t.CategoryId).AsEnumerable();
            var result = _userDefaultMeasuringUnitMapper.RepositoryToModel(measuringCategories);
            return result;
        }
    }
}