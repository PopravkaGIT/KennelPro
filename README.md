# 🐶 KennelPro

> Professional cross-platform dog kennel management platform built with .NET MAUI.

KennelPro is a modern cross-platform application for professional breeders, kennel owners and dog owners.

The project is focused on creating a complete digital ecosystem for managing dogs, kennels, medical information, breeding records, litters, puppies, documents and future intelligent features.

---

# 📖 About

**KennelPro** is designed to simplify everyday kennel management by bringing important dog-related information into one application.

The application is being developed for:

- 🏡 Professional breeders
- 🐶 Kennel owners
- ❤️ Dog owners
- 🐾 Future pet owners

The long-term goal is to provide one centralized platform for managing:

- dogs;
- kennels;
- medical history;
- vaccinations;
- parasite treatments;
- reproduction;
- litters;
- puppies;
- documents;
- notifications;
- cloud backups;
- analytics;
- future AI assistance.

KennelPro is built with **.NET MAUI**, allowing the application to target multiple platforms from a shared C# and XAML codebase.

The project is designed with scalability and maintainability in mind, using separation between UI, business logic and data-access layers.

---

# 🎯 Project Goals

KennelPro aims to make kennel management easier, safer and more organized.

The main goals are:

- 🐶 Manage dogs from one place
- 🏡 Manage kennel information
- 🩺 Store medical history
- 💉 Track vaccinations
- 💊 Track parasite treatments
- 🧬 Manage reproduction information
- 🐾 Manage litters and puppies
- 📄 Store important documents
- 🏆 Manage titles and pedigree information
- 🔔 Prepare automatic reminders
- ☁ Synchronize data between devices
- 💾 Backup and restore data
- 📊 Provide statistics and analytics
- 🤖 Introduce AI assistance in future versions

---

# ✨ Current Features

## 🔐 Authentication

KennelPro currently includes a working authentication system.

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
- 🔗 User-to-kennel relationship

Authentication data is connected to the current kennel.

This allows KennelPro to isolate data between different kennels and prevent users from accessing another kennel's records.

### Authentication flow

