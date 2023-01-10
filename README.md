# GhostNetwork - Notifications

![Notifications-3](https://user-images.githubusercontent.com/9577482/211377915-21cedc13-f2d4-4133-b495-bfbe6660585c.png)

## Core

Implement basic functionality and contains interfaces for template storage, template compiler, user preference storage, channels

- takes input: list of recipients, object assosiated with event
- retrieves list of channels assosiated with event
- retrieves message template for each channel for each recipient
- compiles template into message and send it through the channel 

## Channels

Implements sending logic to specific destination. Currently next channels implemented:

- Email via SMTP
- Email via SendGrid
- WebSocket (in process)


## Template storage

Provide access to message templates. Currently next channels implemented:

- Folder
- S3 (in process)

## Template compiler

Transforms template into message
