mkdir -p ./csv

python ./team_scraper.py > ./csv/teams.csv
python ./games_scraper.py > ./csv/games.csv
python ./quarterbacks_scraper.py > ./csv/quarterbacks.csv
python ./wide_receiver_scraper.py > ./csv/widereceivers.csv
python ./tight_end_scraper.py > ./csv/tightends.csv
python ./running_back_scraper.py > ./csv/runningbacks.csv
