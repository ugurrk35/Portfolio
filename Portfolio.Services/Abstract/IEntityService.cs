using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Abstract
{
    public interface IEntityService
    {
        string ToSafeSlug(string slug, long entityId, string entityTypeId);

        PEntity Get(long entityId, string entityTypeId);

        void Add(string name, string slug, long entityId, string entityTypeId);

        void Update(string newName, string newSlug, long entityId, string entityTypeId);

        Task Remove(long entityId, string entityTypeId);
    }
}
