using System.Collections.Generic;

namespace Portfolio.Api.Extension
{
    public class SmartTableResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalRecord { get; set; }

        public int NumberOfPages { get; set; }
    }
}
