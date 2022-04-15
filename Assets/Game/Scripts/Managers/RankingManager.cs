using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankingManager : MonoSingleton<RankingManager>
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private List<Character> ranking;
    private int rankingOfPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRanking();
        
    }
    private void UpdateRanking()
    {
        ranking = characters.OrderByDescending(o => o.transform.position.z).ToList();
        rankingOfPlayer = ranking.FindIndex(o=> o.transform.CompareTag("Player"));
        UIManager.Instance.InGameScreen.UpdateRankText(rankingOfPlayer+1);
    }
}
