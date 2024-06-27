# 18. Installation Guide

## 18.1 Installation Overview

This guide provides detailed instructions for setting up a WinForms application
that uses a database and the Google Maps Static API. The setup process involves:

1\. Configuring the application settings

2\. Obtaining a Google Maps Static API key

3\. Setting up a database (using either XAMPP or Docker)

4\. Running the WinForms application

The database setup is a required step, and you must choose either Method 1
(XAMPP) or Method 2 (Docker) for this purpose.

The Database Cluster Setup is optional and can be implemented for more advanced
configurations.

## 18.2 Prerequisites

- .NET Framework (version compatible with your WinForms application)
- Google Maps Static API Key
- Database setup (choose either XAMPP or Docker)

## 18.3 Setup

1. Clone the repository
2. Copy the `App.config.example` to `App.config`

## Customizing App.config

The `App.config` file contains important settings for your application. Here's
how to customize it:

```xml
<appSettings>
    <add key="DevMode" value="True"/>
    <add key="GoogleMapsApiKey" value="Enter your Google Maps Static API Key"/>
    <add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
    <!-- Additional connection strings if needed -->
</appSettings>
```

18.4 Key Settings

1. **DevMode**: Set to "True" for development, "False" for production
2. **GoogleMapsApiKey**: Replace with your actual Google Maps Static API Key
3. **ConnectionString1**: Customize based on your chosen database setup method

## 18.5 Obtaining Google Maps Static API Key

To use the Google Maps Static API in your application, you need to obtain an API
key.

Follow these steps:

1. Go to the Google Cloud Console: <https://console.cloud.google.com/>

![A screenshot of a computer Description automatically
generated](media/9066d5e10e5000d9fb2b808e6c847acb.png)

![A screenshot of a computer Description automatically
generated](media/ed9f29af1a5d070b10de5142060b4324.png)

1. Create a new project or select an existing one.

    ![A screenshot of a computer Description automatically
    generated](media/911210b2cdff14fce098fab06e52ba9c.png)

    ![A screenshot of a computer Description automatically
    generated](media/9829218de7e1db466043d7ce51245ec8.png)

    ![A screenshot of a computer Description automatically
    generated](media/8048ffa4b2ac7710519ebe01d10f3222.png)

1. Enable the Google Maps Static API:
    1. In the sidebar, click on "APIs & Services" \> "Library"
    2. Search for "Maps Static API"
    3. Click on "Maps Static API" and then click "Enable"

![](media/c918fa12662e168432b6c91e52126df9.png)

![A screen shot of a computer Description automatically
generated](media/65c29f22a52c568cb19241006f5236f5.png)

![A screenshot of a computer Description automatically
generated](media/62520a870782201f8382182458f84b40.png)

![A screenshot of a computer Description automatically
generated](media/d35e367d3d6f4205e09a3b2f55edaf85.png)

1. Create credentials for the API:
    1. In the sidebar, click on "APIs & Services" \> "Credentials"
    2. Click "Create Credentials" and select "API Key"

![A screenshot of a computer Description automatically
generated](media/fc8f432ff51bf55828e5e9c38e0d2139.png)

![A screenshot of a computer Description automatically
generated](media/51d4ab41cc9b0de5b4014cd6e489fd07.png)

![A screenshot of a computer Description automatically
generated](media/35143d82621bd70e6e6c9ddaea641321.png)

1. Restrict the API key (recommended):
    1. In the API key details page, click "Restrict Key"
    2. Under "Application restrictions", choose " IP addresse " and add your
        organization 's ASN IP
    3. Under "API restrictions", select "Restrict key" and choose "Maps Static
        API"
2. Copy the API key and paste it into your `App.config` file:

```xml
<add key="GoogleMapsApiKey" value="YOUR_API_KEY_HERE"/>
```

Remember to keep your API key secure and never share it publicly. For production
use, consider using environment variables or secure key management systems.

## 18.6 Database Setup (Required)

Choose one of the following methods to set up your database:

### Method 1: XAMPP

#### Downloading and Installing XAMPP

1. Visit the official XAMPP website: <https://www.apachefriends.org/>
2. Download and install XAMPP for your operating system
3. Start XAMPP Control Panel
4. Start Apache and MySQL services

