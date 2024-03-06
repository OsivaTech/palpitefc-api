namespace PalpiteApi.Api.Endpoints;

public static class News
{
    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", () =>
        {
            return "[{\"id\":2,\"title\":\"No Atlético-MG, Hulk alcança 10º \\\"duplo-duplo\\\" da carreira e se aproxima de feitos históricos\",\"content\":\"Artilheiro e \\\"garçom\\\". Foi assim na histórica temporada 2021 e agora em 2023 com a camisa do Atlético-MG. Hulk se destaca por gols e assistências. Na vitória por 3 a 1 sobre o Fortaleza, que colocou o Galo na briga pelo G-4 do Brasileiro, o atacante deu passe para gol e balançou as redes. Com isso, chegou ao 10º \\\"duplo-duplo\\\" da carreira.\\nSão 27 gols e 10 assistências, em 57 jogos na temporada. O termo \\\"duplo-duplo\\\", é usado para designar um jogador com dois dígitos em duas estatísticas.\",\"info\":null,\"userId\":3,\"createdAt\":\"2023-11-03T11:50:43.255Z\",\"updatedAt\":\"2023-11-03T11:50:43.255Z\",\"author\":{\"id\":3,\"name\":\"Redação Palpite Fc\",\"team\":\"atletico\"}},{\"id\":1,\"title\":\"Rafinha rasga elogios ao Cruzeiro após derrota para o São Paulo: 'Melhor equipe que enfrentamos'\",\"content\":\"O São Paulo venceu o Cruzeiro por 1 a 0 nesta quinta-feira (2), no Morumbi, fechando a 31ª rodada do Brasileirão. E, na opinião de Rafinha, o time mineiro foi o adversário do segundo turno.\\nApós o apito final, o lateral analisou a partida e foi só elogios para a Raposa.\\n''Vou falar a verdade, eu sou sincero. A gente perde, a gente ganha... a equipe do Cruzeiro está jogando muito, tem que dar os parabéns, ganhamos de uma grande equipe. Até agora, no segundo turno, a melhor equipe que enfrentei\\\"\",\"info\":null,\"userId\":1,\"createdAt\":\"2023-11-03T11:40:22.746Z\",\"updatedAt\":\"2023-11-03T11:40:22.746Z\",\"author\":{\"id\":1,\"name\":\"Guilherme Venâncio\",\"team\":\"cruzeiro\"}}]";
        });
    }
}
