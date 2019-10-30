using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    class SoftDelete : ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
