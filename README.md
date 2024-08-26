# Assignment: CRUD API for Product Management

Welcome to the CRUD API for Product Management project! In this endeavor, we’ve developed a robust API that allows you to create, retrieve, update, and delete product information seamlessly. Whether you’re building an e-commerce platform, inventory management system, or any application that deals with products, this API provides essential functionality to handle your product data efficiently.

Our project leverages Entity Framework Code First, ensuring a smooth database setup. Upon execution, it automatically creates a database named “Assignment” and initializes a “Product” table with dummy data. As you explore the endpoints and schema, you’ll find a straightforward yet powerful solution for managing your product inventory. Let’s dive into the details! 🚀📦

## Endpoints

### GET

1. **Get all products**
   - **Endpoint:** `/api/products`
   - **Description:** Retrieves a list of all products in the database.
   - **Example:** `GET /api/products`

2. **Get product by ID**
   - **Endpoint:** `/api/products/{id}`
   - **Description:** Retrieves a specific product based on its unique `ProductId`.
   - **Example:** `GET /api/products/100001`

### POST

3. **Create a new product**
   - **Endpoint:** `/api/products`
   - **Description:** Adds a new product to the database.
   - **Example Request Body:**
     ```json
     {
         "ProductName": "New Product",
         "Category": "Electronics",
         "Stock": 50,
         "Description": "A cutting-edge gadget",
         "Createdon": "2024-08-27T10:00:00"
     }
     ```

### DELETE

4. **Delete product by ID**
   - **Endpoint:** `/api/products/{id}`
   - **Description:** Deletes the product with the specified `ProductId`.
   - **Example:** `DELETE /api/products/100001`

### PUT

5. **Update product by ID**
   - **Endpoint:** `/api/products/{id}`
   - **Description:** Updates the details of an existing product.
   - **Example Request Body (to update stock):** `/api/Product/100001`
     ```json
     {
      "ProductName": "chair",
      "Category": "furniture",
      "Stock": 80,
      "Description": "4 wheel black height adjustable chair",
      "Createdon": "2024-08-26T20:58:13.076Z"
     }
     ```

6. **Decrease stock of a product**
   - **Endpoint:** `/api/products/decrement-stock/{id}/{quantity}`
   - **Description:** Decreases the stock of a specific product by the given quantity.
   - **Example:** `PUT /api/products/decrement-stock/100001/5`

7. **Increase stock of a product**
   - **Endpoint:** `/api/products/add-to-stock/{id}/{quantity}`
   - **Description:** Increases the stock of a specific product by the given quantity.
   - **Example:** `PUT /api/products/add-to-stock/100001/10`

## Schema

#### Product
```csharp
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public DateTime? Createdon { get; set; }
}
```

## Database

This project is using sql server as database.
In `Appsettings.json` file `sqlConnection` key, I am using LocalDb as our test database. On first time project execution, it will create a database called `Assignment` and one table `Product` with above schema. It seeds the table with 2 dummy records.
<p>Product table ProductId column is 6 digit int starting from `100000` and incrementing by 1.
I have used Entity Framework Code First approach to generate table and perform CRUD operations.
All Database operations (except `Delete` endpoint, which is synchronous as only 1 row is affected)</p>

## Unit Tests

Using `xunit` framework and `moq`, unit tests are written for 5 out of 7 endpoints.

## Steps to Run Code locally
Clone the repo locally
```sh
git clone https://github.com/iamsarveshydv/Assignment.git
```

Change to cloned repo
```sh
cd Assignment/Carl_Assignment
```

Execute the project 
```sh
dotnet run
```

project will build and execute `localhost:5001` and `localhost:5000`<br>
open the url `https://localhost:5000/swagger/index.html`
