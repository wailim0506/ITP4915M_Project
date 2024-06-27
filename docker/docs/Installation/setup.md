# Setup

## Clone the Repository

1. Clone the repository
2. Copy the `App.config.example` to `App.config`

## Customizing App.config

The `App.config` file contains important settings for your application. Here's how to customize it:

```xml
<appSettings>
    <add key="DevMode" value="True"/>
    <add key="GoogleMapsApiKey" value="Enter your Google Maps Static API Key"/>
    <add key="ConnectionString1" value="server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"/>
    <!-- Additional connection strings if needed -->
</appSettings>
```

## Key Settings

1. **DevMode**: Set to "True" for development, "False" for production
2. **GoogleMapsApiKey**: Replace with your actual Google Maps Static API Key
3. **ConnectionString1**: Customize based on your chosen database setup method
