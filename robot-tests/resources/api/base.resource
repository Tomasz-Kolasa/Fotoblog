*** Settings ***

Library    RequestsLibrary
Resource    ../common_api_e2e/api-urls.resource
Resource    ../common_api_e2e/auth.resource


*** Keywords ***

Login And Set Bearer Token
    [Documentation]    Logs in to the API and retrieves the bearer token for authentication

    ${params}    Create Dictionary    userName=${USERNAME.value}    password=${PASSWORD.value}
    ${headers}   Create Dictionary    Content-Type=application/json

    ${response}    POST    ${BASE_URL}${LOGIN_ENDPOINT}    json=${params}    headers=${headers}

    Status Should Be    200    ${response}

    VAR    ${token}    ${response.json()['data']}
    Should Not Be Empty   ${token}    The bearer token should not be empty after login

    Set Suite Variable    ${BEARER_TOKEN}    ${token}