# running_back_scraper.py

import uuid
from typing import Any, Dict

import bs4
import requests

import common

rb_urls = {
    "https://www.pro-football-reference.com/players/F/FourLe00.htm",
    "https://www.pro-football-reference.com/players/B/BarkSa00.htm",
    "https://www.pro-football-reference.com/players/S/SandMi01.htm",
    #"https://www.pro-football-reference.com/players/D/DrakKe00.htm", # mutliple teams in 2019; don't use
    "https://www.pro-football-reference.com/players/H/HydeCa00.htm",
    "https://www.pro-football-reference.com/players/J/JacoJo01.htm",
    "https://www.pro-football-reference.com/players/L/LindPh00.htm",
    "https://www.pro-football-reference.com/players/M/MackMa00.htm",
    "https://www.pro-football-reference.com/players/M/MostRa00.htm",
    "https://www.pro-football-reference.com/players/E/EdwaGu00.htm",
    "https://www.pro-football-reference.com/players/P/PeteAd01.htm"
}

# map from in-html "data-stat" strings to our csv column name strings
rb_short_stat_to_stat_name = {
    "g": "GamesPlayed",
    "gs": "GamesStarted",
    "rush_att": "RushingAttempts",
    "rush_yds": "RushingYards",
    "rush_td": "RushingTouchdowns",
    "rush_first_down": "FirstDownsRushing",
    "rush_long": "LongestRushingAttempt",
}

def output_for_rb_url(url: str):
    # get html document
    req = requests.get(url)
    req.raise_for_status()

    # parse with beautifulsoup4
    soup = bs4.BeautifulSoup(req.text, 'html.parser')

    # extract stat summaries
    stats_2019 = soup.find_all("tr", {"id": "rushing_and_receiving.2019"})
    assert len(stats_2019) == 1
    stats_2019 = stats_2019[0]


    stat_dict: Dict[str, Any] = common.scrape_common_player_info(soup)
    stat_dict["Position"] = "Running Back"

    team_td = stats_2019.find_all("td", {"data-stat": "team"})
    assert len(team_td) == 1
    team_name = team_td[0].a["title"]
    TeamId = common.team_name_to_id[team_name]
    stat_dict["TeamId"] = TeamId


    for stat_row in stats_2019.find_all("td"):
        if stat_row["data-stat"] in rb_short_stat_to_stat_name:
            stat_dict[rb_short_stat_to_stat_name[stat_row["data-stat"]]] = int(stat_row.text)
    
    line = "{Id},{Position},{FirstName},{LastName},{TeamId},{GamesPlayed},{GamesStarted},{RushingAttempts},{RushingYards},{RushingTouchdowns},{FirstDownsRushing},{LongestRushingAttempt}".format(
        **stat_dict
    )
    print(line)

def main():
    print("Id,Position,FirstName,LastName,TeamId,GamesPlayed,GamesStarted,RushingAttempts,RushingYards,RushingTouchdowns,FirstDownsRushing,LongestRushingAttempt")
    for url in rb_urls:
        try:
            output_for_rb_url(url)
        except Exception as e:
            print(f"exception during url {url}: ", e)
            raise

if __name__ == "__main__":
    main()
