import { peerSocket } from "messaging";
import { WebSocketWrapper } from "./WebSocketWrapper";
import { HealthData } from "./HealthData"

const wsUri = "wss://159.223.238.159:4649/";
const websocket = new WebSocketWrapper(wsUri);

const apiUri = "https://postman-echo.com/post";
const healthData = new HealthData(apiUri, (data) => {
  peerSocket.send(data);
});

// Every minute upload health data
const timeOutTime = 60000;
setTimeout(healthData.publish.bind(healthData), timeOutTime);


peerSocket.onopen = evt => {
  websocket.connect();
}

peerSocket.onmessage = evt => {
  websocket.send(evt.data);
  healthData.addData(evt.data.heartRate);
};

