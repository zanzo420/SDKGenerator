#!/bin/bash

# USAGE: unity_RunAutoTests.bat [<UNITY_VERSION>] [<SdkName>]

if [ -z $1 ]; then
    UNITY_VERSION=$UNITY_PUBLISH_VERSION
else
    UNITY_VERSION=$1
fi
if [ -z $2 ]; then
    SdkName=UnitySDK
else
    SdkName=$2
fi

RepoProject="${WORKSPACE}/sdks/${SdkName}/ExampleTestProject"
BuildIdentifier=JBuild_${SdkName}_${VerticalName}_${NODE_NAME}_${EXECUTOR_NUMBER}

RunClientJenkernaught() {
    echo === Build OSX Client Target ===
    pushd "${RepoProject}"
    $UNITY_VERSION -projectPath "${RepoProject}" -quit -batchmode -executeMethod PlayFab.Internal.PlayFabPackager.MakeOsxBuild -logFile "${WORKSPACE}/logs/buildOSXClient.txt" || (cat "${WORKSPACE}/logs/buildOSXClient.txt" && return 1)
    output=$?
    popd
    return output
}

DoWork() {
    . ./unity_copyTestTitleData.sh "${RepoProject}/Assets/Resources" copy || exit 1
    RunClientJenkernaught
}

DoWork
