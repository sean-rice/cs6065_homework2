# wide_receiver_scraper.py

import uuid
from typing import Any, Dict

import bs4
import requests

import common

wr_urls = {
    "https://www.pro-football-reference.com/players/A/AdamDa01.htm",
    "https://www.pro-football-reference.com/players/B/BeckOd00.htm",
    "https://www.pro-football-reference.com/players/B/BrowJo02.htm",
    "https://www.pro-football-reference.com/players/E/EvanMi00.htm",
    "https://www.pro-football-reference.com/players/G/GallMi00.htm",
    "https://www.pro-football-reference.com/players/G/GollKe00.htm",
    "https://www.pro-football-reference.com/players/H/HillTy00.htm",
    "https://www.pro-football-reference.com/players/K/KuppCo00.htm",
    "https://www.pro-football-reference.com/players/L/LockTy00.htm",
    "https://www.pro-football-reference.com/players/P/ParkDe01.htm",
}

wr_short_stat_to_stat_name = {
    "g": "GamesPlayed",
    "gs": "GamesStarted",
    "targets": "PassTargets",
    "rec": "Receptions",
    "rec_yds": "ReceivingYards",
    "rec_td": "ReceivingTouchdowns",
    "rec_first_down": "FirstDownsReceiving",
}

def output_for_wr_url(url: str):
    # get html document
    req = requests.get(url)
    req.raise_for_status()

    # parse with beautifulsoup4
    soup = bs4.BeautifulSoup(req.text, 'html.parser')

    # extract stat summaries
    stats_2019 = soup.find_all("tr", {"id": "receiving_and_rushing.2019"})
    assert len(stats_2019) == 1
    stats_2019 = stats_2019[0]


    stat_dict: Dict[str, Any] = common.scrape_common_player_info(soup)
    stat_dict["Position"] = "Wide Receiver"

    team_td = stats_2019.find_all("td", {"data-stat": "team"})
    assert len(team_td) == 1
    team_name = team_td[0].a["title"]
    team_id = common.team_name_to_id[team_name]
    stat_dict["TeamId"] = team_id


    for stat_row in stats_2019.find_all("td"):
        if stat_row["data-stat"] in wr_short_stat_to_stat_name:
            stat_dict[wr_short_stat_to_stat_name[stat_row["data-stat"]]] = int(stat_row.text)
    
    line = "{Id},{Position},{FirstName},{LastName},{TeamId},{GamesPlayed},{GamesStarted},{PassTargets},{Receptions},{ReceivingYards},{ReceivingTouchdowns},{FirstDownsReceiving}".format(
        **stat_dict
    )
    print(line)

def main():
    print("Id,Position,FirstName,LastName,TeamId,GamesPlayed,GamesStarted,PassTargets,Receptions,ReceivingYards,ReceivingTouchdowns,FirstDownsReceiving")
    for url in wr_urls:
        try:
            output_for_wr_url(url)
        except Exception as e:
            print(f"exception during url {url}: ", e)
            raise

if __name__ == "__main__":
    main()
