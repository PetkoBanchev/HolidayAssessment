# Holiday Assessment App

A full-stack .NET application that retrieves, processes, and displays holiday data per country using an external API, a backend Web API, and a Razor Pages frontend dashboard.

---

## Project Structure

```bash
HolidayAssessment/
│
├── src/
│ └── HolidayAssessment/ # Backend ASP.NET Core Web API
│
├── ui/
│ └── HolidayAssessment.UI/ # Frontend Razor Pages UI
│
├── tests/
│ └── HolidayAssessment.Tests/ # xUnit test project
```
---

## Features

- Import holidays by country and year
- View last 3 holidays per country
- Filter weekday holidays (excludes weekends)
- Count weekday holidays per country
- Find shared holidays between countries
- Cached country list using MemoryCache
- Razor Pages dashboard UI
- Clean layered architecture (API → Service → Repository)

---

## Prerequisites

- .NET 9 SDK
- Visual Studio 2022+ / Rider / VS Code
- Internet access (external holiday API)

Verify installation:

```bash
dotnet --version
```
[.NET 9 Download](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
```bash
dotnet ef --version
```
If not present, you can install it like this
```bash
dotnet tool install --global dotnet-ef
```
## How to run the application
You must run both the backend API and the frontend UI. You must start the backend first; otherwise, the frontend throws exceptions!

### 1. Clone the repository

```bash
git clone https://github.com/PetkoBanchev/HolidayAssessment.git
```
Or use this link and clone it with Visual Studio or GitHub Desktop for example
[GitHub Repository](https://github.com/PetkoBanchev/HolidayAssessment)

### 2. Navigate to the root
```bash
cd HolidayAssessment
```
Or in Visual Studio
#### View > Terminal (Ctrl + `)

### 3. Run migrations
```bash
dotnet ef database update
```

### 4. Run the backend
```bash
cd src/HolidayAssessment
dotnet run
```
You test the API endpoints here:
[http://localhost:5181/scalar/v1](http://localhost:5181/scalar/v1)

### 5. Run the frontend
Open a new terminal and navigate to the root. Then run:
```bash
cd ui/HolidayAssessment.UI
dotnet run
```
The front is here:
[http://localhost:5195/](http://localhost:5195/)

### 6. Import data from Nager
Use the frontend or the Import Holidays endpoint
```http
POST /api/holidays/import
```
If you don't import data, the endpoints will always return empty results

## API Endpoints
```http

Import Holidays
POST /api/holidays/import

Last 3 Holidays
GET /api/holidays/{countryCode}/last-three

Weekday Holidays
POST /api/holidays/weekday-holidays

Weekday Holiday Count
POST /api/holidays/weekday-holidays-count

Shared Holidays
GET /api/holidays/shared-holidays?year=2026&countryA=NL&countryB=DE
```

## Running tests
Open a terminal in the root folder
```bash
dotnet test
```

## Common Issues

### Backend not reachable
- Ensure backend is running on:

https://localhost:5181

### API call failures (404)

Check:
- BaseAddress in HttpClient
- Backend is running
- No typos in endpoint URLs

### Empty country list

Ensure:
- You have imported the data for the chosen year and country!
- Country API endpoint is working
- MemoryCache is enabled in the backend

### Application not loading

Make sure BOTH are running:
```
Backend → 7000
Frontend → 5195
```














