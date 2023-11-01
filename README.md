# Rebar
System for selling shakes

## Introduction

RebarAPI is a robust .NET 6 Web API built for a fictional rebar store, facilitating orders for shakes, managing the shake menu, and handling daily checkout operations. The API leverages MongoDB as its primary data store and provides CRUD operations on various entities such as shakes, orders, and more.

## Features

- **CRUD Operations**: Manage shakes and orders with comprehensive Create, Read, Update, and Delete operations.
- **Database Integration**: The application seamlessly connects to MongoDB, providing persistent storage for your data.
- **Swagger Integration**: Easily test your API endpoints using the integrated Swagger UI.
- **Exception Handling**: Development mode comes with built-in exception handling for easier debugging.
- **Secure Checkout**: End-of-day checkout requires manager authorization for increased security.

## Setup

1. **Database Configuration**: Ensure MongoDB is running locally on the default port (27017). The API is configured to use a database named `RebarDB`.

2. **Running the API**:
   - Clone the repository.
   - Navigate to the project directory and run `dotnet build` followed by `dotnet run`.

3. **Swagger UI**: Once the API is running, navigate to `https://localhost:7162/swagger` to access the Swagger UI for the API.

## Endpoints

1. **MenuController**:
    - **GET** `/api/menu`: Retrieve the full list of shakes from the menu.
    - **GET** `/api/menu/{id}`: Retrieve details of a specific shake using its ID.
    - **POST** `/api/menu`: Add a new shake to the menu.
    - **PUT** `/api/menu/{id}`: Update details of a specific shake.
    - **DELETE** `/api/menu/{id}`: Remove a shake from the menu using its ID.

2. **OrdersController**:
    - **GET** `/api/orders`: Fetch all orders.
    - **GET** `/api/orders/{id}`: Retrieve a specific order using its ID.
    - **POST** `/api/orders`: Place a new order.

3. **CheckoutController**:
    - **POST** `/api/checkout/close`: Close the daily checkout. Requires a manager password.
    - **GET** `/api/checkout/report`: Retrieve a daily report showing all orders placed on the current day.

## Data Models

1. **Shake**:
    - Id (Guid)
    - Name (string)
    - Description (string)
    - PriceL (decimal)
    - PriceM (decimal)
    - PriceS (decimal)

2. **OrderItem**:
    - Shake (Shake object)
    - Size (string)
    - Price (decimal)

3. **Order**:
    - Id (Guid)
    - Items (List of OrderItem)
    - TotalPrice (decimal)
    - CustomerName (string)
    - OrderDate (DateTime)
    - Discounts (List of Discount)

4. **Discount**:
    - Name (string)
    - Percentage (decimal)

## Future Improvements

- UI interface, what's the point of all this business if we don't have a nice UI that the soldiers can order through?🥤🥃🍹🧃
- Implement authentication and authorization mechanisms to restrict access to certain operations.
- Extend the data model to include more entities like customer profiles, inventory management, etc.
- Implement proper logging and monitoring mechanisms.
- Add data validation to ensure data integrity.
- Implement error handling for more graceful failure modes.

## Conclusion

RebarAPI offers a comprehensive set of features for managing a fictional rebar store. The API design ensures scalability and extensibility, allowing new features and integrations to be added with minimal effort.
