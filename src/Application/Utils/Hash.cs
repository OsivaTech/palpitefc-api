using System.Security.Cryptography;
using System.Text;

namespace PalpiteApi.Application.Utils;

public class Hash
{
    #region Fields

    private readonly HashAlgorithm _hashAlgorithm;

    #endregion

    #region Constructors

    public Hash(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm;
    }

    #endregion

    #region Public Methods

    public string EncryptPassword(string senha)
    {
        var encodedValue = Encoding.UTF8.GetBytes(senha);
        var encryptedPassword = _hashAlgorithm.ComputeHash(encodedValue);

        var sb = new StringBuilder();
        foreach (var caracter in encryptedPassword)
        {
            sb.Append(caracter.ToString("X2"));
        }

        return sb.ToString();
    }

    #endregion
}

