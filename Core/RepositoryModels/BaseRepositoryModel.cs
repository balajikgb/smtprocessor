using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryModels
{
    public abstract class BaseRepositoryModel
    {
        public string ErrorMsg { get; set; }
        public bool IsError { get; set; }
    }
}
