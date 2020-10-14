# quarterbacks_scraper.py

import uuid
from typing import Any, Dict

import bs4
import requests

import common

quarterbacks_urls = {
    "https://www.pro-football-reference.com/players/B/BradTo00.htm",
    "https://www.pro-football-reference.com/players/R/RivePh00.htm",
    "https://www.pro-football-reference.com/players/R/RyanMa00.htm",
    "https://www.pro-football-reference.com/players/G/GoffJa00.htm",
    "https://www.pro-football-reference.com/players/W/WentCa00.htm",
    "https://www.pro-football-reference.com/players/A/AlleJo02.htm",
    "https://www.pro-football-reference.com/players/B/BreeDr00.htm",
    "https://www.pro-football-reference.com/players/B/BrisJa00.htm",
    "https://www.pro-football-reference.com/players/C/CarrDe02.htm",
    "https://www.pro-football-reference.com/players/M/MayfBa00.htm",
}

# map from in-html "data-stat" strings to our csv column name strings
qb_short_stat_to_stat_name = {
    "g": "GamesPlayed",
    "gs": "GamesStarted",
    "pass_cmp": "PassesCompleted",
    "pass_att": "PassesAttempted",
    "pass_yds": "PassingYardsGained",
    "pass_td": "PassingTouchdowns",
    "pass_int": "InterceptionsThrown",
    "pass_first_down": "FirstDownsPassing",
}

def output_for_qb_url(url: str):
    # get html document
    req = requests.get(url)
    req.raise_for_status()

    # parse with beautifulsoup4
    soup = bs4.BeautifulSoup(req.text, 'html.parser')

    # extract stat summaries
    stats_2019 = soup.find_all("tr", {"id": "passing.2019"})
    assert len(stats_2019) == 1
    stats_2019 = stats_2019[0]


    stat_dict: Dict[str, Any] = common.scrape_common_player_info(soup)
    stat_dict["Position"] = "Quarterback"

    team_td = stats_2019.find_all("td", {"data-stat": "team"})
    assert len(team_td) == 1
    team_name = team_td[0].a["title"]
    TeamId = common.team_name_to_id[team_name]
    stat_dict["TeamId"] = TeamId


    for stat_row in stats_2019.find_all("td"):
        if stat_row["data-stat"] in qb_short_stat_to_stat_name:
            stat_dict[qb_short_stat_to_stat_name[stat_row["data-stat"]]] = int(stat_row.text)
    
    line = "{Id},{Position},{FirstName},{LastName},{TeamId},{GamesPlayed},{GamesStarted},{PassesCompleted},{PassesAttempted},{PassingYardsGained},{PassingTouchdowns},{InterceptionsThrown},{FirstDownsPassing}".format(
        **stat_dict
    )
    print(line)

def main():
    print("Id,Position,FirstName,LastName,TeamId,GamesPlayed,GamesStarted,PassesCompleted,PassesAttempted,PassingYardsGained,PassingTouchdowns,InterceptionsThrown,FirstDownsPassing")
    for url in quarterbacks_urls:
        try:
            output_for_qb_url(url)
        except Exception as e:
            print(f"exception during url {url}: ", e)
            raise

if __name__ == "__main__":
    main()
