# 🐶 KennelPro

> Professional cross-platform dog kennel management platform built with .NET MAUI.

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-Cross--Platform-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/dotnet/maui/)
[![C%23](https://img.shields.io/badge/C%23-Language-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![SQLite](https://img.shields.io/badge/SQLite-Database-003B57?logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![Status](https://img.shields.io/badge/Status-v0.2.0--alpha-orange)](#-development-status)

---

# 📖 About

**KennelPro** is a modern cross-platform application designed for professional dog breeders, kennel owners and regular dog owners.

The main goal of the project is to create a single digital ecosystem where users can manage their dogs, kennel information, medical records, breeding history, documents and important events.

KennelPro is built with **.NET MAUI**, allowing the application to target multiple platforms from a shared C# and XAML codebase.

The project is designed with scalability and maintainability in mind, using separation between the UI, business logic and data-access layers.

---

# 🎯 Project Goals

KennelPro aims to make everyday kennel management easier by providing tools for:

- 🐶 Dog management
- 🏡 Kennel management
- 🩺 Medical records
- 💉 Vaccination tracking
- 💊 Parasite treatment tracking
- 🧬 Breeding management
- ♀ Heat cycle tracking
- 🐾 Litter management
- 🐕 Puppy management
- 📄 Document management
- 🏆 Titles and pedigrees
- 🔔 Notifications and reminders
- ☁ Cloud synchronization
- 💾 Backup and restore
- 📊 Statistics and analytics
- 🤖 Future AI assistance

---

# ✨ Current Features

## 🔐 Authentication

KennelPro already includes a working authentication foundation.

Implemented:

- 👤 User registration
- 🔑 Login
- 🔒 Password hashing
- 🏡 Automatic kennel creation during registration
- 💾 SQLite user storage
- 🧠 Session management
- 🚪 Logout
- 🔄 Login/Register navigation
- 🔐 Current-user identification

Authentication data is connected to the current kennel, allowing the application to isolate user data between different kennels.

---

# 🏡 Kennel Management

The application uses the kennel as one of the main data-isolation boundaries.

Each registered user can have a kennel associated with their account.

Kennel information is used to connect:

- users;
- dogs;
- medical records;
- breeding information;
- litters;
- puppies;
- documents;
- notifications.

This architecture allows KennelPro to support multiple independent kennels without mixing their data.

---

# 🐶 Dog Management

Dog management is currently one of the main functional parts of the application.

Implemented:

- ➕ Add dogs
- ✏️ Edit dogs
- 🗑 Delete dogs
- 👁 View dog details
- 🔎 Search dogs
- 🐕 Dog list
- 🧬 Breed selection
- ⚧ Gender selection
- 🎂 Birth date
- 🔢 Microchip number
- 🏆 Pedigree information
- 📝 Notes

Dog records are associated with the current kennel.

The application checks the kennel ownership before performing dog operations, preventing users from accessing dogs belonging to another kennel.

---

# 🩺 Medical Management

KennelPro contains a medical management module for maintaining a dog's health history.

Implemented foundation:

- 🩺 Medical records
- 💉 Vaccinations
- 💊 Parasite treatments
- 💊 Medications
- 🦠 Diseases
- 📅 Medical dates
- 📝 Medical notes
- ➕ Create records
- ✏️ Edit records
- 🗑 Delete records
- 👁 View medical information

Medical information is linked to dogs and protected by kennel ownership checks.

The module is currently functional at the CRUD level, while additional scenario testing and future UI improvements are still planned.

---

# 🧬 Reproduction Management

KennelPro contains a reproduction management module.

The current architecture supports:

- ♀ Heat cycles
- 🐕 Mating records
- 🐾 Litters
- 🐶 Puppies

The backend includes:

- models;
- repositories;
- services;
- validation;
- kennel isolation;
- database relationships;
- EF Core migrations.

Supported reproduction data includes information such as:

- heat cycle dates;
- heat cycle status;
- notes;
- male and female dogs;
- mating dates;
- litter parents;
- litter birth dates;
- puppy names;
- puppy numbers;
- puppy gender;
- puppy birth dates;
- chip numbers;
- pedigree numbers;
- notes.

The reproduction module is currently **partially complete**.

The backend and data layer are implemented, while the complete CRUD UI and navigation for all reproduction entities are still being developed.

---

# 📄 Documents

The project already contains the foundation for document management.

The planned system will allow users to store and manage important dog and kennel documents.

Planned document functionality includes:

- 📄 Documents
- 🏆 Titles
- 📎 Dog-related files
- 🗂 Document organization
- 📤 Export
- 📑 PDF generation

The document module is currently under development.

---

# 🔔 Notifications

KennelPro contains notification infrastructure intended to support reminders for important events.

Planned reminders include:

- 💉 Vaccinations
- 💊 Parasite treatments
- 🩺 Medical events
- ♀ Heat cycles
- 🧬 Breeding events
- 🐾 Puppy-related events
- 📅 Important dates

The notification system is currently at an early development stage.

---

# 🌐 API Infrastructure

The project already contains API infrastructure based on `HttpClient`.

The planned API functionality includes integration with dog breed data.

Future API features may provide:

- 🐕 Breed search
- 📖 Breed descriptions
- 📷 Breed photographs
- 📊 Breed characteristics
- 🏆 FCI standards where legally available
- 🔎 Search and filtering

The API layer is currently infrastructure/preparation rather than a fully completed feature.

---

# ☁ Cloud & Backup

Cloud functionality is planned as part of the long-term project roadmap.

Planned features:

- ☁ Automatic cloud synchronization
- 💾 Backup
- ♻️ Data restoration
- 📱 Multi-device synchronization
- 🔐 Secure data storage

These features are not yet fully implemented.

---

# 🤖 AI Assistant

AI assistance is planned for a future version of KennelPro.

The future AI assistant may be able to:

- 💬 Answer user questions
- 🔎 Search dogs and documents
- 📊 Analyze kennel data
- 🔔 Identify important upcoming events
- 📋 Generate short reports
- ⚠️ Highlight actions that require attention

AI functionality is currently a planned feature and is not part of the completed core functionality.

---

# ⚙️ Infrastructure

KennelPro currently uses several architectural components to keep the project modular.

Implemented infrastructure includes:

- ✅ Entity Framework Core
- ✅ SQLite
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ Service Layer
- ✅ MVVM
- ✅ Validators
- ✅ Helper classes
- ✅ HTTP/API infrastructure
- ✅ Authentication infrastructure
- ✅ Database migrations
- ✅ Application logging infrastructure
- ✅ Storage infrastructure
- ✅ Backup infrastructure
- ✅ Cloud infrastructure

---

# 🛠 Technology Stack

| Technology | Purpose |
|---|---|
| **.NET 10** | Application framework |
| **.NET MAUI** | Cross-platform UI |
| **C#** | Main programming language |
| **XAML** | User interface |
| **Entity Framework Core** | ORM / data access |
| **SQLite** | Local database |
| **MVVM** | Application architecture |
| **Repository Pattern** | Data-access abstraction |
| **Dependency Injection** | Service management |
| **HttpClient** | API communication |

---

# 🏗 Architecture

KennelPro follows a layered architecture:

```text
┌─────────────────────┐
│         UI          │
│       XAML          │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│     ViewModels      │
│        MVVM         │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│      Services       │
│   Business Logic    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│    Repositories     │
│   Data Access       │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Entity Framework  │
│       Core          │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│       SQLite        │
│      Database       │
└─────────────────────┘
