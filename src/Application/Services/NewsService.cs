using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;
    private readonly UserContext _userContext;

    public NewsService(INewsRepository repository, UserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    public async Task<Result<NewsResponse>> CreateAsync(NewsRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<News>();
        entity.UserId = _userContext.Id;

        var newsId = await _repository.InsertAndGetId(entity);

        cancellationToken.ThrowIfCancellationRequested();

        var news = await _repository.Select(newsId);

        return ResultHelper.Success(news.Adapt<NewsResponse>());
    }

    public async Task<Result<NewsResponse>> UpdateAsync(int id, NewsRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<News>();
        entity.Id = id;
        entity.UserId = _userContext.Id;

        await _repository.Update(entity);

        cancellationToken.ThrowIfCancellationRequested();

        var news = await _repository.Select(id);

        return ResultHelper.Success(news.Adapt<NewsResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _repository.Delete(id);

        return ResultHelper.Success();
    }

    public async Task<Result<IEnumerable<NewsResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var news = await _repository.Select();

        cancellationToken.ThrowIfCancellationRequested();

        var response = news.Adapt<IEnumerable<NewsResponse>>();

        return ResultHelper.Success(response);
    }
}
