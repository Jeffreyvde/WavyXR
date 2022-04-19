import { HeartRateSensor } from "heart-rate";
import * as document from "document";
import { peerSocket } from "messaging";

const hrmData = document.getElementById("hrm-data");

const hrm = new HeartRateSensor({ frequency: 1 });

// Heart Rate System
hrm.addEventListener("reading", () => 
{
  hrmData.text = hrm.heartRate ? hrm.heartRate : 0;
  
  // Send data to companion app
  if (peerSocket.readyState === peerSocket.OPEN) 
  {
      let data = 
      {
          heartBeat: hrm.heartRate  
      }
      peerSocket.send(data);
  }
});

hrm.addEventListener("onerror", () => 
{
  hrmData.text = "-";
});

hrm.start();


peerSocket.onmessage = evt => 
{
  console.log("Test123:")
  console.log(JSON.stringify(evt));
};
