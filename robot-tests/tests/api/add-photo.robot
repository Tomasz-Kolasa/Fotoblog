*** Comments ***
This test suite is for testing ${ADD_PHOTO_ENDPOINT} of the application.

NOTE:
"AllowedExtensions": [ ".jpg", ".jpeg", ".png" ]
"AllowedImgContentTypes": [ "image/jpeg", "image/png" ]


*** Settings ***
Resource   ${CURDIR}${/}..${/}..${/}resources${/}api${/}base.resource
Resource     ${CURDIR}${/}..${/}..${/}resources${/}api${/}common.resource

Suite Setup    Set Bearer Token And Create API Session
Test Teardown    Delete Test Photo By Title    ${EXAMPLE_PHOTO_TITLE}
Suite Teardown    Delete All Sessions

Test Template    It Should Be Possible To Upload Allowed Format Images


*** Variables ***
${TEST_IMAGE_JPG_PATH}    ${CURDIR}${/}..${/}..${/}test-data${/}photos${/}photo.jpg
${TEST_IMAGE_JPEG_PATH}    ${CURDIR}${/}..${/}..${/}test-data${/}photos${/}photo.jpeg
${TEST_IMAGE_PNG_PATH}    ${CURDIR}${/}..${/}..${/}test-data${/}photos${/}photo.png

${TEST_IMAGE_INVALID_CONTENT_TYPE}    ${CURDIR}${/}..${/}..${/}test-data${/}photos${/}invalid_content_type.png

${EXAMPLE_PHOTO_TITLE}          Nice item
${EXAMPLE_PHOTO_DESCRIPTION}    What a nice shot!


*** Test Cases ***        IMAGE_PATH                        CONTENT_TYPE
Jpg Image Upload          ${TEST_IMAGE_JPG_PATH}            image/jpeg
Jpeg Image Upload         ${TEST_IMAGE_JPEG_PATH}           image/jpeg
PNG Image Upload          ${TEST_IMAGE_PNG_PATH}            image/png


Upload Photo With MIME Type Mismatch Should Fail
    [Template]    NONE
    ${response}=    Upload Photo    ${TEST_IMAGE_INVALID_CONTENT_TYPE}    400    image/jpeg    ${EXAMPLE_PHOTO_TITLE}    ${EXAMPLE_PHOTO_DESCRIPTION}
    Should Be Equal As Strings    ${response.status_code}    400



*** Keywords ***
It Should Be Possible To Upload Allowed Format Images
    [Arguments]    ${img_path}    ${content_type}
    ${response}    Upload Photo    ${img_path}    200    ${content_type}    ${EXAMPLE_PHOTO_TITLE}    ${EXAMPLE_PHOTO_DESCRIPTION}
    Should Be Equal As Strings    ${response.status_code}    200
    
    