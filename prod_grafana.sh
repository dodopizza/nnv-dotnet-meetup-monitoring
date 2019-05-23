SCRIPT_DIR=`cd $(dirname $0)/;pwd`
docker rm -f prometheus;

docker run -d --restart=always \
  --net=host \
  -p 3000:3000 \
  -v "${SCRIPT_DIR}/data/grafana:/var/lib/grafana" \
  -e "GF_SECURITY_ADMIN_PASSWORD=secret" \
  --name=grafana \
  grafana/grafana