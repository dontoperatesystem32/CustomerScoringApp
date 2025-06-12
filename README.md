# Customer Scoring App - ASP.NET 8.0 Web API with MS SQL Server, Prometheus, Grafana & MSSQL Exporter

This project is a fully containerized **Customer Scoring App** that evaluates customer eligibility for loans and calculates the optional loan amount. The solution is built using ASP.NET 8.0 Web API with integrated Microsoft SQL Server, Prometheus, Grafana, and MSSQL Exporter for monitoring, orchestrated via Docker Compose.

---

## 📦 Tech Stack

* **ASP.NET Core 8.0 Web API**
* **Microsoft SQL Server 2022**
* **Prometheus v2.52.0**
* **Grafana OSS 11.1.0**
* **MSSQL Exporter v1.2.1**
* **Docker & Docker Compose**

---

## 🚀 Quick Start

### Prerequisites

* [Docker](https://www.docker.com/get-started) installed
* [Docker Compose](https://docs.docker.com/compose/) installed
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed (only required if you're modifying the code, especially for migrations)
* (Optional) [Git](https://git-scm.com/) if you want to clone the repository

### Clone the Repository

```bash
git clone <your-repo-url>
cd <repo-folder>
```

Or simply download the repository as a ZIP and extract it.

### Build & Run the Project

In the project root directory (where `docker-compose.yml` is located), open your terminal and run:

```bash
docker-compose up --build
```

Docker Compose will:

* Build the ASP.NET 8.0 Web API project.
* Start SQL Server 2022 container.
* Start Prometheus container.
* Start Grafana container.
* Start MSSQL Exporter for database monitoring.

---

## 🕊️ Monitoring

* **Prometheus**: [http://localhost:9090](http://localhost:9090)
* **Grafana**: [http://localhost:3000](http://localhost:3000)

  * Default credentials: `admin` / `YourStrong!Passw0rd` (you'll be prompted to change the password on first login)

---

## 📔 API

* The Customer Scoring API is available at:
  `http://localhost:5000` (mapped to internal port 8080 by Docker)

---

## ➕ How to Add New Conditions

If you want to extend the customer scoring logic by adding new **Conditions**, follow these steps:

### 1️⃣ Prerequisites

* You need to have **.NET 8.0 SDK** installed on your machine.

### 2️⃣ Create New Condition Class

* Create a new class in:

```
./Models/ConditionModels
```

* Implement your new condition logic in that class.

### 3️⃣ Register the New Condition

* In `Program.cs`:

Inside the `lifetime.ApplicationStarted.Register(() => ...)` section, add:

```csharp
conditionRepository.CreateCondition("{condition type that you pasted in the condition class that you created}");
```

* In `DataContext.cs`:

Inside `modelBuilder.Entity<BaseCondition>()` section, add:

```csharp
.HasValue<TotalLoansCondition>("TotalLoansCondition");
```

Replace `TotalLoansCondition` with your new condition class and type.

### 4️⃣ Add Database Migration

* Open terminal in the project root directory and run:

```bash
dotnet-ef migrations add YourConditionName
```

### 5️⃣ Rebuild and Restart Containers

* Shut down and remove the containers (including volumes to reset the DB):

```bash
docker-compose down -v
```

* Build and start containers again:

```bash
docker-compose up --build
```

The database will be automatically updated with the new migration.

---

## 🧹 Cleanup

To stop and remove all containers:

```bash
docker-compose down
```

---

## 📄 License

This project is licensed under the MIT License.

---

Feel free to fork, contribute or open issues if you encounter any problems.
