# Wick John

This is my submission for the 4482A Tier 3 APP assignment.

## Outline
You play as a candle, which can draw flame from other candles, or burn down burnable items. The goal in each level is to light the rest of your candle friends. There also various interactable objects as described below.

Main menu:
-Start button (Leads to level selection screen)
-Exit button
-Instructions

Level Selection Menu:
 - Level progression system, where each successive level is unlocked upon completion of the previous one.
 - Persistent save state:
   - Unlocked levels
   - Current best number of moves used to solve level

Level Gameplay:
 - Input for character movement
 - Object types:
   - Immovable Walls
   - Push-able blocks
   - Flammable tree stumps (propagates to adjacent cobwebs)
   - Puppies (avoid touching or else they will get hurt and fail the level)
   - Wet/rain tiles, which will extinguish your candle’s flames
   - Light-able candles
     - You can also become lit by walking next to other already lit candles
     - Your keyboard input moves all lit candles at once. In other words, once a candle is lit, the player input will begin to apply to as well.
 - Move counter
 - Reset Button (R)
 - Undo Button (Z)
 - Exit button to level select (ESC)
 - Win condition: all candles in scene are lit.
   - Redirect to level selection screen
