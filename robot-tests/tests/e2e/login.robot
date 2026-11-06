*** Settings ***

Resource    ../../resources/e2e/base_page.resource
Resource    ../../resources/e2e/pages/login_page.resource
Resource    ../../resources/common_api_e2e/auth.resource


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


Too Short Username Shows Hint And Keeps Login Button Disabled
    [Documentation]    Verifies that a too short username shows an error hint and keeps the login button disabled without leaving the login page
    [Tags]    E2E    login
    [Teardown]    Close Browser

    Given The User Is On The Login Page
    When The User Enters Too Short Username
    And The User Submits Proper Length Password
    Then Username Length Hint Should Be Displayed    6-20 znaków
    And The Login Button Should Be Disabled
    And The User Should Remain On The Login Page


Too Short Password Shows Hint And Keeps Login Button Disabled
    [Documentation]    Verifies that a too short password shows an error hint and keeps the login button disabled without leaving the login page
    [Tags]    E2E    login
    [Teardown]    Close Browser

    Given The User Is On The Login Page
    When The User Enters A Proper Length Username
    And The User Submits Too Short Password
    Then The Password Length Hint Should Be Displayed    6-20 znaków
    And The Login Button Should Be Disabled
    And The User Should Remain On The Login Page


Too Long Username Shows Hint And Keeps Login Button Disabled
    [Documentation]    Verifies that a too long username shows an error hint and keeps the login button disabled without leaving the login page
    [Tags]    E2E    login
    [Teardown]    Close Browser

    Given The User Is On The Login Page
    When The User Enters Too Long Username
    And The User Submits Proper Length Password
    Then Username Length Hint Should Be Displayed    6-20 znaków
    And The Login Button Should Be Disabled
    And The User Should Remain On The Login Page


Too Long Password Shows Hint And Keeps Login Button Disabled
    [Documentation]    Verifies that a too long password shows an error hint and keeps the login button disabled without leaving the login page
    [Tags]    E2E    login
    [Teardown]    Close Browser

    Given The User Is On The Login Page
    When The User Enters A Proper Length Username
    And The User Submits Too Long Password
    Then The Password Length Hint Should Be Displayed    6-20 znaków
    And The Login Button Should Be Disabled
    And The User Should Remain On The Login Page


Successful Login Redirects Main Page And A Success Toast Should Be Displayed
    [Documentation]    Verifies that a successful login redirects the user to the main page and displays a success toast
    [Tags]    E2E    login
    [Teardown]    Log Url Teardown

    Given The User Is On The Login Page
    When The User Subbmits Valid Username And Valid Password
    Then The User Should Be Redirected To The Main Page
    And The Successful Login Toast Should Be Displayed


*** Keywords ***

The User Submits Invalid Credentials
    Input Text    ${LOGIN_INPUT_USERNAME}    ${invalid_username}
    Input Text    ${LOGIN_INPUT_PASSWORD}    ${invalid_password}
    Click Element    ${LOGIN_BUTTON_ZALOGUJ}

The Invalid Login Error Toast Should Be Displayed
    Wait Until Element Is Visible    ${LOGIN_ELEMENT_TOAST_TXT}    10s
    Element Should Contain    ${LOGIN_ELEMENT_TOAST_TXT}    Nieprawidłowe dane logowania

The User Enters A Proper Length Username
    Input Text    ${LOGIN_INPUT_USERNAME}    valid_len_username

The User Submits Too Short Password
    Input Text    ${LOGIN_INPUT_PASSWORD}    short

The Password Length Hint Should Be Displayed
    [Arguments]    ${hint_message}
    Wait Until Element Is Visible    ${LOGIN_FORM_HINT_MSG}    10s
    Element Should Contain    ${LOGIN_FORM_HINT_MSG}    ${hint_message}

The Login Button Should Be Disabled
    Element Should Be Disabled    ${LOGIN_BUTTON_ZALOGUJ}

The User Enters Too Short Username
    Input Text    ${LOGIN_INPUT_USERNAME}    short

The User Submits Proper Length Password
    Input Text    ${LOGIN_INPUT_PASSWORD}    valid_len_password    

Username Length Hint Should Be Displayed
    [Arguments]    ${hint_message}
    Wait Until Element Is Visible    css:.v-messages__message    10s
    Element Should Contain    css:.v-messages__message    ${hint_message}

The User Enters Too Long Username
    Input Text    ${LOGIN_INPUT_USERNAME}    this_is_a_very_long_username_exceeding_the_maximum_length

The User Submits Too Long Password
    Input Text    ${LOGIN_INPUT_PASSWORD}    this_is_a_very_long_password_exceeding_the_maximum_length

The User Subbmits Valid Username And Valid Password
    Input Text    ${LOGIN_INPUT_USERNAME}    ${USERNAME.value}
    Input Text    ${LOGIN_INPUT_PASSWORD}    ${PASSWORD.value}
    Click Element    ${LOGIN_BUTTON_ZALOGUJ}

The User Should Be Redirected To The Main Page
    Wait Until Location Is    ${BASE_URL}/    10s
    Location Should Be    ${BASE_URL}/

The Successful Login Toast Should Be Displayed
    Wait Until Element Is Visible    ${LOGIN_ELEMENT_TOAST_TXT}    10s
    Element Should Contain    ${LOGIN_ELEMENT_TOAST_TXT}    Zalogowano!

Log Url Teardown
    Run Keyword If Test Failed    Log Url
    Close Browser

Log Url
    [Documentation]    This keyword is used to test the current URL
    ${current_url}=    Get Location
    Log    Current URL to: ${current_url}
