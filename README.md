# Developer Evaluation Project

`READ CAREFULLY`

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)

## Instructions to Run and Test

### 1. Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.
- .NET 8 SDK (optional, only if you want to run it outside Docker).

### 2. Execution (The Easy Way)
The project is fully containerized and uses `docker-compose` to spin up the Web API alongside its dependencies (PostgreSQL, MongoDB, and Redis).
The database configuration is completely automated, including entity framework migrations being applied automatically on startup.

1. Open your terminal at the `template/backend` folder.
2. Run the following command:
   ```bash
   docker-compose up -d --build
   ```
3. Wait a few seconds for the containers to build and start. The API will be available at port `8080`.

### 3. Testing the Application
Once the containers are running, you can interact with the API using Swagger:

- **Swagger UI**: Access [http://localhost:8080/swagger](http://localhost:8080/swagger) in your browser.

#### How to test the Sales API:
1. **Create a Sale** (`POST /api/Sales`): 
   Use the provided endpoints to create a sale. Test the business rules by sending different item quantities (e.g., `< 4`, `5`, `15`, `25`). You will observe that the API automatically calculates the 10% and 20% discounts or blocks the request (400 Bad Request) if the 20-item limit is exceeded.
2. **Retrieve a Sale** (`GET /api/Sales/{id}`): Check the created sale and its items.
3. **Domain Events**: You can check the Docker console logs (`docker logs ambev_developer_evaluation_webapi`) to see the CQRS Domain Events being published (e.g., `SaleCreatedEvent`, `SaleModifiedEvent`, `SaleCancelledEvent`, `ItemCancelledEvent`).

#### How to test the Auth API:
1. First, create a new user via `POST /api/Users`. Make sure to pass `status: 1` (Active) and `role: 3` (Admin).
2. Second, use the registered email and password on `POST /api/Auth` to receive a JWT token.
