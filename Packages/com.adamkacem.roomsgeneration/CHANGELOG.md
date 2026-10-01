# Changelog

## [1.0.0] - 2026-10-01

First release as a Unity package.

### Added
- Generate, New Seed + Generate and Clear buttons in the Inspector. Maps can be built in the editor, not only in Play mode.
- Map Settings preset asset (map size, min room side, allow big rooms, room prefab), shown inside the generator's Inspector.
- Warnings for settings that can't work (map too small to split, size not a multiple of 4, missing room prefab).
- Room data: `MainGenerator.Rooms` (position, size, doors of every room), `GetRoomAt`, `GetFarthestRoomFrom` and the `OnGenerated` event.
- Random Seed Each Time, Generate On Start and Use This Object Position options.
- Floor Placer: Tile Size and Height Offset, works with any floor tile size and keeps the prefab's own scale.
- GameObject > Rooms Generation > Map Generator menu item.
- Placeholder Demo sample: grey-box walls, door, floor, furniture and decorations, a room prefab, a preset and an example script.

### Changed
- All scripts are in the `AdamKacem.RoomsGeneration` namespace, with their own assembly definitions.
- `minSize` is now **Min Room Side**, `widthTest` / `heightTest` are now **Map Width** / **Map Depth** (in the preset).
- Wall decorations and prop rotations now follow the seed, so the same seed always gives exactly the same map.

### Removed
- Unused ProBuilder dependency.
