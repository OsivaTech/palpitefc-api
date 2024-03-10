using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Application.Requests;
public class OptionsRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Count { get; set; }
}
