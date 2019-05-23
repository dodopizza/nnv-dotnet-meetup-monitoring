SCRIPT_DIR=`cd $(dirname $0)/;pwd`
docker rm -f prometheus;

docker run -d --restart=always \
  --net=host \
  -p 9090:9090 \
  -v "${SCRIPT_DIR}/prometheus:/etc/prometheus/" \
  -v "${SCRIPT_DIR}/data/prometheus:/prometheus" \
  --name prometheus \
  prom/prometheus:latest \
  --config.file=/etc/prometheus/prometheus.yml \
  --storage.tsdb.retention=14d