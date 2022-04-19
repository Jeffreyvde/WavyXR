export class HealthData
{
    constructor(url, responseCallback)
    {
      console.log(url);
      this.url = url;

      this.heartObject = { heartData: [0,20,300] };
      this.responseCallback = responseCallback;
      
      console.log(JSON.stringify(this.heartObject));
    }
  
    publish()
    {
      console.log(this.url);
      fetch(this.url, {
        method: "POST",
        headers: {'Content-Type': 'application/json'}, 
        body: JSON.stringify(this.heartObject)
      }).then(this.responseCallback);
    }
  
    addData(heartBeat)
    {
      console.log("CONNECTED");
    }
  
    onClosed(socket)
    {
      console.log("CONNECTED");
    }
  
    onMessage(evt)
    {
      console.log(`MESSAGE: ${evt.data}`);
    }
}