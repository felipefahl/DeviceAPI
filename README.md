# Device Database API

A REST service that supports the management of a device database

### Represented entities
1. Device
   * Device name
   * Device brand
   * Creation time

## Prerequisites
- [Docker](https://www.docker.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)


## Running the API

1. Clone the repository
```bash
git clone https://github.com/felipefahl/DeviceAPI.git
```


2. Build the API image and run the services using Docker Compose.

```bash
cd DeviceAPI
docker-compose build
docker-compose up
```

3. Verify the API is running by sending a request to [http://localhost:32033/api/devices](http://localhost:32033/api/devices)
```bash
curl http://localhost:5000/api/devices
```

## Stopping the API
1. Stop the services and remove the services and volumes.

```bash
docker-compose down
docker-compose rm
```

## Supported Operations
1. Add device
2. Get device by identifier
3. List all devices
4. Update device (full and partial)
5. Delete a device
6. Search device by brand

With the API running all methods cam be seen here [http://localhost:32033/swagger/index.html](http://localhost:32033/swagger/index.html)