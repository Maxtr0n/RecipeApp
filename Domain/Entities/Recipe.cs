using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Recipe : EntityBase
    {
        public string Title { get; set; } = default!;

        public IEnumerable<string> Ingredients { get; set; } = Enumerable.Empty<string>();

        public string Description { get; set; } = default!;

        public IEnumerable<string> Images { get; set; } = Enumerable.Empty<string>();

        public string Author { get; set; } = default!;
    }
}
