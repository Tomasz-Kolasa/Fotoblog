*** Settings ***

Library    RequestsLibrary
Resource    ../variables/api-urls.resource


*** Variables ***
${USERNAME: Secret}    %{VALID_USER_NAME}
${PASSWORD: Secret}    %{VALID_USER_PASSWORD}

${BEARER_TOKEN}    ${EMPTY}

*** Keywords ***

Login And Set Bearer Token
    [Documentation]    Logs in to the API and retrieves the bearer token for authentication

    ${params}    Create Dictionary    userName=${USERNAME.value}    password=${PASSWORD.value}
    ${headers}   Create Dictionary    Content-Type=application/json

    ${response}    POST    ${BASE_URL}${LOGIN_ENDPOINT}    json=${params}    headers=${headers}

    Status Should Be    200    ${response}

    ${token}        Set Variable    ${response.json()['data']}
    Should Not Be Empty   ${token}    The bearer token should not be empty after login

    Set Suite Variable    ${BEARER_TOKEN}    ${token}