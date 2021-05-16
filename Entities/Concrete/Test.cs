using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Test : DocumentDbEntity
    {
        public string Name { get; set; }
    }
}
