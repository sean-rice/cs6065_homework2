from typing import Any, Dict
import uuid

import bs4

# we pre-generate these guids for easier reference and future merging into db
team_name_to_id = {
    "New England Patriots": "2a0bc705-1ab4-4481-9fd1-faaa0d514664",
    "Buffalo Bills": "1f819a1e-48ac-4ab4-8d68-3d103e800169",
    "New York Jets": "c4ccf4f4-bd6f-4f00-b529-b2ae3094161e",
    "Miami Dolphins": "c72d6287-cb95-4406-873b-dcca14b08a12",
    "Baltimore Ravens": "b90cc0d9-4b4a-436d-8989-ebe642dff849",
    "Pittsburgh Steelers": "dea4d603-4845-420b-8978-7057dd9796f5",
    "Cleveland Browns": "53c7789c-5eaa-4c93-99ce-f0c9ee6b11e3",
    "Cincinnati Bengals": "f7f4468c-4577-41fa-964f-d8280f8752c0",
    "Houston Texans": "e9c3a390-cfef-4291-a86a-c7a5f41bd341",
    "Tennessee Titans": "fcd0846b-a0be-43a1-9f42-d776f51ca72e",
    "Indianapolis Colts": "123ed6b8-ba00-486f-b968-2fb4baeadec9",
    "Jacksonville Jaguars": "523efd9e-9349-4bc9-8124-2c77b4f74718",
    "Kansas City Chiefs": "e6846c31-bcff-4ab5-be07-3001230d83db",
    "Denver Broncos": "e5bede41-4ca7-4008-aef9-139eb27eaed9",
    "Oakland Raiders": "616989a3-d726-4f0d-9c41-e074b6f11b9d",
    "Los Angeles Chargers": "dc36ea04-3e37-49a8-a362-4c389c4c50f3",
    "Philadelphia Eagles": "6924268e-67d7-49a4-8144-40a1f7ed7b46",
    "Dallas Cowboys": "af8475ec-d7e5-4528-a9d5-6d67816cc337",
    "New York Giants": "23d2b711-81e5-47d4-aafd-2d8e94014f47",
    "Washington Redskins": "c554ae26-303b-4fb9-a9b6-86f94c22ef62",
    "Green Bay Packers": "8c1b5db2-7e57-49e3-a77b-49ad8e2726bb",
    "Minnesota Vikings": "140a432b-110b-4f86-b5e6-43bca1613d90",
    "Chicago Bears": "3afdc1a8-411c-401d-9b2a-a42fd47d6501",
    "Detroit Lions": "60d4bbe1-4e1a-4aa9-92ac-2c61a8461954",
    "New Orleans Saints": "a3f9b9e7-0cc8-4c96-8695-cc6918559553",
    "Atlanta Falcons": "aa40e3c1-fb22-421e-a984-7983f627baa2",
    "Tampa Bay Buccaneers": "334ecdf6-c757-4dbb-be32-4fd7c364dbe2",
    "Carolina Panthers": "1e2f1baa-d51e-4d34-9e81-6621a4021b12",
    "San Francisco 49ers": "38f18c7e-a375-4160-9498-7b71f1800063",
    "Seattle Seahawks": "a8513f2b-2dc8-4690-87c9-7437409acd5d",
    "Los Angeles Rams": "5ab4d5e6-2a8a-4fc2-a6e3-58f2f3658416",
    "Arizona Cardinals": "7dd98d56-6e53-45e7-b453-fb1c21240097",
}

def scrape_common_player_info(soup):
    d: Dict[str, Any] = {}
    d["Id"] = uuid.uuid4()
    name_list = soup.find("h1", {"itemprop": "name"}).span.text.strip().split(" ")
    first_name = name_list[0]
    last_name = " ".join(name_list[1:])
    d["FirstName"] = first_name
    d["LastName"] = last_name
    return d
