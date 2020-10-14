# tight_end_scraper.py

import uuid
from typing import Any, Dict

import bs4
import requests

import common

te_urls = {
    "https://www.pro-football-reference.com/players/H/HursHa00.htm",
    "https://www.pro-football-reference.com/players/A/AkinJo00.htm",
    "https://www.pro-football-reference.com/players/B/BoylNi00.htm",
    "https://www.pro-football-reference.com/players/D/DoylJa00.htm",
    "https://www.pro-football-reference.com/players/E/EngrEv00.htm",
    "https://www.pro-football-reference.com/players/F/FantNo00.htm",
    "https://www.pro-football-reference.com/players/G/GesiMi00.htm",
    "https://www.pro-football-reference.com/players/G/GrahJi00.htm",
    "https://www.pro-football-reference.com/players/H/HowaO.00.htm",
    "https://www.pro-football-reference.com/players/O/OlseGr00.htm",
}
# Receptions,ReceivingYards,FirstDownsReceiving,YardsBeforeCatch,YardsAfterCatch")
te_short_stat_to_stat_name = {
    "g": "GamesPlayed",
    "gs": "GamesStarted",
    "targets": "PassTargets",
    "rec": "Receptions",
    "rec_yds": "ReceivingYards",
    "rec_first_down": "FirstDownsReceiving",
    "rec_air_yds": "YardsBeforeCatch",
    "rec_yac": "YardsAfterCatch",
}

def output_for_te_url(url: str):
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
    stat_dict["Position"] = "Tight End"

    team_td = stats_2019.find_all("td", {"data-stat": "team"})
    assert len(team_td) == 1
    team_name = team_td[0].a["title"]
    team_id = common.team_name_to_id[team_name]
    stat_dict["TeamId"] = team_id


    for stat_row in stats_2019.find_all("td"):
        if stat_row["data-stat"] in te_short_stat_to_stat_name:
            stat_dict[te_short_stat_to_stat_name[stat_row["data-stat"]]] = int(stat_row.text)
    
    # not only are these stats stored in a different table, but they are
    # somehow "hidden" within a comment yet still displayed in browsers, so we
    # have to pull the table html code string out of a comment.
    # this code in particular is a bit of a mess.
    more_stats_2019 = soup.find_all("div", {"id": "all_detailed_receiving_and_rushing"})
    assert len(more_stats_2019) == 1, "found more than one detailed div"
    more_stats_2019 = more_stats_2019[0]
    commented_stats = more_stats_2019.find_all(
        string=lambda text: isinstance(text, bs4.Comment)
    )
    assert len(commented_stats) == 1, "found more than one comment"
    
    soup2 = bs4.BeautifulSoup(commented_stats[0], "html.parser")
    advanced_stats_year_rows = soup2\
        .find("table", {"id": "detailed_receiving_and_rushing"})\
        .find_all("tr", {"class":"full_table"})
    
    row_2019 = list(
        filter(lambda year_row: year_row.a.text == "2019", advanced_stats_year_rows)
    )
    assert len(row_2019) == 1
    row_2019 = row_2019[0]

    for stat in row_2019:
        short_stat = stat["data-stat"]
        if short_stat in te_short_stat_to_stat_name and short_stat not in stat_dict:
            stat_dict[te_short_stat_to_stat_name[short_stat]] = int(stat.text)
    
    line = "{Id},{Position},{FirstName},{LastName},{TeamId},{GamesPlayed},{GamesStarted},{PassTargets},{Receptions},{ReceivingYards},{FirstDownsReceiving},{YardsBeforeCatch},{YardsAfterCatch}".format(
        **stat_dict
    )
    print(line)

def main():
    print("Id,Position,FirstName,LastName,TeamId,GamesPlayed,GamesStarted,PassTargets,Receptions,ReceivingYards,FirstDownsReceiving,YardsBeforeCatch,YardsAfterCatch")
    for url in te_urls:
        try:
            output_for_te_url(url)
        except Exception as e:
            print(f"exception during url {url}: ", e)
            raise

if __name__ == "__main__":
    main()
