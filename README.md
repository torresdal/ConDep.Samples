HowTo
==============

1. Edit dev.env.json<br/>
   Change "Servers" "Name" to a server you want to test deployment of samples to<br/>
   For more servers just add:<br/>
```js
{ "Name" : "server1"},
{ "Name" : "server2"},
{ "Name" : "server n" }
```
2. Build (from Visual Studio or build.bat)
3. cd <yourCheckoutDir>\ConDepSamples\bin\Debug
4. ConDep.exe ConDepSamples dev <sample> <br/>
   <sample> is any class inheriting from ApplicationArtifact e.g DotNetWebApplication, DotNetWebSslApplication etc
   
