namespace PalpiteFC.Api.Application.Interfaces;

public interface IHashService
{
    string EncryptPassword(string password);
}