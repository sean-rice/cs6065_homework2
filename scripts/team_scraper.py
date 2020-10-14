# team_scraper.py
# doesn't actually scrape, just outputs

import common

def main():
    print("Id,Name")
    for team_name, team_id in common.team_name_to_id.items():
        print(f"{team_id},{team_name}")


if __name__ == "__main__":
    main()
