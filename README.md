# catan-engine

## compositions 

![catan](https://user-images.githubusercontent.com/77629956/235498003-7aab05e6-09df-458a-8363-c634a2dd98ac.svg)

outdated as of 07.05.

## encodings

| component             | feature              | description          | encoding                       |
|-----------------------|----------------------|----------------------|--------------------------------|
| _ITerrainTile_        | _Terrain_            | isPasture            | 1 if true, else 0              |
|                       |                      | isField              | 1 if true, else 0              |
|                       |                      | isMountain           | 1 if true, else 0              |
|                       |                      | isHill               | 1 if true, else 0              |
|                       |                      | isForest             | 1 if true, else 0              |
|                       | _Production_         | necessaryRoll        | normalized [0, 1], 0 if Desert |
|                       |                      | odds                 | normalized [0, 1], 0 if Desert |
| _IEdgeTile_           | _ReducedTrade (3:1)_ | allowsAny            | 1 if true, else 0              |
|                       | _SpecialTrade (2:1)_ | allowsSheep          | 1 if true, else 0              |
|                       |                      | allowsWheat          | 1 if true, else 0              |
|                       |                      | allowsOre            | 1 if true, else 0              |
|                       |                      | allowsBrick          | 1 if true, else 0              |
|                       |                      | allowsWood           | 1 if true, else 0              |
| _Vertex (Settlement)_ | _Belongs_            | belongsCurrent       | 1 if true, else 0              |
|                       |                      | belongsOpponent_1    | 1 if true, else 0              |
|                       |                      | belongsOpponent_2    | 1 if true, else 0              |
|                       |                      | belongsOpponent_3    | 1 if true, else 0              |
|                       | _IsSettlement_       | placedSettlement     | 1 if true, else 0              |
|                       | _IsCity_             | placedCity           | 1 if true, else 0              |
| _Edge (Road)_         | _Belongs_            | _see Vertex Belongs_ | _see Vertex Belongs_           |
|                       | _IsRoad_*            | placedRoad           | 1 if true, else 0              |
| _IPlayer (Current)_ | _Resources_     | #Sheep   | [0, 1] by #/19 |
|                |                      | #Wheat   | [0, 1] by #/19 |
|                |                      | #Ore     | [0, 1] by #/19 |
|                |                      | #Brick   | [0, 1] by #/19 |
|                |                      | #Wood    | [0, 1] by #/19 |

\* Might be unnecessary since roads only have a single state and _Belongs_ should already indicate that a player has built a road here. Just saw this now.
