using System.ComponentModel;

namespace PalpiteFC.Api.Application.Enums;

public enum PointType
{
    [Description("ExactScore")] ES,
    [Description("GoalDifference")] GD,
    [Description("MatchWinner")] MW,
    [Description("EarlyBonus")] EB
}