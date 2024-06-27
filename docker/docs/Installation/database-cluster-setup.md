# Database Cluster Setup (Optional)

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
