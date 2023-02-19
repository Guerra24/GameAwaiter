# Game Awaiter

A launcher for miHoYo/HoYoverse games running under [Moonlight](https://github.com/moonlight-stream/)/[Sunshine](https://github.com/LizardByte/Sunshine)

# Setup

1. Download the executable and place it somewhere.

2. Create a new application in Sunshine.

3. Set the command to the following:

`<path to GameAwaiter> "<path to game exe>" <delay>`

### Examples:

`C:\Tools\GameAwaiter.exe "D:\Program Files\Star Rail\Games\StarRail.exe" 2000`

`C:\Tools\GameAwaiter.exe "D:\Program Files\Honkai Impact 3rd glb\Games\BH3.exe" 2000`

`C:\Tools\GameAwaiter.exe "D:\Program Files\Genshin Impact\Genshin Impact game\GenshinImpact.exe" 2000`

### Notes

Make sure you use the game executable, not the launcher.

The last parameter is the delay used to prevent Sunshine from getting confused due to the UAC dialog showing before display capture has been initialized. In milliseconds and by default uses 5s.

# Why?

miHoYo/HoYoverse games require administrative permissions to run, which does not work under Sunshine, see [#764](https://github.com/LizardByte/Sunshine/issues/764). Also, the games use a wrapper process that bootstraps the real process which means the original PID is not the real game, so once that ends Sunshine kills the stream.

This launcher workarounds both issues by monitoring the wrapper and then switching to the real process by using the exe name. It can also detect if the game is already running and resume the stream without relaunching the game.