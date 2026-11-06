*** Settings ***

Library    SeleniumLibrary

*** Variables ***
${BASE_URL}    https://photos.besoftware.pro
${LOGIN_PAGE}    ${BASE_URL}/login


*** Keywords ***

Open Chrome Browser
    [Arguments]    ${page_url}
    ${options}=    Evaluate    sys.modules['selenium.webdriver'].ChromeOptions()    sys, selenium
    Evaluate    $options.add_argument("--headless=new")
    Evaluate    $options.add_argument("--no-sandbox")
    Evaluate    $options.add_argument("--disable-dev-shm-usage")
    Open Browser    ${page_url}    chrome    options=${options}