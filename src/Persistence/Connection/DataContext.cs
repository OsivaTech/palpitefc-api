using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using PalpiteFC.Api.Domain.Settings;
using System.Data;
using System.Text;

namespace PalpiteFC.Api.Persistence.Connection;

public class DataContext
{
    #region Fields

    private readonly DbSettings _dbSettings;

    #endregion

    #region Constructors

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    #endregion

    #region Public Methods

    public IDbConnection CreateConnection() => new MySqlConnection(_dbSettings.ToConnectionString());

    public async Task Init()
    {
        await InitDatabase();
        await InitTables();
    }

    #endregion

    #region Non-Public Methods

    private async Task InitDatabase()
    {
        // create database if it doesn't exist
        using var connection = CreateConnection();

        var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";

        await connection.ExecuteAsync(sql);
    }

    private async Task InitTables()
    {
        // create tables if they don't exist
        using var connection = CreateConnection();

        var query = new StringBuilder();

        InitChampionshipTeamPoints();
        InitChampionships();
        InitConfig();
        InitGames();
        InitNews();
        InitOptions();
        InitPalpitations();
        InitTeams();
        InitTeamsGame();
        InitUserVotes();
        InitUsers();
        InitVotes();
        InitUserPoints();

        await connection.ExecuteAsync(query.ToString());

        void InitChampionshipTeamPoints()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `championshipTeamPoints` (
                `id` int NOT NULL AUTO_INCREMENT,
                `position` int NOT NULL,
                `points` int NOT NULL,
                `teamId` int NOT NULL,
                `championshipsId` int NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `championshipTeamPoints_teamId_idx` (`teamId`),
                KEY `championshipTeamPoints_championshipsId_idx` (`championshipsId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitChampionships()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `championships` (
                `id` int NOT NULL AUTO_INCREMENT,
                `name` varchar(191) NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }


        void InitConfig()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `config` (
                `id` int NOT NULL AUTO_INCREMENT,
                `name` varchar(191) NOT NULL,
                `value` varchar(191),
                PRIMARY KEY (`id`),
                UNIQUE KEY `config_name_key` (`name`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitGames()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `games` (
                `id` int NOT NULL AUTO_INCREMENT,
                `name` varchar(191) NOT NULL,
                `championshipId` int NOT NULL,
                `start` datetime(3),
                `finished` tinyint(1) DEFAULT '0',
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `games_championshipId_idx` (`championshipId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitNews()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `news` (
                `id` int NOT NULL AUTO_INCREMENT,
                `title` varchar(191) NOT NULL,
                `content` varchar(500) NOT NULL,
                `info` varchar(191),
                `userId` int NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `news_userId_idx` (`userId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitOptions()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `options` (
                `id` int NOT NULL AUTO_INCREMENT,
                `title` varchar(191) NOT NULL,
                `count` int NOT NULL DEFAULT '0',
                `voteId` int NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `options_voteId_idx` (`voteId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitPalpitations()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `palpitations` (
                `id` int NOT NULL AUTO_INCREMENT,
                `firstTeamId` int NOT NULL,
                `firstTeamGol` int NOT NULL DEFAULT '0',
                `secondTeamId` int NOT NULL,
                `secondTeamGol` int NOT NULL DEFAULT '0',
                `userId` int NOT NULL,
                `gameId` int NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `palpitations_userId_idx` (`userId`),
                KEY `palpitations_gameId_idx` (`gameId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitTeams()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `teams` (
                `id` int NOT NULL AUTO_INCREMENT,
                `name` varchar(191) NOT NULL,
                `image` varchar(191),
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitTeamsGame()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `teamsGame` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `gol` int(11) NOT NULL DEFAULT 0,
                `teamId` int(11) NOT NULL,
                `gameId` int(11) NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `teamsGame_gameId_idx` (`gameId`),
                KEY `teamsGame_teamId_idx` (`teamId`),
                CONSTRAINT `teamsGame_teamsId_gameId_unq` UNIQUE (`teamId`, `gameId`)
                ) ENGINE=InnoDB, 
                CHARSET=utf8mb4,
                COLLATE=utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitUserVotes()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `userVote` (
                `id` int NOT NULL AUTO_INCREMENT,
                `optionId` int NOT NULL,
                `userId` int NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `userVote_optionId_idx` (`optionId`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitUsers()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `users` (
                `id` int NOT NULL AUTO_INCREMENT,
                `name` varchar(191) NOT NULL,
                `email` varchar(191) NOT NULL,
                `password` varchar(191) NOT NULL,
                `role` int NOT NULL,
                `points` int,
                `document` varchar(191),
                `team` varchar(191),
                `info` varchar(191),
                `number` varchar(191),
                `birthday` varchar(191),
                `code` varchar(191),
                `userGuid` varchar(36),
                PRIMARY KEY (`id`),
                UNIQUE KEY `users_email_key` (`email`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitVotes()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `votes` (
                `id` int NOT NULL AUTO_INCREMENT,
                `title` varchar(191) NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE InnoDB,
                CHARSET utf8mb4,
                COLLATE utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }

        void InitUserPoints()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS `userPoints` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `userId` int(11) NOT NULL,
                `gameId` int(11) NOT NULL,
                `points` int(11) NOT NULL,
                `createdAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
                `updatedAt` datetime(3) NOT NULL,
                PRIMARY KEY (`id`),
                KEY `userPoints_gameId_idx` (`gameId`)
                ) ENGINE=InnoDB, 
                CHARSET=utf8mb4,
                COLLATE=utf8mb4_unicode_ci;";

            query.AppendLine(sql);
        }
    }

    #endregion
}

