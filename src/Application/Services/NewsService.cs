using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;
    private readonly IUserRepository _userRepository;

    public NewsService(INewsRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<Result<NewsResponse>> CreateOrUpdateAsync(NewsRequest request)
    {
        var id = request.News!.Id;

        if (id > 0)
        {
            await _repository.Update(request.News.Adapt<News>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.News.Adapt<News>());
        }

        var news = await _repository.Select(id);

        return ResultHelper.Success(news.Adapt<NewsResponse>());
    }

    public async Task<Result<NewsResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return ResultHelper.Success<NewsResponse>(new() { Id = id });
    }

    public async Task<Result<IEnumerable<NewsResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var news = await _repository.Select();

        var users = await _userRepository.Select();

        var response = new List<NewsResponse>();

        foreach (var item in news)
        {
            response.Add((item, users.First(w => w.Id == item.UserId)).Adapt<NewsResponse>());
        }

        return ResultHelper.Success<IEnumerable<NewsResponse>>(response);
    }
}
