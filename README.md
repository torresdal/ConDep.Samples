HowTo
==============

1. Edit `ConDepSamples\dev.env.json`<br/>
   Change "Servers" "Name" to a server you want to test deployment of samples to<br/>
   For more than one server just add them:<br/>
```
{ "Name" : "server1"},
{ "Name" : "server2"},
{ "Name" : "server n" }
```
2. Build (from Visual Studio or build.bat)
3. `cd <yourCheckoutDir>\ConDepSamples\bin\Debug`
4. `ConDep.exe ConDepSamples dev <sample>` <br/>
   ...where `<sample>` is any class inheriting from `ApplicationArtifact` e.g `DotNetWebApplication`, `DotNetWebSslApplication` etc
   
