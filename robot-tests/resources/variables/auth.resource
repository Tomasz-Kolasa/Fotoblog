*** Variables ***

${USERNAME: Secret}    %{VALID_USER_NAME}
${PASSWORD: Secret}    %{VALID_USER_PASSWORD}

${BEARER_TOKEN}    ${EMPTY}