```text
Application Start
       │
       ▼
Session Check
       │
   ┌───┴───┐
   ▼       ▼
Logged    Not Logged
In        In
   │       │
   ▼       ▼
 Main     Login
 Page       │
            ▼
         Register
🏡 Kennel Management

Kennel is one of the main data-isolation boundaries in KennelPro.

During registration, a kennel is automatically created and connected to the user.

The kennel is used to connect:

users;
dogs;
medical records;
reproduction records;
litters;
puppies;
documents;
notifications.

The architecture is designed so that data belonging to different kennels is not mixed.

🐶 Dog Management

Dog management is currently one of the main completed functional modules.

Implemented:

➕ Add dogs
✏️ Edit dogs
🗑 Delete dogs
👁 View dog details
🔎 Search dogs
🐕 Dog list
🧬 Breed selection
⚧ Gender selection
🎂 Birth date
🔢 Microchip number
🏆 Pedigree information
📝 Notes

Dog records are associated with the current kennel.

Before performing operations, the application checks the ownership of the dog through the current user's kennel.

This prevents users from accessing or modifying dogs belonging to another kennel.

Dog management flow
Main Page
    │
    ▼
Dogs
    │
    ├── Add Dog
    │
    ├── Search
    │
    ├── View Details
    │
    ├── Edit
    │
    └── Delete
🩺 Medical Management

KennelPro contains a medical management module for maintaining dog health information.

Implemented foundation:

🩺 Medical records
💉 Vaccinations
💊 Parasite treatments
💊 Medications
🦠 Diseases
📅 Medical dates
📝 Medical notes
➕ Create records
✏️ Edit records
🗑 Delete records
👁 View medical information

Medical information is connected to individual dogs.

The module also applies kennel ownership checks to prevent access to medical information belonging to another kennel.

The medical module is currently functional at the CRUD level, with additional scenario testing and UI improvements planned.

🧬 Reproduction Management

KennelPro contains a reproduction management module designed for breeding-related information.

The backend currently supports:

♀ Heat cycles
🐕 Mating records
🐾 Litters
🐶 Puppies

Implemented backend components include:

models;
repositories;
services;
validation;
kennel isolation;
database relationships;
EF Core migrations.

Supported information includes:

Heat Cycles
start date;
end date;
status;
notes;
dog relationship.
Mating
male dog;
female dog;
mating date;
notes.
Litters
parents;
litter name;
birth date;
notes.
Puppies
name;
puppy number;
gender;
birth date;
chip number;
pedigree number;
notes;
litter relationship.

The reproduction backend is implemented, including security and validation.

The complete user interface and CRUD navigation for all reproduction entities are still being developed.

⚠️ Heat cycle and mating management are currently not presented as fully completed user-facing features.

📄 Documents

KennelPro contains the foundation for document management.

The planned document system will allow users to store and organize important kennel and dog-related documents.

Planned functionality includes:

📄 Documents
🏆 Titles
📎 Dog-related files
🗂 Document organization
📤 Export
📑 PDF generation

The document module is currently under development.

🔔 Notifications

KennelPro contains notification infrastructure for future reminders and important events.

Planned reminders include:

💉 Vaccinations
💊 Parasite treatments
🩺 Medical events
♀ Heat cycles
🧬 Breeding events
🐾 Puppy events
📅 Important dates

The notification system is currently at an early development stage.

🌐 API Infrastructure

KennelPro already contains HTTP/API infrastructure based on HttpClient.

The planned API system will focus on dog breed information.

Future functionality includes:

🐕 Breed search
📖 Breed descriptions
📷 Breed photographs
📊 Breed characteristics
🏆 FCI standards where legally available
🔎 Breed search and filtering

The API infrastructure is currently prepared, while the complete breed API integration is planned for a future phase.

☁ Cloud & Backup

Cloud functionality is part of the long-term roadmap.

Planned features:

☁ Automatic cloud synchronization
💾 Backup
♻️ Data restoration
📱 Multi-device synchronization
🔐 Secure cloud storage

The infrastructure exists, but the complete production-ready cloud synchronization system is not yet finished.

🤖 AI Assistant

AI assistance is planned for future versions of KennelPro.

The future AI assistant may be able to:

💬 Answer user questions
🔎 Search dogs and documents
📊 Analyze kennel data
🔔 Identify important upcoming events
📋 Generate short reports
⚠️ Highlight actions that require attention

AI functionality is currently a planned feature.

🌍 Localization

KennelPro is planned to support multiple languages.

Initial localization targets include:

🇺🇦 Ukrainian
🇬🇧 English

The application architecture is intended to make adding additional languages easier in the future.

🎨 User Interface

The application is being designed with mobile usability in mind.

UI goals include:

📱 Android-first usability
🔘 Large buttons
✍️ Large input fields
🧭 Simple navigation
⚡ Minimal number of actions
☀️ Light theme
🌙 Dark theme
🎨 Customizable color scheme

The current UI is functional, while a more polished visual design is planned for future development stages.

⚙️ Infrastructure

KennelPro currently uses a modular application architecture.

Implemented infrastructure includes:

✅ Entity Framework Core
✅ SQLite
✅ Repository Pattern
✅ Dependency Injection
✅ Service Layer
✅ MVVM
✅ Validators
✅ Helper classes
✅ HTTP/API infrastructure
✅ Authentication infrastructure
✅ Database migrations
✅ Application logging infrastructure
✅ Storage infrastructure
✅ Backup infrastructure
✅ Cloud infrastructure
🛠 Technology Stack
Technology	Purpose
.NET 10	Application framework
.NET MAUI	Cross-platform UI
C#	Main programming language
XAML	User interface
Entity Framework Core	ORM / data access
SQLite	Local database
MVVM	Application architecture
Repository Pattern	Data-access abstraction
Dependency Injection	Service management
HttpClient	API communication
🏗 Architecture

KennelPro follows a layered architecture:

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
│    Data Access      │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Entity Framework    │
│       Core          │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│       SQLite        │
│      Database       │
└─────────────────────┘

The business logic is separated from the UI and data-access layers.

This architecture makes it easier to:

maintain the project;
add new modules;
test individual components;
replace infrastructure components;
scale the application;
support additional platforms.
📂 Project Structure
KennelPro/
│
├── Models/
│   ├── Authentication/
│   ├── Dogs/
│   ├── Kennels/
│   ├── Medical/
│   ├── Reproduction/
│   └── Documents/
│
├── Data/
│   ├── Database/
│   ├── Repositories/
│   ├── Seed/
│   └── Migrations/
│
├── Interfaces/
│   ├── Authentication/
│   ├── Dogs/
│   ├── Kennels/
│   ├── Medical/
│   ├── Reproduction/
│   └── Documents/
│
├── Services/
│   ├── Authentication/
│   ├── Dogs/
│   ├── Medical/
│   ├── Reproduction/
│   ├── Documents/
│   ├── Notifications/
│   ├── Backup/
│   ├── Cloud/
│   └── Api/
│
├── Validators/
│
├── Helpers/
│
├── ViewModels/
│   ├── Authentication/
│   ├── Dogs/
│   ├── Medical/
│   └── Reproduction/
│
├── Pages/
│   ├── Authentication/
│   ├── Dogs/
│   ├── Medical/
│   └── Reproduction/
│
├── Resources/
│
├── App.xaml
├── AppShell.xaml
├── MainPage.xaml
└── MauiProgram.cs
🚧 Development Status
Current Version
v0.3.0-alpha

KennelPro has moved beyond the initial infrastructure stage.

The project currently contains working core functionality for authentication and dog management, a functional medical module, and a partially completed reproduction module.

📊 Current Progress
Module	Status
🏗 Architecture / DI / EF Core / SQLite	~80%
🔐 Authentication & Sessions	~75%
🏡 Main Page / Navigation	~65%
🐶 Dog Management	~60%
🩺 Medical Management	~45%
🧬 Reproduction	~40%
📄 Documents	~25%
🔔 Notifications	~20%
⚙️ Settings	~15%
☁ Cloud / Backup	~10%
🌐 API	~10%
📊 Analytics	~5%
🤖 AI Assistant	~5%

These percentages represent the current development state and are not final feature-completion guarantees.

🗺 Roadmap
✅ Completed / Functional Foundation
✔ Project architecture
✔ Database design
✔ Database models
✔ Entity Framework Core
✔ SQLite integration
✔ Database migrations
✔ Repository Pattern
✔ Repository implementations
✔ Dependency Injection
✔ Service Layer
✔ Validators
✔ Helper classes
✔ Authentication system
✔ Session management
✔ Kennel creation
✔ Dog CRUD
✔ Dog search
✔ Medical CRUD foundation
✔ Reproduction backend foundation
✔ Kennel data isolation
✔ Android build
🚧 Current Development
🔬 Reproduction UI
🧬 Heat cycle UI
🐕 Mating UI
🐾 Litter management UI
🐶 Puppy management UI
🩺 Medical scenario testing
📄 Documents
🔔 Notifications
🎨 UI improvements
📅 Planned
v0.4.x
🧬 Complete reproduction UI
🐾 Complete litter management
🐶 Complete puppy management
📄 Document management
🔔 Notifications
⚙️ Settings
v0.5.x
🌍 Dog breed API
📖 Breed descriptions
📷 Breed images
🔎 Breed search
📊 Breed information
v0.6.x
☁ Cloud synchronization
💾 Backup and restore
📱 Multi-device synchronization
📄 PDF export
📱 QR codes
v0.7.x
🌍 Localization
🇺🇦 Ukrainian
🇬🇧 English
🎨 Advanced themes
📊 Statistics
v1.0.0
🤖 AI Assistant
📊 Advanced analytics
👨‍⚕ Veterinary cabinet
👨‍👩‍👧 Family access
🔗 Dog card sharing
☁ Production cloud infrastructure
🔐 Data Isolation

KennelPro is designed around kennel-based data isolation.

The general security chain is:

SessionManager
      │
      ▼
Current User
      │
      ▼
KennelId
      │
      ▼
Dog
      │
      ▼
Medical / Reproduction / Other Data

Identifiers received from navigation or UI are not treated as trusted ownership information.

Services verify that the requested entity belongs to the current user's kennel before allowing access or modification.

This approach is intended to prevent cross-kennel data access.

🧪 Development & Testing

The project is regularly checked using Android builds.

Example build command:

dotnet build KennelPro.csproj -f net10.0-android -c Debug

The current project has successfully passed Android compilation during development.

Runtime testing on physical devices and emulators is still required for several modules.

⚠️ Known Issues

Current development issues include:

Runtime scenarios still require manual testing.
Reproduction UI is not fully complete.
Breed API integration is not finished.
Notifications are not fully implemented.
Documents are still under development.
Cloud synchronization is not production-ready.
SQLitePCLRaw dependency warnings may appear during NuGet restore/build.
Password hashing should be upgraded to a stronger password-specific algorithm before production release.
🔮 Future Vision

KennelPro aims to become a complete digital ecosystem for dog breeders and owners.

The final platform should make it possible to manage the entire lifecycle of a dog from one application.

Kennel
  │
  ├── Dogs
  │    │
  │    ├── Medical History
  │    ├── Vaccinations
  │    ├── Parasite Treatments
  │    ├── Documents
  │    └── Reproduction
  │
  ├── Litters
  │    └── Puppies
  │
  ├── Notifications
  │
  ├── Cloud Backup
  │
  ├── Analytics
  │
  └── AI Assistant

The long-term goal is to make kennel management:

simpler;
faster;
safer;
more automated;
accessible from multiple devices.
🤝 Contributing

KennelPro is currently an independent development project.

The architecture and codebase are still actively evolving.

Contributions, ideas and technical feedback may be considered as the project develops.

📜 License

KennelPro is a Source-Available commercial project.

The source code is publicly available for viewing and educational purposes.

Commercial use, redistribution, resale, or creating competing products based on this software is prohibited without permission.

All rights reserved.

See LICENSE for details.
