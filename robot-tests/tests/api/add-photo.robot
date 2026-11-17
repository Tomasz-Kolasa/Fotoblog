*** Comments ***
This test suite is for testing the upload photo functionality of the application.

NOTE:
"AllowedExtensions": [ ".jpg", ".jpeg", ".png" ]
"AllowedImgContentTypes": [ "image/jpeg", "image/png" ]


*** Settings ***
Library    OperatingSystem
Resource   ..${/}resources${/}api${/}base.resource

Suite Setup    Set Bearer Token And Create API Session
Suite Teardown    Clean Up Test Suit Activities

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


Upload Image With MIME Type Mismatch Should Fail
    [Template]    NONE
    ${response}=    Upload Image    ${TEST_IMAGE_INVALID_CONTENT_TYPE}    400    image/jpeg    ${EXAMPLE_PHOTO_TITLE}    ${EXAMPLE_PHOTO_DESCRIPTION}
    Should Be Equal As Strings    ${response.status_code}    400



*** Keywords ***
Upload Image
    [Arguments]    ${img_path}    ${expected_status}    ${content_type}    ${title}    ${description}
    
    ${file_obj}=    Get File For Streaming Upload    ${img_path}

    ${_}    ${file_name}=    Split Path    ${img_path}
    ${file_tuple}=    Evaluate    ($file_name, $file_obj, $content_type)
    ${files}=    Create Dictionary    File=${file_tuple}

    ${data}    Create Dictionary     Title=${title}    Description=${description}

    ${response}    POST On Session    fotoblog    ${BASE_URL}${ADD_PHOTO_ENDPOINT}
    ...    data=${data}    files=${files}
    ...    expected_status=${expected_status}

    RETURN    ${response}


It Should Be Possible To Upload Allowed Format Images
    [Arguments]    ${img_path}    ${content_type}
    ${response}    Upload Image    ${img_path}    200    ${content_type}    ${EXAMPLE_PHOTO_TITLE}    ${EXAMPLE_PHOTO_DESCRIPTION}
    Should Be Equal As Strings    ${response.status_code}    200


Delete Uploaded Test Photos
    [Documentation]    All photos uploaded are deleted by title \${EXAMPLE_PHOTO_TITLE}

    ${response}=    GET On Session    fotoblog    ${BASE_URL}${GET_ALL_PHOTOS_ENDPOINT}
    IF    len($response.json()['data']) < 1
        RETURN
    END
    
    FOR    ${photo}    IN    @{response.json()['data']}
        IF    $photo['title'] == $EXAMPLE_PHOTO_TITLE
            ${params}=    Create Dictionary    id=${photo}[id]
            DELETE On Session    fotoblog    url=${BASE_URL}${DELETE_PHOTO_ENDPOINT}    params=${params}
        END
    END

Clean Up Test Suit Activities
    Delete Uploaded Test Photos
    Delete All Sessions
    