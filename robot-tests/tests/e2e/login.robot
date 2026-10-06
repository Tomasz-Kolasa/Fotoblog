*** Settings ***

Resource    ../../resources/variables/e2e_urls.resource
Resource    ../../resources/e2e/keywords.resource


*** Variables ***

${invalid_username}    invalid_user
${invalid_password}    invalid_pass


*** Test Cases ***

Invalid Login Shows Error And Keeps User On Login Page
    [Documentation]    Verifies invalid credentials show an error toast without leaving the login page
    [Tags]    E2E    login
    [Teardown]    Close Browser

    Given The User Is On The Login Page
    When The User Submits Invalid Credentials
    Then The Invalid Login Error Toast Should Be Displayed
    And The User Should Remain On The Login Page


*** Keywords ***

The User Is On The Login Page
    Open Chrome Browser    ${LOGIN_PAGE}

The User Submits Invalid Credentials
    Input Text    css:input[placeholder="login..."]    ${invalid_username}
    Input Text    css:input[placeholder="hasło..."]    ${invalid_password}
    Click Element    xpath://button[normalize-space(.)="zaloguj"]

The Invalid Login Error Toast Should Be Displayed
    Wait Until Element Is Visible    css:.v-toast__text    10s
    Element Should Contain    css:.v-toast__text    Nieprawidłowe dane logowania

The User Should Remain On The Login Page
    Location Should Be    ${LOGIN_PAGE}