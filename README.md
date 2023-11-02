# InventoryDemo

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Description

The Customer Management Application is a web application built with ASP.NET Core MVC for managing customer data. It allows users to create, read, update, and delete customer records in a SQL Server database.

## Features

- View a list of all customers.
- Create a new customer record.
- Edit existing customer details.
- View detailed information about a specific customer.
- Delete customer records.

## Getting Started

These instructions will help you get a copy of the project up and running on your local machine.

### Prerequisites

Before you begin, make sure you have the following software installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/), or any code editor of your choice.

### Installation

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/customer-management-app.git
2. Navigate to the project directory:
    ```bash
    cd customer-management-app

3. Create a SQL Server database and update the connection string in CustomerDataAccessLayer.cs with the appropriate database details.

### Usage
1. Build the Application
    ```bash
    dotnet build
2. Run the Application
   ```bash
   dotnet run
