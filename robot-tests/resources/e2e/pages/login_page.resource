*** Settings ***

Resource    ../base_page.resource


*** Variables ***

${LOGIN_INPUT_USERNAME}    css:input[placeholder="login..."]
${LOGIN_INPUT_PASSWORD}    css:input[placeholder="hasło..."]
${LOGIN_BUTTON_ZALOGUJ}    xpath://button[normalize-space(.)="zaloguj"]
${LOGIN_ELEMENT_TOAST_TXT}    css:.v-toast__text
${LOGIN_FORM_HINT_MSG}    css:.v-messages__message


*** Keywords ***

The User Is On The Login Page
    Open Chrome Browser    ${LOGIN_PAGE}

The User Should Remain On The Login Page
    Location Should Be    ${LOGIN_PAGE}