# Obtaining Google Maps Static API Key

To use the Google Maps Static API in your application, you need to obtain an API key.

Follow these steps:

1. Go to the Google Cloud Console: [https://console.cloud.google.com/](https://console.cloud.google.com/)

   ![](img/9066d5e10e5000d9fb2b808e6c847acb.png)

2. Create a new project or select an existing one.

   ![](img/911210b2cdff14fce098fab06e52ba9c.png)

3. Enable the Google Maps Static API:

   1. In the sidebar, click on "APIs & Services" > "Library"
   2. Search for "Maps Static API"
   3. Click on "Maps Static API" and then click "Enable"

   ![](img/c918fa12662e168432b6c91e52126df9.png)

4. Create credentials for the API:

   1. In the sidebar, click on "APIs & Services" > "Credentials"
   2. Click "Create Credentials" and select "API Key"

   ![](img/fc8f432ff51bf55828e5e9c38e0d2139.png)

5. Restrict the API key (recommended):

   1. In the API key details page, click "Restrict Key"
   2. Under "Application restrictions", choose "IP addresses" and add your organization's ASN IP
   3. Under "API restrictions", select "Restrict key" and choose "Maps Static API"

   ![](img/51d4ab41cc9b0de5b4014cd6e489fd07.png)

6. Copy the API key and paste it into your `App.config` file:

```xml
<add key="GoogleMapsApiKey" value="YOUR_API_KEY_HERE"/>
```
