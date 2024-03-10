using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Application.Responses;
public class EnqueteResponse
{
    public string? Title { get; set; }
    public OptionsResponse? Options { get; set; }
}
