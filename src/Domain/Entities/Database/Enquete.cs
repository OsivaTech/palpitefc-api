using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Domain.Entities.Database;
public class Enquete
{
    public string? Title { get; set; }
    public IEnumerable<Options>? Options { get; set; }
}
