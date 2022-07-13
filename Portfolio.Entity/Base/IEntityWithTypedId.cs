using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Base
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
