
# 18. Installation Guide

## 18.1 Installation Overview

This guide provides detailed instructions for setting up a WinForms application that uses a database and the Google Maps Static API. The setup process involves:

1. Configuring the application settings
2. Obtaining a Google Maps Static API key
3. Setting up a database (using either XAMPP or Docker)
4. Running the WinForms application

The database setup is a required step, and you must choose either Method 1 (XAMPP) or Method 2 (Docker) for this purpose.

The Database Cluster Setup is optional and can be implemented for more advanced configurations.

## 18.2 Prerequisites

- .NET Framework (version compatible with your WinForms application)
- Google Maps Static API Key
- Database setup (choose either XAMPP or Docker)

## 18.3 Setup

### 18.3.1 Clone the Repository

1. Clone the repository
2. Copy the `App.config.example` to `App.config`

### 18.3.2 Customizing App.config

The `App.config` file contains important settings for your application. Here's how to customize it:

```xml
<appSettings>
    <add key="DevMode" value="True"/>
    <add key="GoogleMapsApiKey" value="Enter your Google Maps Static API Key"/>
    <add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
    <!-- Additional connection strings if needed -->
</appSettings>
```

### 18.3.3 Key Settings

1. **DevMode**: Set to "True" for development, "False" for production
2. **GoogleMapsApiKey**: Replace with your actual Google Maps Static API Key
3. **ConnectionString1**: Customize based on your chosen database setup method

## 18.4 Obtaining Google Maps Static API Key

To use the Google Maps Static API in your application, you need to obtain an API key.

Follow these steps:

