*** Comments ***

This test suite is for testing ${UPDATE_PHOTO_ENDPOINT} of the application.

Test suit notes:
${TEST_PHOTO_OBJECT} contains holds data of the test photo created via POST request upon Test Setup

*** Settings ***

Library    OperatingSystem
Resource   ${CURDIR}${/}..${/}..${/}resources${/}api${/}base.resource
Resource     ${CURDIR}${/}..${/}..${/}resources${/}api${/}common.resource

Suite Setup    Set Bearer Token And Create API Session
Test Setup    Prepare Test Photo Data
Test Teardown    Delete Test Photo Data
Suite Teardown    Delete All Sessions


*** Variables ***

${TEST_IMAGE_JPG_PATH}    ${CURDIR}${/}..${/}..${/}test-data${/}photos${/}photo.jpg

${TEST_PHOTO_OBJECT}    ${none}

${TEST_PHOTO_TITLE}          Update Title
${TEST_PHOTO_DESCRIPTION}    Update Description
${content_type}    image/jpeg



*** Test Cases ***

Photo Title and Description Update
    Log    Test photo object: ${TEST_PHOTO_OBJECT}
    ${TEST_PHOTO_OBJECT}[title]=    Set Variable    Title Updated
    ${TEST_PHOTO_OBJECT}[description]=    Set Variable    Description Updated
    ${response}=    Update Photo    ${TEST_PHOTO_OBJECT}    200
    Log    ${response.json()}
    Should Be True    ${response.json()['status']}
    Should Be Equal    ${response.json()['errorCode']}    ${none}


Photo Title Update With No Description
    Log    Test photo object: ${TEST_PHOTO_OBJECT}
    ${TEST_PHOTO_OBJECT}[title]=    Set Variable    Title Updated
    ${TEST_PHOTO_OBJECT}[description]=    Set Variable    ${EMPTY}
    ${response}=    Update Photo    ${TEST_PHOTO_OBJECT}    200
    Log    ${response.json()}
    Should Be True    ${response.json()['status']}
    Should Be Equal    ${response.json()['errorCode']}    ${none}

Photo Update With Too Long Title
    Log    Test photo object: ${TEST_PHOTO_OBJECT}
    ${TEST_PHOTO_OBJECT}[title]=    Set Variable    Title Updated With Too Long Text
    ${response}=    Update Photo    ${TEST_PHOTO_OBJECT}    400
    Log    ${response.json()}
    Should Be Equal As Strings    ${response.json()['title']}    One or more validation errors occurred.

Photo Update With Too Short Title
    Log    Test photo object: ${TEST_PHOTO_OBJECT}
    ${TEST_PHOTO_OBJECT}[title]=    Set Variable    A
    ${response}=    Update Photo    ${TEST_PHOTO_OBJECT}    400
    Log    ${response.json()}
    Should Be Equal As Strings    ${response.json()['title']}    One or more validation errors occurred.

Photo Update With Too Long Description
    Log    Test photo object: ${TEST_PHOTO_OBJECT}
    ${TEST_PHOTO_OBJECT}[title]=    Set Variable    Title Updated
    ${TEST_PHOTO_OBJECT}[description]=    Set Variable    Description Updated With Too Long Text Should Be Not Accepted By API
    ${response}=    Update Photo    ${TEST_PHOTO_OBJECT}    400
    Log    ${response.json()}
    Should Be Equal As Strings    ${response.json()['title']}    One or more validation errors occurred.


*** Keywords ***

Prepare Test Photo Data
    [Documentation]    This Test Setup uploads the test photo and stores it's data in test variable
    
    ${response}    Upload Photo    ${TEST_IMAGE_JPG_PATH}    200    image/jpeg    ${TEST_PHOTO_TITLE}    ${TEST_PHOTO_DESCRIPTION}
    Should Be Equal As Strings    ${response.status_code}    200
    ${obj}=    Get Photo Object By Title    ${TEST_PHOTO_TITLE}
    Set Suite Variable    ${TEST_PHOTO_OBJECT}    ${obj}
    Should Not Be Equal    ${TEST_PHOTO_OBJECT}    ${none}    Test photo could not be loaded


Delete Test Photo Data
    Delete Test Photo By Id    ${TEST_PHOTO_OBJECT}[id]
    ${TEST_PHOTO_OBJECT}=    Set Variable    ${none}
    