using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {
    static void Main(String[] args) {
        List<int> nmq = Console.ReadLine().Trim().Split(null).Select(s => int.Parse(s)).ToList();
        int n = nmq[0];
        int m = nmq[1];
        int q = nmq[2];

        List<int> gameChoices = Console.ReadLine().Trim().Split(null).Select(s => int.Parse(s)).ToList();
        Dictionary<int, List<int>> gameToPlayers = new Dictionary<int, List<int>>();
        for (int i = 0; i < gameChoices.Count; i++) {
            if (!gameToPlayers.ContainsKey(gameChoices[i])) {
                gameToPlayers[gameChoices[i]] = new List<int>();
            }
            gameToPlayers[gameChoices[i]].Add(i + 1);
        }

        Dictionary<int, int> playerToComp = new Dictionary<int, int>();
        for (int i = 1; i <= n; i++) {
            playerToComp[i] = i;
        }
    }
}
