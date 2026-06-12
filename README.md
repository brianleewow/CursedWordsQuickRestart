# Cursed Words Quick Restart

A MelonLoader mod for the Steam game
[`Cursed Words: The Word Game That Isn't`](https://store.steampowered.com/app/3856460/).

Hold `R` for 1 second during an active run to restart with the same character,
crown level, and run type.

## Requirements

- `Cursed Words`
- MelonLoader

MelonLoader links:

- MelonLoader: https://github.com/LavaGang/MelonLoader
- Official wiki: https://melonwiki.xyz/
- Releases: https://github.com/LavaGang/MelonLoader/releases

## Install

1. Install MelonLoader for `Cursed Words`.
2. Launch the game once, then close it.
3. Download `CursedWordsQuickRestart.dll` from this repo's `releases/` folder or
   the release zip.
4. Open the game's install folder in Steam:

   ```text
   Right click Cursed Words -> Manage -> Browse local files
   ```

5. Put the DLL in the `Mods` folder inside that install folder. Create `Mods` if
   it does not exist yet.

   Common Windows example:

   ```text
   C:\Program Files (x86)\Steam\steamapps\common\Cursed Words\Mods\
   ```

   Common Linux/Steam Deck example:

   ```text
   ~/.steam/steam/steamapps/common/Cursed Words/Mods/
   ```

6. Launch the game.

## Compatibility

Built against the `Cursed Words` Unity `6000.0.62f1` Mono build. Future game
updates may require a mod update if run restart methods or scene flow change.

## Notes

This repository does not distribute game assets or decompiled game source.
The included source only calls public game methods at runtime.
