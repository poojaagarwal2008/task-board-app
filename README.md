

# TaskBoardSolution 🛠️

A distributed microservices-based task management system built using .NET Core 8, CQRS, SOLID principles, DDD architecture, Azure Blob Storage, and Docker containers.


## 🏗️ Project Structure

TaskBoardSolution/ │ ├── TaskService/ # Microservice for managing tasks ├── FileService/ # Microservice for uploading and storing files in Azure ├── docker-compose.yml # For running both microservices together ├── TaskBoardSolution.sln └── README.md


## 🚀 Architecture Overview

- **CQRS**: Separate Command and Query models
- **SOLID Principles**: Clean, maintainable, scalable
- **Domain-Driven Design (DDD)**: Entities and business logic encapsulated
- **Microservices**: Independent services for task management and file upload
- **Azure Blob Storage**: For storing file uploads securely
- **Docker + Docker Compose**: For containerized deployments
- **Swagger (OpenAPI)**: For API documentation

---markdown

## 📊 System Architecture Diagram

[Frontend (Postman / Web / Mobile)] | ↓ +---------------------+ +--------------------+ | TaskService | ←→→→ | FileService | | (CRUD Task APIs) | | (Azure Blob Upload) | +---------------------+ +--------------------+ ↓ ↓ In-Memory List DB Azure Blob Storage


---yaml

## 🧰 Tech Stack

- .NET Core 8.0 (Web API)
- CQRS Design Pattern
- SOLID Principles
- Domain-Driven Design (DDD)
- Azure Storage Blob SDK
- Docker
- Swagger / OpenAPI
- Azure DevOps (for CI/CD future ready)


## ⚙️ How to Run Locally

### 1. Clone the Repository

```bash
git clone https://github.com/YourGitHubUsername/TaskBoardSolution.git
cd TaskBoardSolution
2. Build and Run Microservices using Docker Compose
bash
Copy
Edit
docker-compose up --build
✅ This will:

Build TaskService and FileService Docker containers

Run both services together

3. Access Swagger UI
TaskService APIs → http://localhost:5001/swagger

FileService APIs → http://localhost:5002/swagger

🛠️ Main API Endpoints
🚀 TaskService (http://localhost:5001)

Method	Endpoint	Description
POST	/api/Task	Create new task
PUT	/api/Task/{id}	Update existing task
DELETE	/api/Task/{id}	Delete a task
GET	/api/Task/{id}	Get task by ID
GET	/api/Task?column={state}	Get all tasks (optionally filter by column)
📂 FileService (http://localhost:5002)

Method	Endpoint	Description
POST	/api/File/upload	Upload file to Azure Blob Storage
✅ Returns a public file URL after upload.

📦 Environment Variables
FileService - appsettings.json
json
Copy
Edit
"ConnectionStrings": {
  "AzureBlob": "Your_Azure_Blob_Storage_Connection_String"
}
✅ Replace with your Azure Storage connection string.

🧪 Testing (Future Scope)
Unit Testing with xUnit/NUnit and Moq

Integration Tests for API endpoints

Service Bus Queues for inter-microservice messaging

🛡️ Future Improvements (Optional)
Add Authentication & Authorization (JWT)

Implement Ocelot API Gateway

Azure Service Bus for asynchronous messaging

Deploy services to Azure Kubernetes Service (AKS)

CI/CD Pipeline with Azure DevOps

