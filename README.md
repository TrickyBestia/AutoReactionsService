# AutoReactionsService

## General

A service for the [TrickyBot](https://github.com/TrickyBestia/TrickyBot) that makes the bot be able to automatically add reactions to messages.

## Discord commands

### `autoreactions add {rolePing} {reaction}`

Starts auto-adding specified reaction to messages of the specified role.
Requires `autoreactions.add` permission.

### `autoreactions remove {rolePing} {reaction}`

Stops auto-adding specified reaction to messages of the specified role.
Requires `autoreactions.remove` permission.

### `autoreactions list`

Sends a message containing information about currently active autoreactions.
Requires `autoreactions.list` permission.
