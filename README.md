# SimpleUnlocker
![void with a Purple Sash and cosmetics they don't own](https://files.freewifi.club/noindex/simpleunlocker.banner.png)

A dead simple customizable unlocker for anything within PEAK that requires progression or some type of objective to be complete.
- `Cosmetics` (Enabled by default)
  - Unlocks all Cosmetics, even if you don't have it unlocked in the base game.
  - Non destructive. (Relocks items if the mod or config option is disabled)
  - Works with Cosmetics that require an objective (ex. Beat all Ascents) other than earning an achievement.    
  - Doesn't fill your Sash with accolades you haven't obtained (by not modifying achievements) unlike some unlockers.
- `Sash Colors`
  - Pick freely between all Sashes from Blue to Gold.
  - Doesn't lock Ascents based on your chosen Sash Color.
  - Uses your highest ascent Sash (like Vanilla) if disabled.
- `Ascents`
  - Unlocks all Ascents for you to play.
  - Still allows you to unlock Sashes for Vanilla / without the mod.
    - May allow you to fast track Sashes by playing a higher Ascent than you could normally play.

## Configuration
You can modify all aspects from this mod by modifying the config file at `BepInEx/config/club.freewifi.void.SimpleUnlocker.cfg`, modifying it ingame is not possible at this time.
<details>
<summary>Default Config</summary>

```properties
## Settings file was created by plugin SimpleUnlocker v1.0.0
## Plugin GUID: club.freewifi.void.SimpleUnlocker

[General]

## Unlocks all Cosmetics within the game.
# Setting type: Boolean
# Default value: true
Unlock Cosmetics = true

## Changes your Sash color. Default value will select your Sash color based on your highest completed ascent, this will not affect which Ascents you have available.
# Setting type: SashColor
# Default value: Default
# Acceptable values: Blue, Green, LightGreen, Black, Brown, Red, Purple, Silver, Gold, Default
Sash = Default

## Unlocks all Ascents within the game.
# Setting type: Boolean
# Default value: false
Unlock Ascents = false
```
</details>