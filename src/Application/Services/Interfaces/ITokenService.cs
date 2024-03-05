using PalpiteApi.Application.Requests;

namespace PalpiteApi.Application.Services.Interfaces;

public interface ITokenService
{
    string Generate(SignInRequest signInInfo);
}
