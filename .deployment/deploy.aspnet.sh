#!/bin/bash
scriptFile=`realpath $0`
directory=`dirname $scriptFile`
cd $directory


# 1 Argument: The domain under which this aspnet piece will be deployed
DOMAIN=$1

# 2 Argument: the name of the image
IMAGE_NAME=$2

# 3 Argument: the name of container
CONTAINER_NAME=$3

# 4 Argument: the middlewares which should be used
TRAEFIK_MIDDLEWARE=$4

# 5 Argument: provisioning commands
PROVISIONING_COMMANDS=$5

# 6 Argument: additional run args which will be passed to the docker run instruction
DOCKER_RUN_ARGS=$6


CONTAINER_NAME=$(echo "$CONTAINER_NAME" | awk '{print tolower($0)}')

TRAEFIK_ROUTER_NAME="${CONTAINER_NAME//./"-"}-router"


DOCKER_NETWORK=bridge_l1dmz




echo .
echo .
echo .
echo "--------------------------------------"
echo "Configuration:"
echo "    |> domain:      ${DOMAIN}"
echo "    |> middlewares: ${TRAEFIK_MIDDLEWARE}"
echo
echo "    |> image:     ${IMAGE_NAME}"
echo "    |> container: ${CONTAINER_NAME}"
echo "--------------------------------------"
echo .
echo .
echo .
echo "stopping and deleting container '${CONTAINER_NAME}'"
docker stop $CONTAINER_NAME || true && docker rm $CONTAINER_NAME || true



echo .
echo .
echo .
echo "Provisioning"
if [ -z "${PROVISIONING_COMMANDS// }" ]; then
    echo "no provisioning provided"
else
    $PROVISIONING_COMMANDS
fi




if [ -z "${TRAEFIK_MIDDLEWARE// }" ]; then
    TRAEFIK_MIDDLEWARE=
else
    TRAEFIK_MIDDLEWARE="--label "traefik.http.routers.${TRAEFIK_ROUTER_NAME}.middlewares=${TRAEFIK_MIDDLEWARE}""
fi




echo .
echo .
echo .
echo "starting '$CONTAINER_NAME' with domain '${DOMAIN}'"
docker run \
    --detach \
    --restart=always \
    --network=${DOCKER_NETWORK} \
    --name "${CONTAINER_NAME}" \
    --label "traefik.enable=true" \
    --label "traefik.http.routers.${TRAEFIK_ROUTER_NAME}.rule=Host(\`${DOMAIN}\`)" \
    --label "traefik.http.routers.${TRAEFIK_ROUTER_NAME}.entrypoints=https-entrypoint" \
    --label "traefik.http.routers.${TRAEFIK_ROUTER_NAME}.tls.certresolver=myresolver" \
    ${TRAEFIK_MIDDLEWARE} \
    ${DOCKER_RUN_ARGS} \
    ${IMAGE_NAME}