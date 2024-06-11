using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Interfaces;
public interface IWaitingListService
{
    Task<Result<IEnumerable<WaitingListResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> InsertAsync(WaitingListRequest request, CancellationToken cancellationToken);
}
