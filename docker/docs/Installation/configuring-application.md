# Configuring Your Application

Update your `App.config` file with the appropriate connection string based on your chosen database setup method:

For XAMPP:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```

For Docker:

```xml
<add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
```
