# Rooms Generation Tool

Procedural room-and-door map generator for Unity using Binary Space Partitioning (BSP). It splits an area into connected rooms, then builds walls, doors, floor, furniture and wall decorations from your own prefabs. Same seed = same map.

## Quick start

1. **Window > Package Manager** > select **Rooms Generation Tool** > **Samples** > **Import** next to **Placeholder Demo**.
2. Drag **Map Generator (Placeholder Demo)** from the imported sample folder into your scene.
3. Select it and click **Generate** in the Inspector.

Or start from scratch: **GameObject > Rooms Generation > Map Generator**, then **Create > Rooms Generation > Map Settings** and drag the new preset into the generator's **Settings** slot.

## In code

```csharp
using AdamKacem.RoomsGeneration;

generator.Generate(1234);                       // build a map with seed 1234
RoomInfo start = generator.Rooms[0];            // position, size and doors of each room
RoomInfo end = generator.GetFarthestRoomFrom(start);
```

## Asset rules (short version)

- Walls: exactly 4 units long, pivot at floor level in the middle of the length, room side facing +Z at rotation 0.
- Floor tiles: any size (set Tile Size on the Floor Placer), pivot in the center.

Full documentation, diagrams and examples: https://github.com/AdamKacem/RoomsGenerationTool#readme
