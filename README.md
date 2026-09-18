# Fotoblog

Fotoblog is a web application that allows users to publish and manage photos. Users can add photos, descriptions and tags, and browse published content.

The project was also used as a practical environment for **API test automation, test scripting, Data-Driven Testing and Continuous Integration**.

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
* Data-Driven Testing (DDT)
* Postman CLI
* GitHub Actions
* Continuous Integration (CI)

## API Test Automation

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

The API tests use **Data-Driven Testing (DDT)** to execute the same test scenarios with different sets of input data.

Test data is maintained separately from the test logic and supplied to the Postman collection during execution. This allows multiple test cases to be executed using different input combinations without duplicating the test scripts.

For example, the same API test can be executed against multiple sets of credentials or request parameters.

This approach improves test coverage and makes the automated tests easier to maintain and extend.

## Continuous Integration

The Postman API test collection is integrated with **GitHub Actions**.

The CI workflow automatically executes the API tests when changes are pushed to the repository. This provides automated feedback about the API after code changes and helps detect regressions early.

### CI workflow

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
    ▼
Postman CLI
    │
    ▼
Postman API Collection
    │
    ├── Data-Driven Testing
    │
    ├── API requests
    │
    └── Chai.js assertions
             │
             ▼
       Test results
```

Test execution is performed automatically by **Postman CLI** inside the GitHub Actions workflow.

The workflow results are available directly in GitHub Actions, making the API tests part of the project's Continuous Integration process.

## Project Structure

```text
Fotoblog/
├── .github/
│   └── workflows/       # GitHub Actions CI workflows
│
├── .postman/             # Postman configuration
├── postman/              # Postman collections and test data
│
├── Fotoblog/             # .NET application
├── Fotoblog.BLL/         # Business Logic Layer
├── Fotoblog.DAL/         # Data Access Layer
├── Fotoblog.Utils/       # Utility components
│
├── client/               # Vue.js frontend
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
* Postman scripting
* Chai.js assertions
* Data-Driven Testing


    
