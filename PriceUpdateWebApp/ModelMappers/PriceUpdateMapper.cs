using System.Collections.Generic;
using System.Linq;
using PriceUpdateRepository.DataModels;
using Armstrong.Services.Web.UserDefaultMeasuringUnit.RepositoryModels;

namespace PriceUpdate.ModelMappers
{
    public class PriceUpdateMapper : IPriceUpdateMapper
    {
        public IEnumerable<MeasuringStandardModel> RepositoryToModel(IEnumerable<MeasuringStandardRepositoryModel> measuringStandardRepositoryModelList)
        {
            return measuringStandardRepositoryModelList.Select(measuringStandardModel => new MeasuringStandardModel()
                {
                    Id = measuringStandardModel.Id,
                    Standard = measuringStandardModel.Standard
                })
                .ToList();
        }
        
       
    }
}