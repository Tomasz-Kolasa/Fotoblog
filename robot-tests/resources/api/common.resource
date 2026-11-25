*** Settings ***
Library    RequestsLibrary
Library    OperatingSystem
Resource    ${CURDIR}${/}..${/}common_api_e2e${/}api-urls.resource

*** Keywords ***
Delete Test Photo By Title
    [Documentation]    Delete photo by given title
    [Arguments]    ${title}

    ${response}=    GET On Session    fotoblog    ${BASE_URL}${GET_ALL_PHOTOS_ENDPOINT}
    IF    len($response.json()['data']) < 1
        RETURN
    END
    
    FOR    ${photo}    IN    @{response.json()['data']}
        IF    $photo['title'] == $title
            ${params}=    Create Dictionary    id=${photo}[id]
            DELETE On Session    fotoblog    url=${BASE_URL}${DELETE_PHOTO_ENDPOINT}    params=${params}
        END
    END


Delete Test Photo By Id
    [Documentation]    Delete photo by given Id
    [Arguments]    ${id}

    ${response}=    GET On Session    fotoblog    ${BASE_URL}${GET_ALL_PHOTOS_ENDPOINT}
    IF    len($response.json()['data']) < 1
        RETURN
    END
    
    FOR    ${photo}    IN    @{response.json()['data']}
        IF    $photo['id'] == $id
            ${params}=    Create Dictionary    id=${photo}[id]
            DELETE On Session    fotoblog    url=${BASE_URL}${DELETE_PHOTO_ENDPOINT}    params=${params}
        END
    END


Upload Photo
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


Update Photo
    [Arguments]    ${photo-object}    ${expected_status}

    ${response}=    PUT On Session    fotoblog    url=${BASE_URL}${UPDATE_PHOTO_ENDPOINT}    json=${photo-object}
    ...    expected_status=${expected_status}

    RETURN    ${response}
    


Get Photo Object By Title
    [Documentation]    This keyword returns uploaded image object (json data got by GET request) by given title or ${null} in case photo not found
    ...    Note: fotoblog-api does not return id of uploaded photo
    [Arguments]    ${title}

    ${response}=    GET On Session    fotoblog    ${BASE_URL}${GET_ALL_PHOTOS_ENDPOINT}
    IF    len($response.json()['data']) < 1
        RETURN    ${null}
    END

    ${photo_objects}=    Set Variable    ${{ [p for p in $response.json()['data'] if p['title'] == $title] }}
    
    IF    len($photo_objects) == 1
        RETURN    ${photo_objects}[0]
    ELSE
        RETURN    ${null}
    END

    