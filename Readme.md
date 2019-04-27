#Vehicle Plot Delivery

##Prerequisites:
- rabbitmq
- redis

**I used the following commands to install RabbitMQ and redis: (Docker installation required)**
```
docker run -d --hostname rabbitmq --name rabbitmq rabbitmq:3-management
```
and
```
docker run --name redis -d redis
```

##Configuration##
The following projects contains appsettings.json for configuring the dependencies.
- QueryServiceWeb
- SenderClient
- ReceiverClient

###Setting###
- "redisConnectionString": URI for redis connection. Default value is "localhost".
- "rabbitMQUri": RabbitMQ URI. Default value is "amqp://localhost".
- "queue": Queue name used for storing vehicle plots in RabbitMQ. Default value is "vehicle_plot".

##Running Applications##

###Sender Client###
To run sender client application:
```
dotnet run --vid <vehicle id> [--interval <interval in milliseconds>]
```
e.g:
```
dotnet run --vid 1 --interval 1000
```

###Receiver Client###
**To run receiver client as a console application:**
```
dotnet run --console
```

**To run receiver client as a service:**
1. Publish ReceiverClient.
2. Run the following to install as service:
```
sc create <Service Name> binPath="<Publish path to ReceiverClient.exe>"
```
e.g.:
```
sc create VehiclePlotReceiver binPath="C:\Projects\VehiclePlotDelivery\Vehicle Plot Delivery\ReceiverClient\bin\Release\netcoreapp2.2\publish\ReceiverClient.exe"
```
3. Start/Stop the service via services.msc

###Query Service###
**To run query service as a console applicatioon:**
```
dotnet run --console
```

**To run receiver client as a service:**
```
sc create <Name of Service> binPath=<Publish path of QueryService>
```
e.g:
```
sc create VehiclePlotQueryService binPath="C:\Projects\VehiclePlotDelivery\Vehicle Plot Delivery\QueryService\bin\Debug\netcoreapp2.2\win-x64\publish\QueryServiceWeb.exe"
```

**To remove query service from service:**
```
sc delete <Name of Service>
```
eg:
```
sc delete VehiclePlotQueryService
```

##Tests##
Unit Test and Load Tests are located on Tests solution folder. 
Unit tests uses xUnit as test framework and Moq for mocking.
Query Service Load tests is a console application for testing Query Service latency and avg request / sec.
I used RabbitMQ management for getting average message handled / sec. Please see the **Published and Consumed messages per second.png** for the report.