---
page_type: sample
languages:
- csharp
products:
- dotnet
description: "A sample project to show Event Grid module on Azure IoT Edge"
urlFragment: "using-event-grid-publish"
---

# Use publish/subscriber messaging pattern on edge using Event Grid

This quick start demonstrates how to use publish/subscriber messaging pattern on edge using Event Grid. 3 modules are deployed as part of this quick start.

1. Event Grid module - enables publish/subscriber messaging pattern on edge
1. Publisher module  - responsible for publishing events
1. Subscriber module - responsible for receiving/handling events

Additionally with an Event Grid subscription to 'edgeHub' we can forward events to IoTHub via routes. Refer to Deployment template for details.

## Event Grid module

The module's device twin is used to create the topic and subscription used by the publisher and subscriber module. For simplicity, listens on HTTP and forwards to HTTP endpoint (Azure). Capable to restricting to only HTTPs  on both incoming and outgoing. The URL depends on the module name. If deploying with a different name make sure to update the publisher module.

## Publisher module

Responsible for publishing events to a topic with name 'quickstarttopic'. The topic is created in Event Grid module's device twin. Assumes Event Grid module is deployed with name 'eventgridmodule'. If done differently make sure to update the code appropriately.

## Subscriber module

Azure function is used to demonstrate how to write an event handler. The handler URL is registered as part of subscription creation. Any changes to the function URL needs to modify Event Grid device twin configuration as well.

## How-to: Install extension and deploy

1. Open the top folder in VSCode (need Azure IoT Edge Extension)
2. create or open a .env file and fill in the values (REGISTRY,USERNAME,PASSWORD)
3. Now, you can build, push, deploy the solution

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
