# BitcubeDevTask
The code contained in this repository is for a developer assessment task. It is written in C# using SQLite and follows a Domain-Driven Design (DDD) approach. This project showcases an API that supports user and product management, enabling users to create accounts, manage products, and perform checkout operations.

Key Features
User Management: Includes user registration and authentication using ASP.NET Core Identity, with a focus on security and best practices.

Product Management: Provides APIs to add, modify, delete, and list products. Each product is associated with a specific user to ensure proper ownership and access control.

Checkout Process: Implements a streamlined checkout experience, allowing users to enter product IDs and quantities, and provides clear error handling for invalid requests.

Data Storage: Utilizes SQLite for persistent storage, ensuring data integrity and ease of use.

API Documentation: Integrated with Swagger for interactive API documentation, making it easy for developers to explore and test endpoints.
# Bitcube Developer Task Solution

## Overview

This project is a .NET Core application that implements a product management system with a checkout feature. It uses a microservices architecture, with a focus on domain-driven design principles. The application supports adding and removing products in a shopping cart and manages user checkouts.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Building & Running the Application](#building-and-running-the-application)

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)

## Setup Instructions

1. **Clone the Repository**

   Open your terminal or command prompt and run the following command:

   ```bash
   git clone https://github.com/PatrickAsanzi/PatrickBitcubeDevTaskSolution.git
   
2. **Restore NuGet Packages**
Restore the required NuGet packages by running:
dotnet restore BitcubeDeveloperTaskSolution.sln

## Building and Running the Application

3. **Build Docker Compose Image**

* In the Solution Explorer, locate the docker-compose project.

* Right-click on the solution in the Solution Explorer and select Set Startup Projects.

* Make sure that Docker is running on your machine. You can check this by looking for the Docker icon in your system tray.

* Select Build Solution or press Ctrl + Shift + B. This will trigger the Docker Compose build process.

* Run the Docker containers defined in your Docker Compose file:
Click the Run button (green arrow) in Visual Studio, or press F5 to start debugging.
This will start the Docker containers, and you can view the output in the Output window.

* Once the containers are running, you can access your application through the port specified in the Docker Compose file. For example, if your application runs on port 5000, you would access it at http://localhost:5000

## Using the API Online
You can access the API documentation and test the endpoints using the following link: 
(https://bitcubedevtaskappcontainer-bxehfkbpduhkd7au.southafricanorth-01.azurewebsites.net/swagger/index.html)
Step 1: User Management
Create a New User:

Use the "Create User" endpoint in the User Management section.
Provide the required information: email, password, and userId.
After creating a user, an API key will be generated for the user.
View Users:

To view existing users, use the "View Users" endpoint.
Enter the userId to retrieve the details of the user, including the email, password, userId, and API key.
Step 2: Authentication
To access most services (except for user creation), users must log in. Use the "Login" endpoint to authenticate and obtain a token.

Step 3: Product Management
After logging in, users can manage products:

Create New Products:

Use the "Create Product" endpoint to add new products.
View All Products:

Use the "Get All Products" endpoint to view all available products.
Update and Delete Products:

Use the relevant endpoints in the Product Management section to update or delete products.
Step 4: Checkout Management
Add Products to Checkout Basket:

Use the "Add to Checkout Basket" endpoint.
Provide the product details and your API key to add products to your basket.
View Items in Checkout Basket:

Use the "View Checkout Basket" endpoint to see the items currently in your basket.
Remove Items from Checkout Basket:

Use the "Remove from Checkout Basket" endpoint to remove items.
Complete Checkout:

Once ready, use the "Complete Checkout" endpoint to calculate the totals for your checkout basket.



