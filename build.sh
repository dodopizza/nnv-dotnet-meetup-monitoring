#!/bin/bash

docker stop ticketbroker
dotnet publish -f netcoreapp2.2 -c Release -o ../../out /m:1 ./TicketBroker
docker start ticketbroker