#### Configuring Database

1. Access phpMyAdmin: <http://localhost/phpmyadmin>
2. Create a new database named "itp4915m_se1d_group4"
3. Import your SQL scripts to set up the database structure

### Method 2: Docker

#### Downloading and Installing Docker

1. Visit <https://www.docker.com/products/docker-desktop>
2. Download and install Docker Desktop for your operating system
3. Start Docker Desktop

#### 18.6 Configuring Docker for Your Database

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

1. Place your SQL scripts in a folder named `sql-scripts` in your project
    docker directory
2. Open a terminal/command prompt in your project docker directory
3. Run `docker-compose up -d`
4. Access phpMyAdmin at <http://localhost:8080> to manage your database

## 18.7 Configuring Your Application

Update your `App.config` file with the appropriate connection string based on
your chosen database setup method:

For XAMPP:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```

For Docker:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```

## 18.8 Running Your WinForms Application

1. Ensure your chosen database environment (XAMPP or Docker) is running
2. Navigate to the TemplateV1 folder
3. Run `LMCIS-1DG4.exe`

## 18.9 Accessing Services

- **Main Application**: Run `LMCIS-1DG4.exe`
- **PHPMyAdmin (XAMPP)**: <http://localhost/phpmyadmin>
- **PHPMyAdmin (Docker)**: <http://localhost:8080>

## 18.10 Database Cluster Setup (Optional)

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

## 18.11 Stopping the Application

1. Close the WinForms application
2. Open a terminal/command prompt in your project docker directory
3. For Docker: Open a terminal/command prompt

    Run `docker-compose down`

4. For XAMPP: Stop Apache and MySQL services in XAMPP Control Panel

## 18.12 Troubleshooting

- Verify database connection settings in `App.config`
- Ensure the database service is running (XAMPP or Docker)
- Check that port 3306 is not being used by another application
- Validate your Google Maps API key

Remember to keep sensitive information like API keys and passwords secure,
especially in production environments.

19\. User Guide

**19.1 Browse Spare Part**

![一張含有 文字, 螢幕擷取畫面, 軟體, 網站 的圖片
自動產生的描述](media/3133897ebad6634d241b0a242bd619ad.PNG)

![一張含有 文字, 螢幕擷取畫面, 設計 的圖片
自動產生的描述](media/d3e8a38b8aea1861f5302535ecbc95f7.PNG)

After clicking the “View” button, it will go to the page that contain detail
info of the selected spare part

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/28c350090dde264d7980db62b47bedb8.PNG)

**19.2 Create Order**

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/c1ff80406762cbdbb8c10e67ca992bce.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 網站 的圖片
自動產生的描述](media/00961119c2cd6e834ba90b5a73110bd2.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 網站 的圖片
自動產生的描述](media/00961119c2cd6e834ba90b5a73110bd2.PNG)

We can see that the spare part added is in the cart

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/0db7826b60a0fbbf4bd455c99dab82cd.PNG)

![一張含有 文字, 軟體, 網頁, 電腦圖示 的圖片
自動產生的描述](media/4d444d252df8d2b1eb364c7357b0039c.PNG)

![一張含有 文字, 軟體, 網頁, 電腦圖示 的圖片
自動產生的描述](media/ab9730694a375dc1246aae45575fa1d0.PNG)

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/0db7826b60a0fbbf4bd455c99dab82cd.PNG)

![一張含有 文字, 軟體, 網頁, 電腦圖示 的圖片
自動產生的描述](media/ab9730694a375dc1246aae45575fa1d0.PNG)

![一張含有 文字, 軟體, 網頁, 電腦圖示 的圖片
自動產生的描述](media/52b102f1a44160e85e890e05e7b0206c.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 電腦圖示 的圖片
自動產生的描述](media/77893920012afd639fbd5b588c26cbe8.PNG)

**19.3 Search Order**

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/6e1bc8d5a6e4087eaddd2cda9134fd42.PNG)

**19.4 View Order**

![一張含有 文字, 螢幕擷取畫面, 軟體, 電腦圖示 的圖片
自動產生的描述](media/77893920012afd639fbd5b588c26cbe8.PNG)