1. Go to the Google Cloud Console: [https://console.cloud.google.com/](https://console.cloud.google.com/)

   ![A screenshot of a computer Description automatically generated](media/9066d5e10e5000d9fb2b808e6c847acb.png)
2. Create a new project or select an existing one.

   ![A screenshot of a computer Description automatically generated](media/911210b2cdff14fce098fab06e52ba9c.png)
3. Enable the Google Maps Static API:

   1. In the sidebar, click on "APIs & Services" > "Library"
   2. Search for "Maps Static API"
   3. Click on "Maps Static API" and then click "Enable"

   ![A screen shot of a computer Description automatically generated](media/c918fa12662e168432b6c91e52126df9.png)
4. Create credentials for the API:

   1. In the sidebar, click on "APIs & Services" > "Credentials"
   2. Click "Create Credentials" and select "API Key"

   ![A screenshot of a computer Description automatically generated](media/fc8f432ff51bf55828e5e9c38e0d2139.png)
5. Restrict the API key (recommended):

   1. In the API key details page, click "Restrict Key"
   2. Under "Application restrictions", choose "IP addresses" and add your organization's ASN IP
   3. Under "API restrictions", select "Restrict key" and choose "Maps Static API"

   ![A screenshot of a computer Description automatically generated](media/51d4ab41cc9b0de5b4014cd6e489fd07.png)
6. Copy the API key and paste it into your `App.config` file:

```xml
<add key="GoogleMapsApiKey" value="YOUR_API_KEY_HERE"/>
```

Remember to keep your API key secure and never share it publicly. For production use, consider using environment variables or secure key management systems.

## 18.5 Database Setup (Required)

Choose one of the following methods to set up your database:

### Method 1: XAMPP

#### Downloading and Installing XAMPP

1. Visit the official XAMPP website: [XAMPP](https://www.apachefriends.org/)
2. Download and install XAMPP for your operating system
3. Start XAMPP Control Panel
4. Start Apache and MySQL services

#### Configuring Database

1. Access phpMyAdmin: [phpMyAdmin](http://localhost/phpmyadmin)
2. Create a new database named "itp4915m_se1d_group4"
3. Import your SQL scripts to set up the database structure

### Method 2: Docker

#### Downloading and Installing Docker

1. Visit [Docker Desktop](https://www.docker.com/products/docker-desktop)
2. Download and install Docker Desktop for your operating system
3. Start Docker Desktop

#### Configuring Docker for Your Database

1. Create a `docker-compose.yml` file in your project root:

```yaml
version: '3'
services:
  db:
    image: mariadb:10.5
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: itp4915m_se1d_group4
    ports:
      - "3306:3306"
    volumes:
      - ./sql-scripts:/docker-entrypoint-initdb.d
  phpmyadmin:
    image: phpmyadmin/phpmyadmin
    ports:
      - "8080:80"
    environment:
      PMA_HOST: db
      PMA_USER: root
      PMA_PASSWORD: rootpassword
```

2. Place your SQL scripts in a folder named `sql-scripts` in your project directory
3. Open a terminal/command prompt in your project directory
4. Run `docker-compose up -d`
5. Access phpMyAdmin at [phpMyAdmin Docker](http://localhost:8080) to manage your database

## 18.6 Configuring Your Application

Update your `App.config` file with the appropriate connection string based on your chosen database setup method:

For XAMPP:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```

For Docker:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```

## 18.7 Running Your WinForms Application

1. Ensure your chosen database environment (XAMPP or Docker) is running
2. Navigate to the TemplateV1 folder
3. Run `LMCIS-1DG4.exe`

## 18.8 Accessing Services

- **Main Application**: Run `LMCIS-1DG4.exe`
- **PHPMyAdmin (XAMPP)**: [phpMyAdmin](http://localhost/phpmyadmin)
- **PHPMyAdmin (Docker)**: [phpMyAdmin Docker](http://localhost:8080)

## 18.9 Database Cluster Setup (Optional)

For a more robust setup, you can configure a database cluster:

1. Modify the `docker-compose.yml` to include multiple database instances
2. Set up a load balancer (e.g., HAProxy) to distribute requests
3. Update the connection strings in `App.config` to point to the load balancer

Example cluster setup in `docker-compose.yml`:

```yaml
services:
  db-master:
    image: mariadb:10.5
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: itp4915m_se1d_group4
    ports:
      - "3306:3306"
    volumes:
      - ./sql-scripts:/docker-entrypoint-initdb.d

  db-slave1:
    image: mariadb:10.5
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
    ports:
      - "3307:3306"

  db-slave2:
    image: mariadb:10.5
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
    ports:
      - "3308:3306"

  haproxy:
    image: haproxy:latest
    ports:
      - "3309:3306"
    volumes:
      - ./haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg
    depends_on:
      - db-master
      - db-slave1
      - db-slave2
```

## 18.10 Stopping the Application

1. Close the WinForms application
2. Open a terminal/command prompt in your project directory
3. For Docker: Run `docker-compose down`
4. For XAMPP: Stop Apache and MySQL services in XAMPP Control Panel

## 18.11 Troubleshooting

- Verify database connection settings in `App.config`
- Ensure the database service is running (XAMPP or Docker)
- Check that port 3306 is not being used by another application
- Validate your Google Maps API key

Remember to keep sensitive

 information like API keys and passwords secure.

# 19. User Guide

## 19.1 Browse Spare Part

![Browse Spare Part](media/3133897ebad6634d241b0a242bd619ad.PNG)

![View Spare Part Details](media/d3e8a38b8aea1861f5302535ecbc95f7.PNG)

After clicking the “View” button, it will go to the page that contains detailed information of the selected spare part.

![Spare Part Details](media/28c350090dde264d7980db62b47bedb8.PNG)

## 19.2 Create Order

![Create Order](media/c1ff80406762cbdbb8c10e67ca992bce.PNG)

![Add Spare Part to Order](media/00961119c2cd

6f629d06c89e422ee5b1.PNG)

![Create New Order](media/7db7ad1ed10592d173ad2d90b79d30c4.PNG)

## 19.3 View Order

![View Order](media/3d3c7f2750a29f9b08cbe81dfd2e0df3.PNG)

![Order Details](media/4bc63cd3f66de1e8bcd52dcf4e9302d0.PNG)

## 19.4 Generate Delivery Note

![Generate Delivery Note](media/c6d4a91f337d5a9065cfa5a7e82b4c68.PNG)

![Delivery Note Details](media/7d5a34511be3a15b26a22449c9441729.PNG)

## 19.5 Send Delivery Note

![Send Delivery Note](media/53e9dbe5f2608d924b8ec9ae47e7e169.PNG)

## 19.6 Receive Delivery Note

![Receive Delivery Note](media/36bfb7f4b1579ea544373b48da0e5002.PNG)

![View Delivery Note Details](media/4106ac2c611f0938055d11136010969b.PNG)

## 19.7 Update Order Status

![Update Order Status](media/54a21a6e89836dd1e1a7cc0e6a2aa63c.PNG)

## 19.8 Manage Returns

![Manage Returns](media/144eb2e68d90a8b70b8f2e2b2f46d232.PNG)

![Return Details](media/3a3321538d2e232d48dc35bb0a353f5b.PNG)

![Process Return](media/95df01277af7df61d5482e9f20ab6eb8.PNG)

---

以上是將原始文檔分解並重新格式化為GitBook格式的建議。如果您有任何其他需求或想要進一步調整，請告訴我。
