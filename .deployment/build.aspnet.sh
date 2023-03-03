#!/bin/bash
scriptFile=`realpath $0`
directory=`dirname $scriptFile`
cd $directory


# 1 Argument: the name of the image
IMAGE_NAME=$1

# 2 Argument: the project file which will be deployed
PROJECT_FILE=$2

# 3 Argument: the name of the compiled dll file. Will be used as entrypoint
DLL_FILENAME=$3



DOCKER_FILE=Dockerfile.aspnet


echo "##vso[task.setvariable variable=IMAGE_NAME]$IMAGE_NAME"
echo "##vso[task.setvariable variable=PROJECT_FILE]$PROJECT_FILE"
echo "##vso[task.setvariable variable=DLL_FILENAME]$DLL_FILENAME"
echo
echo
echo
echo "--------------------------------------"
echo "Configuration:"
echo "    |> image:     ${IMAGE_NAME}"
echo "    |> project: ${PROJECT_FILE}"
echo "    |> exe:     ${DLL_FILENAME}"
echo "--------------------------------------"
echo
echo
echo
echo "building image '${IMAGE_NAME}' using '${directory}/${DOCKER_FILE}'"
# here we set the context to the root of the repository
docker build ../ \
    --file ${DOCKER_FILE} \
    --build-arg PROJECT_FILE=${PROJECT_FILE} \
    --build-arg DLL_FILENAME=${DLL_FILENAME} \
    --tag "${IMAGE_NAME}"