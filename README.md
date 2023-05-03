# catan-engine

## compositions 
![catan](https://user-images.githubusercontent.com/77629956/235498003-7aab05e6-09df-458a-8363-c634a2dd98ac.svg)

## encodings
| component      | feature              | description   | encoding          |
|----------------|----------------------|---------------|-------------------|
| _ITerrainTile_ | _Terrain_            | isPasture     | 1 if true, else 0 |
|                |                      | isField       | 1 if true, else 0 |
|                |                      | isMountain    | 1 if true, else 0 |
|                |                      | isHill        | 1 if true, else 0 |
|                |                      | isForest      | 1 if true, else 0 |
|                |                      | isDesert      | 1 if true, else 0 |
|                | _Production_         | necessaryRoll | normalized [0, 1] |
|                |                      | odds          | normalized [0, 1] |
| _IEdgeTile_    | _ReducedTrade (3:1)_ | allowsAny     | 1 if true, else 0 |
|                | _SpecialTrade (2:1)_ | allowsSheep   | 1 if true, else 0 |
|                |                      | allowsWheat   | 1 if true, else 0 |
|                |                      | allowsOre     | 1 if true, else 0 |
|                |                      | allowsBrick   | 1 if true, else 0 |
|                |                      | allowsWood    | 1 if true, else 0 |
