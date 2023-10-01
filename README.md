# ASP.NET Single Sign-On with LDAP

This repository contains an ASP.NET application that implements Single Sign-On (SSO) using LDAP (Lightweight Directory Access Protocol) for authentication. With this setup, users can log in to the application using their LDAP credentials, providing a seamless authentication experience.

## Features

- **LDAP Integration**: Authenticate users using LDAP credentials.
- **Single Sign-On (SSO)**: Achieve seamless authentication across multiple applications.

## Prerequisites

Before you begin, ensure you have the following installed/configured:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- LDAP server details (server address, port, base DN, etc.)

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/username/aspnet-ldap-sso.git

2. Navigate to the project directory:
   ```bash
   cd aspnet-ldap-sso

3. Restore dependencies and run the application:
   ```bash
   dotnet restore
   dotnet run

The application will be accessible at http://localhost:5000.

## LDAP Configuration

To configure LDAP for Single Sign-On, modify the appsettings.json file with your LDAP server details:
  {
  "LDAPSettings": {
    "Server": "ldap://ldap.example.com",
    "Port": 389,
    "BaseDN": "ou=users,dc=example,dc=com",
    "Username": "cn=admin,dc=example,dc=com",
    "Password": "yourpassword"
   }
 }

Ensure to replace the placeholders with your LDAP server information.

## Usage
1. Access the application at http://localhost:5000.
2. Click on the "Login" button.
3. Enter your LDAP username and password.
4. Upon successful authentication, you will be logged into the application.

## Contributing
If you'd like to contribute to this project, please follow these steps:
- Fork the repository on GitHub.
- Create a new branch with a descriptive name.
- Commit your changes to the new branch.
- Push the branch to your forked repository.
- Submit a pull request to the original repository.

Please ensure that your code follows the project's coding standards and includes appropriate tests for any new functionality.

## License
This project is open-source and available under the [MIT License](https://opensource.org/licenses/MIT). Feel free to use it as a reference or starting point for your own projects.
