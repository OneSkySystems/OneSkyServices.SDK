# OneSky Analytical Services C# Software Development Kit

OneSky Systems Inc. (OneSky) hosts a set of web services for performing complex analyses.  
The services are located here: <https://saas.agi.com/V1.>
This project provides a C# SDK for interacting with the services.
\
*Note that the software hosted in this repository provides no functionality in and of itself.  It simply provides a development kit that makes using OneSky's Analytical Services easier.  To use the Analytical Services (with or without this development kit), you will need an API key.  That key can be obtained by visiting the services site above, and following the instructions there.*

## How to use this SDK

To use this SDK with OneSky's Analytical Services, follow these steps.

1. First contact OneSky <https://onesky.xyz/contact>, to obtain an API key.

  Once you have the API key, continue on to the next step.

2. Next, create a file called OneSky.Services.config, and place this text in it:  

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
<appSettings>
    <add key="ApiKey" value="OneSky provided API Key" />
    <add key="BaseUrl" value="https://saas.agi.com" />
</appSettings>
</configuration>
```

3. Replace the text "OneSky provided API Key" with the actual key OneSky provided to you.
4. Place this config file in the root folder of your project that uses this SDK.
    * If you try to use this SDK without an API key, you will receive exceptions.
