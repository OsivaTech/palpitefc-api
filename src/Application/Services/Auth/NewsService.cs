using Mapster;
using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services.Auth;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;
    private readonly IUserRepository _userRepository;

    public NewsService(INewsRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<NewsResponse> CreateOrUpdateAsync(NewsRequest request)
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

        return news.Adapt<NewsResponse>();
    }

    public async Task<NewsResponse> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return new() { Id = id };
    }

    public async Task<IEnumerable<NewsResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var news = await _repository.Select();

        var users = await _userRepository.Select();

        var response = new List<NewsResponse>();

        foreach (var item in news)
        {
            response.Add((item, users.First(w => w.Id == item.UserId)).Adapt<NewsResponse>());
        }

        return response;
    }
}
