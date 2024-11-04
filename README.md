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
- [Building the Application](#building-the-application)
- [Running the Application](#running-the-application)
- [Running Tests](#running-tests)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)

## Setup Instructions

1. **Clone the Repository**

   Open your terminal or command prompt and run the following command:

   ```bash
   git clone https://github.com/yourusername/your-repo-name.git
