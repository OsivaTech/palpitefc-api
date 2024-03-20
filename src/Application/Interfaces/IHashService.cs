namespace PalpiteApi.Application.Interfaces;

public interface IHashService
{
    string EncryptPassword(string password);
}