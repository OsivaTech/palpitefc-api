﻿using Mapster;
using Microsoft.Extensions.Caching.Memory;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Interfaces;
using System.Text.Json;

namespace PalpiteApi.Application.Services;

public class GamesService : IGamesService
{
    #region Fields


    private readonly IApiFootballProvider _apiFootballProvider;
    private readonly IMemoryCache _cache;

    #endregion

    #region Contructors

    public GamesService(IApiFootballProvider apiFootballProvider, IMemoryCache cache)
    {
        _apiFootballProvider = apiFootballProvider;
        _cache = cache;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<GameResponse>> GetAsync(CancellationToken cancellationToken)
    {
        return JsonSerializer.Deserialize<IEnumerable<GameResponse>>("[{\"Id\":1181746,\"Name\":\"\",\"Start\":\"2024-03-09T16:30:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1062,\"GameId\":1181746,\"Name\":\"Atletico-MG\",\"Image\":\"https://media.api-sports.io/football/teams/1062.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":125,\"GameId\":1181746,\"Name\":\"America Mineiro\",\"Image\":\"https://media.api-sports.io/football/teams/125.png\"}},{\"Id\":1181748,\"Name\":\"\",\"Start\":\"2024-03-09T17:00:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1196,\"GameId\":1181748,\"Name\":\"Uberlandia\",\"Image\":\"https://media.api-sports.io/football/teams/1196.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13975,\"GameId\":1181748,\"Name\":\"Athletic Club\",\"Image\":\"https://media.api-sports.io/football/teams/13975.png\"}},{\"Id\":1146820,\"Name\":\"\",\"Start\":\"2024-03-09T18:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":121,\"GameId\":1146820,\"Name\":\"Palmeiras\",\"Image\":\"https://media.api-sports.io/football/teams/121.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2618,\"GameId\":1146820,\"Name\":\"Botafogo SP\",\"Image\":\"https://media.api-sports.io/football/teams/2618.png\"}},{\"Id\":1146822,\"Name\":\"\",\"Start\":\"2024-03-09T18:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":128,\"GameId\":1146822,\"Name\":\"Santos\",\"Image\":\"https://media.api-sports.io/football/teams/128.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1201,\"GameId\":1146822,\"Name\":\"Inter De Limeira\",\"Image\":\"https://media.api-sports.io/football/teams/1201.png\"}},{\"Id\":1180902,\"Name\":\"\",\"Start\":\"2024-03-09T18:30:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7835,\"GameId\":1180902,\"Name\":\"Portuguesa RJ\",\"Image\":\"https://media.api-sports.io/football/teams/7835.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2206,\"GameId\":1180902,\"Name\":\"Boavista SC\",\"Image\":\"https://media.api-sports.io/football/teams/2206.png\"}},{\"Id\":1180896,\"Name\":\"\",\"Start\":\"2024-03-09T21:00:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":124,\"GameId\":1180896,\"Name\":\"Fluminense\",\"Image\":\"https://media.api-sports.io/football/teams/124.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":127,\"GameId\":1180896,\"Name\":\"Flamengo\",\"Image\":\"https://media.api-sports.io/football/teams/127.png\"}},{\"Id\":1181750,\"Name\":\"\",\"Start\":\"2024-03-10T11:00:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":21165,\"GameId\":1181750,\"Name\":\"Itabirito\",\"Image\":\"https://media.api-sports.io/football/teams/21165.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13084,\"GameId\":1181750,\"Name\":\"Pouso Alegre\",\"Image\":\"https://media.api-sports.io/football/teams/13084.png\"}},{\"Id\":1146816,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":138,\"GameId\":1146816,\"Name\":\"Guarani Campinas\",\"Image\":\"https://media.api-sports.io/football/teams/138.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":794,\"GameId\":1146816,\"Name\":\"RB Bragantino\",\"Image\":\"https://media.api-sports.io/football/teams/794.png\"}},{\"Id\":1146817,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7779,\"GameId\":1146817,\"Name\":\"Ituano\",\"Image\":\"https://media.api-sports.io/football/teams/7779.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":126,\"GameId\":1146817,\"Name\":\"Sao Paulo\",\"Image\":\"https://media.api-sports.io/football/teams/126.png\"}},{\"Id\":1146818,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7848,\"GameId\":1146818,\"Name\":\"Mirassol\",\"Image\":\"https://media.api-sports.io/football/teams/7848.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7865,\"GameId\":1146818,\"Name\":\"S\\u00E3o Bernardo\",\"Image\":\"https://media.api-sports.io/football/teams/7865.png\"}},{\"Id\":1146819,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7834,\"GameId\":1146819,\"Name\":\"Novorizontino\",\"Image\":\"https://media.api-sports.io/football/teams/7834.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1214,\"GameId\":1146819,\"Name\":\"Portuguesa\",\"Image\":\"https://media.api-sports.io/football/teams/1214.png\"}},{\"Id\":1146821,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":10003,\"GameId\":1146821,\"Name\":\"Santo Andr\\u00E9\",\"Image\":\"https://media.api-sports.io/football/teams/10003.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":139,\"GameId\":1146821,\"Name\":\"Ponte Preta\",\"Image\":\"https://media.api-sports.io/football/teams/139.png\"}},{\"Id\":1146823,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":10018,\"GameId\":1146823,\"Name\":\"\\u00C1gua Santa\",\"Image\":\"https://media.api-sports.io/football/teams/10018.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":131,\"GameId\":1146823,\"Name\":\"Corinthians\",\"Image\":\"https://media.api-sports.io/football/teams/131.png\"}},{\"Id\":1180900,\"Name\":\"\",\"Start\":\"2024-03-10T16:00:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13115,\"GameId\":1180900,\"Name\":\"Sampaio Corr\\u00EAa RJ\",\"Image\":\"https://media.api-sports.io/football/teams/13115.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":120,\"GameId\":1180900,\"Name\":\"Botafogo\",\"Image\":\"https://media.api-sports.io/football/teams/120.png\"}},{\"Id\":1180898,\"Name\":\"\",\"Start\":\"2024-03-10T18:30:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":133,\"GameId\":1180898,\"Name\":\"Vasco DA Gama\",\"Image\":\"https://media.api-sports.io/football/teams/133.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7782,\"GameId\":1180898,\"Name\":\"Nova Igua\\u00E7u\",\"Image\":\"https://media.api-sports.io/football/teams/7782.png\"}},{\"Id\":1181747,\"Name\":\"\",\"Start\":\"2024-03-10T19:30:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2227,\"GameId\":1181747,\"Name\":\"Tombense\",\"Image\":\"https://media.api-sports.io/football/teams/2227.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":135,\"GameId\":1181747,\"Name\":\"Cruzeiro\",\"Image\":\"https://media.api-sports.io/football/teams/135.png\"}},{\"Id\":1178188,\"Name\":\"\",\"Start\":\"2024-03-12T19:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":146,\"GameId\":1178188,\"Name\":\"CRB\",\"Image\":\"https://media.api-sports.io/football/teams/146.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13975,\"GameId\":1178188,\"Name\":\"Athletic Club\",\"Image\":\"https://media.api-sports.io/football/teams/13975.png\"}},{\"Id\":1181752,\"Name\":\"\",\"Start\":\"2024-03-12T19:30:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":12277,\"GameId\":1181752,\"Name\":\"Ipatinga\",\"Image\":\"https://media.api-sports.io/football/teams/12277.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13080,\"GameId\":1181752,\"Name\":\"Democrata GV\",\"Image\":\"https://media.api-sports.io/football/teams/13080.png\"}},{\"Id\":1175675,\"Name\":\"\",\"Start\":\"2024-03-12T20:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7835,\"GameId\":1175675,\"Name\":\"Portuguesa RJ\",\"Image\":\"https://media.api-sports.io/football/teams/7835.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1193,\"GameId\":1175675,\"Name\":\"Cuiaba\",\"Image\":\"https://media.api-sports.io/football/teams/1193.png\"}},{\"Id\":1178192,\"Name\":\"\",\"Start\":\"2024-03-12T21:30:00-03:00\",\"ChampionshipId\":13,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2318,\"GameId\":1178192,\"Name\":\"Palestino\",\"Image\":\"https://media.api-sports.io/football/teams/2318.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1175,\"GameId\":1178192,\"Name\":\"Nacional Asuncion\",\"Image\":\"https://media.api-sports.io/football/teams/1175.png\"}},{\"Id\":1180008,\"Name\":\"\",\"Start\":\"2024-03-12T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":754,\"GameId\":1180008,\"Name\":\"ABC\",\"Image\":\"https://media.api-sports.io/football/teams/754.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1211,\"GameId\":1180008,\"Name\":\"Brusque\",\"Image\":\"https://media.api-sports.io/football/teams/1211.png\"}},{\"Id\":1180009,\"Name\":\"\",\"Start\":\"2024-03-12T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7770,\"GameId\":1180009,\"Name\":\"Caxias\",\"Image\":\"https://media.api-sports.io/football/teams/7770.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":118,\"GameId\":1180009,\"Name\":\"Bahia\",\"Image\":\"https://media.api-sports.io/football/teams/118.png\"}},{\"Id\":1175674,\"Name\":\"\",\"Start\":\"2024-03-13T19:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1221,\"GameId\":1175674,\"Name\":\"Ypiranga-RS\",\"Image\":\"https://media.api-sports.io/football/teams/1221.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":12946,\"GameId\":1175674,\"Name\":\"Porto Velho\",\"Image\":\"https://media.api-sports.io/football/teams/12946.png\"}},{\"Id\":1180010,\"Name\":\"\",\"Start\":\"2024-03-13T19:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":123,\"GameId\":1180010,\"Name\":\"Sport Recife\",\"Image\":\"https://media.api-sports.io/football/teams/123.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1210,\"GameId\":1180010,\"Name\":\"Murici Fc\",\"Image\":\"https://media.api-sports.io/football/teams/1210.png\"}},{\"Id\":1180011,\"Name\":\"\",\"Start\":\"2024-03-13T20:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7782,\"GameId\":1180011,\"Name\":\"Nova Igua\\u00E7u\",\"Image\":\"https://media.api-sports.io/football/teams/7782.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":119,\"GameId\":1180011,\"Name\":\"Internacional\",\"Image\":\"https://media.api-sports.io/football/teams/119.png\"}},{\"Id\":1178190,\"Name\":\"\",\"Start\":\"2024-03-13T21:30:00-03:00\",\"ChampionshipId\":13,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":794,\"GameId\":1178190,\"Name\":\"RB Bragantino\",\"Image\":\"https://media.api-sports.io/football/teams/794.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":120,\"GameId\":1178190,\"Name\":\"Botafogo\",\"Image\":\"https://media.api-sports.io/football/teams/120.png\"}},{\"Id\":1180020,\"Name\":\"\",\"Start\":\"2024-03-13T21:30:00-03:00\",\"ChampionshipId\":13,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2315,\"GameId\":1180020,\"Name\":\"Colo Colo\",\"Image\":\"https://media.api-sports.io/football/teams/2315.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1187,\"GameId\":1180020,\"Name\":\"Sportivo Trinidense\",\"Image\":\"https://media.api-sports.io/football/teams/1187.png\"}},{\"Id\":1175676,\"Name\":\"\",\"Start\":\"2024-03-13T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2233,\"GameId\":1175676,\"Name\":\"America-RN\",\"Image\":\"https://media.api-sports.io/football/teams/2233.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":10004,\"GameId\":1175676,\"Name\":\"S\\u00E3o Luiz\",\"Image\":\"https://media.api-sports.io/football/teams/10004.png\"}},{\"Id\":1180007,\"Name\":\"\",\"Start\":\"2024-03-13T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":152,\"GameId\":1180007,\"Name\":\"Juventude\",\"Image\":\"https://media.api-sports.io/football/teams/152.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":149,\"GameId\":1180007,\"Name\":\"Paysandu\",\"Image\":\"https://media.api-sports.io/football/teams/149.png\"}},{\"Id\":1180012,\"Name\":\"\",\"Start\":\"2024-03-13T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":155,\"GameId\":1180012,\"Name\":\"Sampaio Correa\",\"Image\":\"https://media.api-sports.io/football/teams/155.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1195,\"GameId\":1180012,\"Name\":\"Ferroviario\",\"Image\":\"https://media.api-sports.io/football/teams/1195.png\"}},{\"Id\":1180013,\"Name\":\"\",\"Start\":\"2024-03-14T19:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2208,\"GameId\":1180013,\"Name\":\"Brasiliense\",\"Image\":\"https://media.api-sports.io/football/teams/2208.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":140,\"GameId\":1180013,\"Name\":\"Criciuma\",\"Image\":\"https://media.api-sports.io/football/teams/140.png\"}},{\"Id\":1180014,\"Name\":\"\",\"Start\":\"2024-03-14T20:00:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7865,\"GameId\":1180014,\"Name\":\"S\\u00E3o Bernardo\",\"Image\":\"https://media.api-sports.io/football/teams/7865.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":131,\"GameId\":1180014,\"Name\":\"Corinthians\",\"Image\":\"https://media.api-sports.io/football/teams/131.png\"}},{\"Id\":1175678,\"Name\":\"\",\"Start\":\"2024-03-14T20:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7879,\"GameId\":1175678,\"Name\":\"\\u00C1guia de Marab\\u00E1\",\"Image\":\"https://media.api-sports.io/football/teams/7879.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13134,\"GameId\":1175678,\"Name\":\"Capital\",\"Image\":\"https://media.api-sports.io/football/teams/13134.png\"}},{\"Id\":1178194,\"Name\":\"\",\"Start\":\"2024-03-14T21:30:00-03:00\",\"ChampionshipId\":13,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2356,\"GameId\":1178194,\"Name\":\"Club Nacional\",\"Image\":\"https://media.api-sports.io/football/teams/2356.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":3700,\"GameId\":1178194,\"Name\":\"Always Ready\",\"Image\":\"https://media.api-sports.io/football/teams/3700.png\"}},{\"Id\":1175673,\"Name\":\"\",\"Start\":\"2024-03-14T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2618,\"GameId\":1175673,\"Name\":\"Botafogo SP\",\"Image\":\"https://media.api-sports.io/football/teams/2618.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7852,\"GameId\":1175673,\"Name\":\"An\\u00E1polis\",\"Image\":\"https://media.api-sports.io/football/teams/7852.png\"}},{\"Id\":1180015,\"Name\":\"\",\"Start\":\"2024-03-14T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7833,\"GameId\":1180015,\"Name\":\"Maring\\u00E1\",\"Image\":\"https://media.api-sports.io/football/teams/7833.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":10862,\"GameId\":1180015,\"Name\":\"Amazonas\",\"Image\":\"https://media.api-sports.io/football/teams/10862.png\"}},{\"Id\":1181769,\"Name\":\"\",\"Start\":\"2024-03-14T21:30:00-03:00\",\"ChampionshipId\":73,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":154,\"GameId\":1181769,\"Name\":\"Fortaleza EC\",\"Image\":\"https://media.api-sports.io/football/teams/154.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13098,\"GameId\":1181769,\"Name\":\"Retr\\u00F4\",\"Image\":\"https://media.api-sports.io/football/teams/13098.png\"}},{\"Id\":1180899,\"Name\":\"\",\"Start\":\"2024-03-16T18:00:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7782,\"GameId\":1180899,\"Name\":\"Nova Igua\\u00E7u\",\"Image\":\"https://media.api-sports.io/football/teams/7782.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":133,\"GameId\":1180899,\"Name\":\"Vasco DA Gama\",\"Image\":\"https://media.api-sports.io/football/teams/133.png\"}},{\"Id\":1180901,\"Name\":\"\",\"Start\":\"2024-03-16T18:00:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":120,\"GameId\":1180901,\"Name\":\"Botafogo\",\"Image\":\"https://media.api-sports.io/football/teams/120.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13115,\"GameId\":1180901,\"Name\":\"Sampaio Corr\\u00EAa RJ\",\"Image\":\"https://media.api-sports.io/football/teams/13115.png\"}},{\"Id\":1181751,\"Name\":\"\",\"Start\":\"2024-03-16T18:00:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13084,\"GameId\":1181751,\"Name\":\"Pouso Alegre\",\"Image\":\"https://media.api-sports.io/football/teams/13084.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":21165,\"GameId\":1181751,\"Name\":\"Itabirito\",\"Image\":\"https://media.api-sports.io/football/teams/21165.png\"}},{\"Id\":1180903,\"Name\":\"\",\"Start\":\"2024-03-16T18:30:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":2206,\"GameId\":1180903,\"Name\":\"Boavista SC\",\"Image\":\"https://media.api-sports.io/football/teams/2206.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7835,\"GameId\":1180903,\"Name\":\"Portuguesa RJ\",\"Image\":\"https://media.api-sports.io/football/teams/7835.png\"}},{\"Id\":1181749,\"Name\":\"\",\"Start\":\"2024-03-16T18:30:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13975,\"GameId\":1181749,\"Name\":\"Athletic Club\",\"Image\":\"https://media.api-sports.io/football/teams/13975.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1196,\"GameId\":1181749,\"Name\":\"Uberlandia\",\"Image\":\"https://media.api-sports.io/football/teams/1196.png\"}},{\"Id\":1181854,\"Name\":\"\",\"Start\":\"2024-03-16T21:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":128,\"GameId\":1181854,\"Name\":\"Santos\",\"Image\":\"https://media.api-sports.io/football/teams/128.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1214,\"GameId\":1181854,\"Name\":\"Portuguesa\",\"Image\":\"https://media.api-sports.io/football/teams/1214.png\"}},{\"Id\":1181855,\"Name\":\"\",\"Start\":\"2024-03-16T21:00:00-03:00\",\"ChampionshipId\":475,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":794,\"GameId\":1181855,\"Name\":\"RB Bragantino\",\"Image\":\"https://media.api-sports.io/football/teams/794.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":1201,\"GameId\":1181855,\"Name\":\"Inter De Limeira\",\"Image\":\"https://media.api-sports.io/football/teams/1201.png\"}},{\"Id\":1180897,\"Name\":\"\",\"Start\":\"2024-03-16T21:00:00-03:00\",\"ChampionshipId\":624,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":127,\"GameId\":1180897,\"Name\":\"Flamengo\",\"Image\":\"https://media.api-sports.io/football/teams/127.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":124,\"GameId\":1180897,\"Name\":\"Fluminense\",\"Image\":\"https://media.api-sports.io/football/teams/124.png\"}},{\"Id\":1181753,\"Name\":\"\",\"Start\":\"2024-03-18T20:30:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":13080,\"GameId\":1181753,\"Name\":\"Democrata GV\",\"Image\":\"https://media.api-sports.io/football/teams/13080.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7824,\"GameId\":1181753,\"Name\":\"CAP\",\"Image\":\"https://media.api-sports.io/football/teams/7824.png\"}},{\"Id\":1181754,\"Name\":\"\",\"Start\":\"2024-03-21T20:00:00-03:00\",\"ChampionshipId\":629,\"Finished\":false,\"FirstTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":7824,\"GameId\":1181754,\"Name\":\"CAP\",\"Image\":\"https://media.api-sports.io/football/teams/7824.png\"},\"SecondTeam\":{\"Id\":0,\"Gol\":0,\"TeamId\":12277,\"GameId\":1181754,\"Name\":\"Ipatinga\",\"Image\":\"https://media.api-sports.io/football/teams/12277.png\"}}]")!;

        string[] champsIds = ["11", "13", "73", "475", "624", "629"];

        var games = Enumerable.Empty<GameResponse>();

        foreach (var champId in champsIds)
        {
            var cachedGames = await _cache.GetOrCreate(champId, async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1);

                return await _apiFootballProvider.GetMatchesByLeagueId(champId,
                                                                       DateTime.UtcNow.ToString("yyyy-MM-dd"),
                                                                       DateTime.UtcNow.AddDays(14).ToString("yyyy-MM-dd"));
            })!;

            var adaptedGame = cachedGames.Adapt<IEnumerable<GameResponse>>().ToArray();

            for (int i = 0; i < adaptedGame.Length; i++)
            {
                var item = adaptedGame.ElementAt(i);

                adaptedGame[i].FirstTeam.GameId = item.Id;
                adaptedGame[i].SecondTeam.GameId = item.Id;
                adaptedGame[i].FirstTeam.Gol = cachedGames.First(w => w.Fixture.Id.Value == item.Id).Goals.Home.GetValueOrDefault(0);
                adaptedGame[i].SecondTeam.Gol = cachedGames.First(w => w.Fixture.Id.Value == item.Id).Goals.Away.GetValueOrDefault(0);
            }

            games = games.Concat(adaptedGame);
        }

        var response = games.Adapt<IEnumerable<GameResponse>>();

        return response.OrderBy(x => x.Start);
    }

    #endregion
}
