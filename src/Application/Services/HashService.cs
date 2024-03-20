using System.Security.Cryptography;
using System.Text;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Application.Services;

public class HashService : IHashService
{
    #region Fields

    private readonly HashAlgorithm _hashAlgorithm;

    #endregion

    #region Constructors

    public HashService(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm;
    }

    #endregion

    #region Public Methods

    public string EncryptPassword(string password)
    {
        var encodedValue = Encoding.UTF8.GetBytes(password);
        var encryptedPassword = _hashAlgorithm.ComputeHash(encodedValue);

        var sb = new StringBuilder();
        foreach (var character in encryptedPassword)
        {
            sb.Append(character.ToString("X2"));
        }

        return sb.ToString();
    }

    #endregion
}

