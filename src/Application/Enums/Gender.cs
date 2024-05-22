using System.ComponentModel;

namespace PalpiteFC.Api.Application.Enums;

public enum Gender
{
    [Description("Male")] M = 1,
    [Description("Female")] F = 2,
    [Description("Others")] O = 3
}