After clicking “View Order” of a specific order, user will be directed to the
order’s page.

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/a4df8aa3404129ba7e58b4d2c9a2340b.PNG)

**19.5 Cancel Order**

![](media/a4df8aa3404129ba7e58b4d2c9a2340b.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/fb5bfbfae7f75ea808e7147aa955fc2f.PNG)

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/7155ebdbeae9b4fd823871dc7a39a9be.PNG)

![](media/345a38ab0a42326e7d2967616af438fe.PNG)

**19.6 Edit Order**

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/a4df8aa3404129ba7e58b4d2c9a2340b.PNG)

User will be directed to edit order page

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/1bc3e1a549654c1aa9a8098b677d3d1e.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/1bc3e1a549654c1aa9a8098b677d3d1e.PNG)

After clicking the pen, a textbox and a rubbish bin image is shown

![](media/d6038142fc30350032c8403963529d66.PNG)

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/bb4b8bfc374c6e93bd4b1fe738a8d391.PNG)

![](media/8d13d101fe68630023d48648369a716a.PNG)

To add new spare part to the order, go back to spare part list

![一張含有 文字, 螢幕擷取畫面, 網站, 設計 的圖片
自動產生的描述](media/f660c6755375ace047ba78843c85bc96.PNG)

Go into the spare part’s detail info page

![一張含有 文字, 螢幕擷取畫面, 軟體, 網站 的圖片
自動產生的描述](media/696ee98a9068e8ce0ea6342d580e89ea.PNG)

After clicking the “Add to Existing Order” Button, user will be directed to a
new page

![](media/f428e45b1037cac67696f5e16b18200e.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體 的圖片
自動產生的描述](media/f8461cd5bd401ffd4974a5845a8ff1a7.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體 的圖片
自動產生的描述](media/e12698d9858475776e317954fceb8e76.PNG)

Go back to the order’s page

![一張含有 文字, 螢幕擷取畫面, 軟體, 數字 的圖片
自動產生的描述](media/c1379a1ec6eb170caeec01da9f3149d3.PNG)

To delete spare part in the order, click the red rubbish bin after clicking the
pen

![一張含有 文字, 螢幕擷取畫面, 軟體, 電腦圖示 的圖片
自動產生的描述](media/823b6073cb0e050619745f241a892b9f.PNG)

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/c1faa7e5188ce045a26cc7dbbf2b9c91.PNG)

![一張含有 文字, 軟體, 電腦圖示, 網頁 的圖片
自動產生的描述](media/5e753e541e7f8d7550de5d34cb321c2e.PNG)

![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/f582e0f65ef72b3cf3f05422a75dc04f.PNG)

**19.7 Re-Order**

Go to a specific order’s page first

**![一張含有 文字, 螢幕擷取畫面, 軟體, 網頁 的圖片
自動產生的描述](media/7d5e380c2e95651c6abb1600205b04d2.PNG)**

**![一張含有 文字, 軟體, 螢幕擷取畫面, 網頁 的圖片
自動產生的描述](media/8013065fbcbd521cb6c9ff3b0feb036a.PNG)**

Proceed to cart to see spare part in that order is add to cart

**![](media/c7bc99603a94509f8922556a86530d84.PNG)**

**19.8 Add to Favourite**

Go to the specific spare part page first

![](media/0aa7ace4e5ea6d227575a2698705a493.png)

![](media/0aa7ace4e5ea6d227575a2698705a493.png)

![](media/c05a2fc144bbddf65d329f839b954248.png)

**19.9 Remove from Favourite**

Go to the spare part that is favourite

![](media/8e549bf9242205b8f6ee5fa8cd47a1bc.png)

![](media/18d2b236c2d59ec7b5022796ca72bbe3.png)

Alternatively, user can go to favorite secion to remove spare part from favorite

![](media/cc08d7f6e92f57030887100660253754.png)

**19.10 Give Feedback**

Go to give feedback section through left hand side button on the window

![](media/9689d2e6b4804c612957460ea259aca2.png)

![](media/e51a2bd89a889ff65a5e3a2a80b5bfcc.png)
