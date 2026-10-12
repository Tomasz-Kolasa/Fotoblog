# Fotoblog

Fotoblog is a web application that allows users to publish and manage photos. Users can add photos, descriptions and tags, and browse published content.

The project was also used as a practical environment for **API test automation, End-to-End test automation, Data-Driven Testing, Behavior-Driven Development and Continuous Integration**.

## Technology Stack

### Frontend

* Vue.js 2.7
* Vuetify

### Backend

* .NET 6.0
* REST API

### Testing & CI

* Postman
* JavaScript test scripts
* Chai.js assertions
* Robot Framework
* RequestsLibrary
* SeleniumLibrary
* Data-Driven Testing (DDT)
* Behavior-Driven Development (BDD)
* Postman CLI
* GitHub Actions
* Continuous Integration (CI)

## API Test Automation

The project includes automated API tests implemented using both **Postman** and **Robot Framework**.

### Postman API Tests

The project includes a dedicated Postman collection containing automated API tests for the Fotoblog backend.

The tests cover positive and negative scenarios and validate different aspects of HTTP responses, including:

* HTTP status codes
* response body structure and content
* returned data
* response headers
* validation of API error responses
* response data types
* relationships between requests and returned data

Tests are implemented using **JavaScript in Postman** and use **Chai.js BDD-style assertions** through Postman's `pm.expect` and `pm.response` APIs.

### Data-Driven Testing

The Postman API tests use **Data-Driven Testing (DDT)** to execute the same test scenarios with different sets of input data.

Test data is maintained separately from the test logic and supplied to the Postman collection during execution. This allows multiple test cases to be executed using different input combinations without duplicating the test scripts.

For example, the same API test can be executed against multiple sets of credentials or request parameters.

This approach improves test coverage and makes the automated tests easier to maintain and extend.

### Robot Framework API Tests

The project also includes API tests implemented using **Robot Framework** and **RequestsLibrary**.

The Robot Framework API tests cover API functionality including authentication and endpoint validation. The tests verify:

* HTTP status codes
* response data
* expected API behaviour
* positive and negative scenarios
* authenticated API requests

The tests use reusable **Robot Framework resource files and custom keywords** to separate test scenarios from common API operations and configuration.

Authentication credentials are provided through **environment variables** rather than being stored directly in the test source code.

## End-to-End Test Automation

The project includes **End-to-End (E2E) tests implemented with Robot Framework and SeleniumLibrary**.

The E2E tests interact with the Fotoblog web application through a real browser and verify complete user workflows from the user's perspective.

The tests cover scenarios including:

* user authentication
* navigation through the application
* interaction with web UI elements
* adding and managing application data
* validation of application behaviour

The test scenarios follow **Behavior-Driven Development (BDD)** principles.

Reusable Robot Framework keywords are used to separate test scenarios from implementation details and improve test readability and maintainability.

## Continuous Integration

The automated tests are integrated with **GitHub Actions**.

The CI workflow executes automated tests when changes are pushed to selected branches and during pull requests. This provides automated feedback after code changes and helps detect regressions early.

The CI workflow includes:

* Postman API tests
* Robot Framework API tests
* Robot Framework E2E tests

Sensitive authentication data is provided through **GitHub Actions secrets and environment variables** and is not stored in the repository.

### CI Workflow

```text
Developer
    │
    ▼
Git commit / push
    │
    ▼
GitHub Repository
    │
    ▼
GitHub Actions
    │
    ├───────────────────────────────┐
    │                               │
    ▼                               ▼
Postman CLI                  Robot Framework
    │                               │
    ▼                       ┌───────┴────────┐
Postman API                 │                │
Collection                  ▼                ▼
    │                   API Tests        E2E Tests
    │                RequestsLibrary  SeleniumLibrary
    │                       │                │
    └───────────────┬───────┴────────────────┘
                    ▼
               Test Results
                    │
                    ▼
          GitHub Actions Artifacts
```

Postman API tests are executed using **Postman CLI**, while Robot Framework API and E2E tests are executed directly in the GitHub Actions workflow.

Robot Framework generates detailed **HTML reports and execution logs**, which are uploaded as GitHub Actions artifacts for later inspection.

## Project Structure

```text
Fotoblog/
├── .github/
│   └── workflows/
│       └── automated-tests.yml       # GitHub Actions CI workflow
│
├── .postman/
│   └── ...                           # Postman configuration
│
├── postman/
│   ├── collections/                  # Postman API collections
│   └── test_data.json                # Data-Driven Testing data
│
├── robot-tests/
│   ├── tests/
│   │   ├── api/                      # Robot Framework API tests
│   │   └── e2e/                      # Robot Framework E2E tests
│   │
│   ├── resources/
│   │   ├── api/                      # API resources and keywords
│   │   └── e2e/                      # E2E resources and keywords
│   │
│   ├── pyproject.toml                # Python / Poetry configuration
│   └── ...
│
├── Fotoblog/                         # .NET application
├── Fotoblog.BLL/                     # Business Logic Layer
├── Fotoblog.DAL/                     # Data Access Layer
├── Fotoblog.Utils/                   # Utility components
│
├── client/                            # Vue.js frontend
│
└── Fotoblog.sln
```

## Programming

The project also demonstrates practical programming experience across multiple technologies.

### Backend

* C#
* .NET 6.0
* REST API

### Frontend

* JavaScript
* Vue.js 2.7
* Vuetify

### Test Automation

* JavaScript
* Python
* Robot Framework
* Postman scripting
* RequestsLibrary
* SeleniumLibrary
* Chai.js assertions
* API test automation
* End-to-End test automation
* Data-Driven Testing
* Behavior-Driven Development
