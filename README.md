# Team 2 .NET service

Backend for hosting Car Dealership Application

**Requirements**:
 - Docker
- .NET SDK 5.0 (development)

# WebAPI Docker
To containerize the WebAPI separately:

Build the image:

`cd VehicleWebAPI`

`docker build -t {image name} .`

Run the image:

`docker run -d -p 80:80 -p 443:443 --name {container name} {image name}`

OR run from Docker Hub:

`docker run -d -p 80:80 -p 443:443 rmuehler/vehicle-web-api`

# Docker Compose
To run the WebAPI + Database + UI

From the root directory:

`docker-build compose`

` docker compose up `

