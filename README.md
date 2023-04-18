# catan-engine

```mermaid
flowchart TD
    IVectorizable --> IProductionCircle
    IProductionCircle -.-> ProductionCircle
    
    IVectorizable --> IHexTile
    
    IHexTile --> IEdgeTile
    IEdgeTile -.-> EdgeTile

    IHexTile --> ITerrainTile
    ITerrainTile -.-> TerrainTile
   
    style ProductionCircle fill:#ccffcc, stroke:#009933
    style EdgeTile fill:#ccffcc, stroke:#009933
    style TerrainTile fill:#ccffcc, stroke:#009933
```
