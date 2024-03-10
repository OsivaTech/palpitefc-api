using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Application.Services.Interfaces;
public interface IOptionsService
{
    Task<Result<VoteResponse>> SendOption(OptionsRequest req, CancellationToken cancellationToken);
}
