# weeks_games_scraper.py

import datetime
import uuid

import bs4
import dateparser
import requests

import common

BASE_WEEK_STR = "https://www.pro-football-reference.com/years/2019/week_{}.htm"

def output_for_week(week_number: int):
    
    url = BASE_WEEK_STR.format(week_number)

    # get html document
    req = requests.get(url)
    req.raise_for_status()

    # parse with beautifulsoup4
    soup = bs4.BeautifulSoup(req.text, 'html.parser')

    # extract game summaries
    game_summaries = soup.find_all("div", {"class": "game_summary"})

    for game_summary in game_summaries:
        teams_table_rows = game_summary.tbody.find_all("tr")
        assert len(teams_table_rows) == 3, "got non-3 teams table rows count??"

        date_index, away_index, home_index = 0, 1, 2

        date_text = teams_table_rows[date_index].text
        date = dateparser.parse(date_text)

        away_team_short = teams_table_rows[away_index].td.a["href"].split("/")[2]
        away_team = teams_table_rows[away_index].td.text
        away_points = int(teams_table_rows[away_index].find_all("td")[1].text)

        home_team_short = teams_table_rows[home_index].td.a["href"].split("/")[2]
        home_team = teams_table_rows[home_index].td.text
        home_points = int(teams_table_rows[home_index].find_all("td")[1].text)

        print("{Id},{Week},{PrimaryTeamId},{SecondaryTeamId},{PrimaryPoints},{SecondaryPoints}".format(
            **{
                "Id": str(uuid.uuid4()),
                "Week": week_number,
                "PrimaryTeamId": common.team_name_to_id[home_team],
                "SecondaryTeamId": common.team_name_to_id[away_team],
                "PrimaryPoints": home_points,
                "SecondaryPoints": away_points
            }
        ))

def main():
    print("Id,Week,PrimaryTeamId,SecondaryTeamId,PrimaryPoints,SecondaryPoints")
    for i in range(1, 17+1):
        output_for_week(i)

if __name__ == "__main__":
    main()
