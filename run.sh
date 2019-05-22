#!/bin/bash
SCRIPT_DIR=`cd $(dirname $0)/;pwd`
docker rm -f ticketbroker;
docker rm -f prometheus;
docker rm -f grafana;
docker network rm my-network;

docker network create my-network

docker run -d --restart=always \
  -p 3000:3000 \
  -e "GF_SECURITY_ADMIN_PASSWORD=secret" \
  -v "${SCRIPT_DIR}/data/grafana:/var/lib/grafana" \
  --name=grafana \
  --network=my-network \
  grafana/grafana

docker run -d --restart=always \
  --net=host \
  --network=my-network \
  -v "${SCRIPT_DIR}/prometheus:/etc/prometheus/" \
  -v "${SCRIPT_DIR}/data/prometheus:/prometheus" \
  --name prometheus \
  -p 9090:9090 \
  prom/prometheus:latest \
  --config.file=/etc/prometheus/prometheus.yml \
  --storage.tsdb.retention=14d 

docker run -d \
  -v "${SCRIPT_DIR}/out:/app" \
  -p 8081:80 \
  --network=my-network \
  --name ticketbroker \
  microsoft/dotnet:2.2.1-aspnetcore-runtime-alpine3.8 \
  dotnet /app/TicketBroker.dll
