# Websocket tester
Source directory for server test application for remote controller. This app can be used as a server when developing and testing websocket communications between remote controller and server.

The current implementation echoes back any message send to server.

The server is developed using [websocket-sharp](https://github.com/sta/websocket-sharp) (https://github.com/sta/websocket-sharp), which is included in the project via a DLL.

## Building
Ensure output path for build is set correctly!