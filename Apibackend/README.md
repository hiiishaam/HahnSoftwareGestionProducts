# Apibackend
# ASP.NET Core API with SQL Server (Dockerized)

This project is a Dockerized ASP.NET Core 9 Web API connected to a SQL Server 2022 database.  
It uses **multi-stage builds** to optimize the final Docker image and **docker-compose** for container orchestration.

---

## Features
- ASP.NET Core 9.0 Web API
- SQL Server 2022 database (containerized)
- Multi-stage Docker build (smaller final image)
- Configurable via environment variables
- Uses `docker-compose` for orchestration

## Dockerfile (Multi-stage Build)
The Dockerfile is structured in **four stages**:
1. **Base** – ASP.NET Core runtime image.
2. **Build** – Restores dependencies and builds the project.
3. **Publish** – Publishes the project for deployment.
4. **Final** – Copies published output into a lightweight runtime image.

## docker-compose.yml
The `docker-compose.yml` defines two services:
- **api**: ASP.NET Core Web API
- **db**: SQL Server 2022 database

**Note:** When the API runs, the database will be created automatically.

Both services are connected via the `app-network  driver: bridge` .

## Running the Application

# 1. Clone the Repository
git clone https://github.com/hiiishaam/HahnSoftwareGestionProducts.git
cd HahnSoftwareGestionProducts

# 2. Build and Start Containers
Navigate to the project folder and execute the following command to build the images and start the containers:

cd C:\ProjectPath\Apibackend\Apibackend
docker-compose up --build

# 3. Access the API
API: http://localhost:8888
Database: localhost,1433
User: sa
Password: (defined in docker-compose.yml)
