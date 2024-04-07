using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Errors;
using PalpiteFC.Api.Domain.Interfaces;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class PointSeasonsService : IPointSeasonsService
{
    #region Fields

    private readonly IPointSeasonsRepository _repository;

    #endregion

    #region Constructors

    public PointSeasonsService(IPointSeasonsRepository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<PointSeasonsResponse>> CreateAsync(PointSeasonsRequest request, CancellationToken cancellationToken)
    {
        var pointSeasons = await _repository.Select();

        if (CheckIfDatesConflicts(request, pointSeasons))
        {
            return ResultHelper.Failure<PointSeasonsResponse>(PointSeasonsErrors.ConflictDate);
        }

        var id = await _repository.InsertAndGetId(request.Adapt<PointSeasons>());

        var response = request.Adapt<PointSeasonsResponse>();
        response.Id = id;

        return ResultHelper.Success(response);
    }

    public async Task<Result> DeteleAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return ResultHelper.Success();
    }

    public async Task<Result<IEnumerable<PointSeasonsResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var pointSeasons = await _repository.Select();

        return ResultHelper.Success(pointSeasons.Adapt<IEnumerable<PointSeasonsResponse>>());
    }

    public async Task<Result<PointSeasonsResponse>> GetCurrentAsync(CancellationToken cancellationToken)
    {
        var pointSeasons = await _repository.SelectCurrent();

        return ResultHelper.Success(pointSeasons.Adapt<PointSeasonsResponse>());
    }

    public async Task<Result<PointSeasonsResponse>> UpdateAsync(PointSeasonsRequest request, CancellationToken cancellationToken)
    {
        var pointSeasons = await _repository.Select();

        if (CheckIfDatesConflicts(request, pointSeasons))
        {
            return ResultHelper.Failure<PointSeasonsResponse>(PointSeasonsErrors.ConflictDate);
        }

        await _repository.Update(request.Adapt<PointSeasons>());

        return ResultHelper.Success(request.Adapt<PointSeasonsResponse>());
    }

    #endregion

    #region Non-Public Methods

    private static bool CheckIfDatesConflicts(PointSeasonsRequest request, IEnumerable<PointSeasons> pointSeasons) 
        => pointSeasons.Any(w => w.StartDate <= request.StartDate && w.EndDate >= request.StartDate 
                              || w.StartDate <= request.EndDate && w.EndDate >= request.EndDate);

    #endregion
}
