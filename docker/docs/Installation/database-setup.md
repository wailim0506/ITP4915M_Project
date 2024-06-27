# Database Setup (Required)

Choose one of the following methods to set up your database:

## Method 1: XAMPP

### Downloading and Installing XAMPP

1. Visit the official XAMPP website: [XAMPP](https://www.apachefriends.org/)
2. Download and install XAMPP for your operating system
3. Start XAMPP Control Panel
4. Start Apache and MySQL services

### Configuring Database

1. Access phpMyAdmin: [phpMyAdmin](http://localhost/phpmyadmin)
2. Create a new database named "itp4915m_se1d_group4"
3. Import your SQL scripts to set up the database structure

## Method 2: Docker

### Downloading and Installing Docker

1. Visit [Docker Desktop](https://www.docker.com/products/docker-desktop)
2. Download and install Docker Desktop for your operating system
3. Start Docker Desktop

### Configuring Docker for Your Database

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
