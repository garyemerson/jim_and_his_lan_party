using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class ConnectedCompenent {
    // game -> num players for that game in this conn comp
    public Dictionary<int, int> NumPlayersByGame;

    // player in this conn comp
    public List<int> players;
}

class Solution {
    static void Main(String[] args) {
        int numPlayers;
        int numGames;
        int numWires;
        getParamsData(out numPlayers, out numGames, out numWires);

        Dictionary<int, int> playerToGame;
        Dictionary<int, int> totalPlayersForGame;
        getGameData(out playerToGame, out totalPlayersForGame);

        Dictionary<int, int> gameToStartTime = new Dictionary<int, int>();
        for (int i = 1; i <= numGames; i++) {
            gameToStartTime[i] = -1;
        }

        Dictionary<int, ConnectedCompenent> components = getInitialComponents(
            playerToGame,
            numPlayers,
            gameToStartTime,
            totalPlayersForGame);

        for (int wire = 1; wire <= numWires; wire++) {
            List<int> players = Console.ReadLine().Trim().Split(null).Select(s => int.Parse(s)).ToList();
            int p1 = players[0];
            int p2 = players[1];
            if (components[p1].NumPlayersByGame.Count > components[p2].NumPlayersByGame.Count) {
                // combine game player counts
                foreach (int game in components[p2].NumPlayersByGame.Keys) {
                    if (!components[p1].NumPlayersByGame.ContainsKey(game)) {
                        components[p1].NumPlayersByGame[game] = 0;
                    }
                    components[p1].NumPlayersByGame[game] += components[p2].NumPlayersByGame[game];

                    if (components[p1].NumPlayersByGame[game] >= totalPlayersForGame[game] &&
                        gameToStartTime[game] == -1)
                    {
                        gameToStartTime[game] = wire;
                    }
                }

                // all players in p2's component now go to p1's component
                foreach (int p in components[p2].players) {
                    components[p] = components[p1];
                    components[p1].players.Add(p);
                }
            } else {
                // combine game player counts
                foreach (int game in components[p1].NumPlayersByGame.Keys) {
                    if (!components[p2].NumPlayersByGame.ContainsKey(game)) {
                        components[p2].NumPlayersByGame[game] = 0;
                    }
                    components[p2].NumPlayersByGame[game] += components[p1].NumPlayersByGame[game];

                    if (components[p2].NumPlayersByGame[game] >= totalPlayersForGame[game] &&
                        gameToStartTime[game] == -1)
                    {
                        gameToStartTime[game] = wire;
                    }
                }

                // all players in p1's component now go to p2's component
                foreach (int p in components[p1].players) {
                    components[p] = components[p2];
                    components[p2].players.Add(p);
                }
            }
        }

        for (int game = 1; game <= numGames; game++) {
            Console.WriteLine(gameToStartTime[game]);
        }
    }

    static Dictionary<int, ConnectedCompenent> getInitialComponents(
        Dictionary<int, int> playerToGame,
        int numPlayers,
        Dictionary<int, int> gameToStartTime,
        Dictionary<int, int> totalPlayersForGame)
    {
        Dictionary<int, ConnectedCompenent> components = new Dictionary<int, ConnectedCompenent>();
        for (int p = 1; p <= numPlayers; p++) {
            if (totalPlayersForGame[playerToGame[p]] == 1) {
                gameToStartTime[playerToGame[p]] = 0;
            }
            components[p] = new ConnectedCompenent() {
                NumPlayersByGame = new Dictionary<int, int>() {
                    { playerToGame[p] , 1 }
                },
                players = new List<int>() { p },
            };
        }
        return components;
    }

    static void getParamsData(out int numPlayers, out int numGames, out int numWires) {
        List<int> data = Console.ReadLine().Trim().Split(null).Select(s => int.Parse(s)).ToList();
        numPlayers = data[0];
        numGames = data[1];
        numWires = data[2];
    }

    static void getGameData(out Dictionary<int, int> playerToGame, out Dictionary<int, int> totalPlayersForGame) {
        List<int> games = Console.ReadLine().Trim().Split(null).Select(s => int.Parse(s)).ToList();
        playerToGame = new Dictionary<int, int>();
        totalPlayersForGame = new Dictionary<int, int>();
        for (int i = 0; i < games.Count; i++) {
            playerToGame[i + 1] = games[i];
            if (!totalPlayersForGame.ContainsKey(games[i])) {
                totalPlayersForGame[games[i]] = 0;
            }
            totalPlayersForGame[games[i]]++;
        }
    }
}
