using Microsoft.Extensions.Logging;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.Extensions;
using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Api.Integrations.PagBank.Requests;
using PalpiteFC.Api.Integrations.PagBank.Responses;
using System.Text;
using System.Text.Json;

namespace PalpiteFC.Api.Integrations.PagBank;

public class PagBankProvider : IPagBankProvider
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly ILogger<PagBankProvider> _logger;

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    #endregion

    #region Constructor

    public PagBankProvider(HttpClient httpClient, ILogger<PagBankProvider> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    public async Task<Result<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken)
        => await PostAsync<CreateCustomerRequest, CreateCustomerResponse>("customers", request, cancellationToken);

    public async Task<Result<SubscriptionResponse>> CreateSubscription(SubscriptionRequest request, CancellationToken cancellationToken)
        => await PostAsync<SubscriptionRequest, SubscriptionResponse>("subscriptions", request, cancellationToken);

    #endregion

    #region Non-public Methods

    private async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest request, CancellationToken cancellationToken) where TResponse : new()
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions), Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(requestUri, content, cancellationToken);

            var responseString = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (httpResponse.IsSuccessStatusCode is false)
            {
                _logger.LogWarning("PagBank Api returned a {StatusCode} status code, with content: {Content}",
                                   (int)httpResponse.StatusCode,
                                   responseString);

                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseString) ?? new ErrorResponse();

                return errorResponse.ToResult<TResponse>();
            }

            var successResponse = JsonSerializer.Deserialize<TResponse>(responseString) ?? new TResponse();

            return ResultHelper.Success(successResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred when calling PagBank Api");

            return ResultHelper.Failure<TResponse>(new Message("Exception", "An error occurred when calling PagBank Api"));
        }
    }

    #endregion
}