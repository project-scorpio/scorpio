using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Data
{
    public interface ITestFilter
    {
        bool IsEnable { get; }
    }


    public class SoftDeleteEntity : ISoftDelete
    {
        public SoftDeleteEntity(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public bool IsDeleted { get; set; }
    }
}
