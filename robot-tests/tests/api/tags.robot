*** Settings ***
Library    RequestsLibrary
Library    Collections
Library    ../../resources/api/CustomLibrary.py

Resource    ../../resources/api/auth.resource

Suite Setup     Set Bearer Token And Create API Session


*** Variables ***
# Inputs
${New-Tag-Name}    abstraction


*** Test Cases ***
Add New Tag Response Should Be 200
    [Documentation]    Adds a new photo tag using POST
    [Tags]    API    tags
    
    Delete Tag If Present    ${New-Tag-Name}

    ${params}    Create Dictionary    name=${New-Tag-Name}
    ${headers}   Create Dictionary    Content-Type=application/json
    ${response}    POST On Session   fotoblog    ${BASE_URL}${ADD_TAG_ENDPOINT}
    ...    json=${params}    headers=${headers}
    Should Be Equal As Strings    ${response.status_code}    200


Get All Tags Should Contain Relevant Status Fields And Newly Added Tag
    [Documentation]    Get tags available for the photos using GET and check if new tag is there
    [Tags]    API    tags
    ${response}    GET On Session   fotoblog    ${GET_ALL_TAGS_ENDPOINT}
    Should Be Equal As Strings    ${response.status_code}    200
    Response Should Contain Relevant Status Fields    ${response.text}
    Tag Should Be Present    ${response.text}    ${New-Tag-Name}


Delete Tag Response Should Be 200 And Tag Should Not Be Present
    [Documentation]    Deletes the newly added tag using DELETE
    [Tags]    API    tags
    ${response}    GET On Session   fotoblog    ${GET_ALL_TAGS_ENDPOINT}
    Should Be Equal As Strings    ${response.status_code}    200
    ${id}    Get Tag ID    ${response.json()['data']}    ${New-Tag-Name}
    Tag Should Be Present    ${response.text}    ${New-Tag-Name}
    Delete Tag    ${id}
    Tag Should Not Be Present    ${id}


*** Keywords ***
Response Should Contain Relevant Status Fields
    [Arguments]    ${Response-Text}
    [Documentation]    Checks if the response contains relevant status fields
    ${response}    Evaluate    json.loads('''${Response-Text}''')    modules=json
    Dictionary Should Contain Key    ${response}    errorCode
    Dictionary Should Contain Key    ${response}    status

Tag Should Be Present
    [Arguments]    ${Response-Text}    ${Tag-Name}
    [Documentation]    Checks if the response contains the newly added tag
    ${response}    Evaluate    json.loads('''${Response-Text}''')    modules=json
    ${found}    Evaluate    any(item['name'] == 'abstraction' for item in ${response['data']})
    Should Be True    ${found}

Delete Tag If Present
    [Arguments]    ${Tag-Name}
    [Documentation]    Deletes the tag if it is present in the system. We are not testing the delete functionality here, but we want to ensure that the tag is not present before adding to the system.
    ...    To remove the tag, we will first get all the tags and check if the tag is present. If it is present, we will delete it using the delete endpoint.
    ${response}    GET On Session   fotoblog    ${GET_ALL_TAGS_ENDPOINT}
    Status Should Be    200    ${response}
    ${id}    Get Tag ID    ${response.json()['data']}    ${Tag-Name}
    Run Keyword If    '${id}' != 'None'    Delete Tag    ${id}

Delete Tag
    [Arguments]    ${Tag-ID}
    [Documentation]    Deletes the tag with the given ID using DELETE
    ${response}    DELETE On Session   fotoblog    ${DELETE_TAG_ENDPOINT}/${Tag-ID}
    Status Should Be    200    ${response}
    Run Keyword If    '${response.status_code}' == '200'    Log    Deleted tag ID ${Tag-ID}. Status code: ${response.status_code}    INFO
    
Set Bearer Token And Create API Session
    [Documentation]    Creates an API session and sets the bearer token for authentication
    Login And Set Bearer Token
    Create Session    fotoblog    ${BASE_URL}    headers={"Authorization": "Bearer ${BEARER_TOKEN}"}

Tag Should Not Be Present
    [Arguments]    ${tag_id}
    [Documentation]    Checks if the response does not contain the tag with the given ID
    ${response}    GET On Session   fotoblog    ${GET_ALL_TAGS_ENDPOINT}
    Status Should Be    200    ${response}
    ${response_json}    Evaluate    json.loads('''${response.text}''')    modules=json
    ${found}    Evaluate    any(item['id'] == '${tag_id}' for item in ${response_json['data']})
    Should Not Be True    ${found}