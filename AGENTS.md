\# KennelPro — Project Context



\## 1. Project Overview



KennelPro is a professional cross-platform dog kennel management platform.



The application is being developed with:



\- .NET 10

\- .NET MAUI

\- C#

\- Entity Framework Core

\- SQLite

\- MVVM

\- Repository Pattern

\- Dependency Injection

\- HttpClient



Target platforms:



\- Android

\- iOS

\- macOS / MacCatalyst

\- Windows



The main goal is to create a complete digital management system for professional breeders, kennel owners and, later, ordinary dog owners.



\---



\# 2. IMPORTANT DEVELOPMENT RULE



Do NOT rewrite the existing architecture without a strong reason.



Do NOT delete existing models, repositories, interfaces or services simply because another implementation looks cleaner.



Before changing an existing class:



1\. Read the existing implementation.

2\. Check all references to it.

3\. Check interfaces.

4\. Check repositories.

5\. Check Entity Framework relationships.

6\. Check dependency injection.

7\. Only then make changes.



The project already contains a significant amount of implemented infrastructure.



The goal is to FINISH the project, not restart it.



\---



\# 3. Current Development Stage



Current version:



v0.2.0-alpha



Status:



Active Development.



The architecture and backend foundation are mostly implemented.



The current priority is to turn the existing architecture into a working application.



The first major working feature must be authentication.



\---



\# 4. Architecture



The application follows Clean Architecture principles.



General dependency flow:



UI

↓

ViewModels

↓

Services

↓

Repositories

↓

Entity Framework Core

↓

SQLite



Business logic must stay outside XAML pages.



Pages should primarily:



\- display UI;

\- bind to ViewModels;

\- handle navigation when appropriate.



Business operations belong in Services.



Database operations belong in Repositories.



Data validation belongs in Validators.



Reusable utility functionality belongs in Helpers.



\---



\# 5. Project Structure



Expected structure:



KennelPro/



├── Models/

│

├── Data/

│   ├── Database/

│   └── Repositories/

│

├── Interfaces/

│

├── Services/

│

├── Validators/

│

├── Helpers/

│

├── ViewModels/

│   ├── Authentication/

│   ├── Dogs/

│   ├── Kennels/

│   ├── Medical/

│   ├── Litters/

│   ├── Documents/

│   └── Settings/

│

├── Pages/

│   ├── Authentication/

│   ├── Dogs/

│   ├── Kennels/

│   ├── Medical/

│   ├── Litters/

│   └── Documents/

│

├── Resources/

│

├── App.xaml

├── AppShell.xaml

├── MainPage.xaml

└── MauiProgram.cs



Do not assume every folder is complete.



Some components are currently skeletons and must be implemented gradually.



\---



\# 6. Models



The project already contains database models for the main application modules.



Important models include:



\## Authentication



User



Current User contains:



\- Id

\- Email

\- PasswordHash

\- EmailConfirmed

\- KennelId

\- Kennel

\- CreatedAt



The User model may also contain Name if required by the current authentication implementation.



Do not duplicate User models.



\---



\## Kennel



Kennel contains:



\- Id

\- Name

\- LogoPath

\- Country

\- City

\- CreatedAt

\- Users

\- Dogs



\---



\## Dog



Dog contains:



\- Id

\- KennelId

\- Kennel

\- BreedId

\- Breed

\- Name

\- Gender

\- BirthDate

\- Color

\- ChipNumber

\- PedigreeNumber

\- Notes

\- CreatedAt



Dog relationships include:



\- MedicalRecords

\- Documents

\- Titles

\- HeatCycles

\- Matings

\- LittersAsMother

\- LittersAsFather



\---



\## Litter



Litter contains:



\- Id

\- Name

\- BirthDate

\- MotherDogId

\- MotherDog

\- FatherDogId

\- FatherDog

\- Puppies



\---



\# 7. Authentication



Authentication is one of the current priorities.



Existing components include:



\## AuthenticationService



Responsible for:



\- registration;

\- login;

\- logout;

\- checking current session;

\- retrieving current user.



Registration currently creates:



1\. Kennel

2\. User

3\. Password hash

4\. Session



The registration flow must preserve this relationship.



\---



\## PasswordService



Responsible for password hashing and verification.



Do not store plain-text passwords.



Existing implementation uses hashing.



If improving password security, do it carefully and migrate existing hashes if necessary.



Do not silently break existing accounts.



\---



\## SessionManager



Located in:



Helpers/SessionManager.cs



It uses MAUI Preferences.



Responsibilities:



\- SaveUser(Guid userId)

\- GetCurrentUserId()

\- IsLoggedIn()

\- Logout()



Current user session is stored using:



CurrentUserId



Do not replace this with another session mechanism without a reason.



\---



\## VerificationService



Responsible for verification code generation and verification.



It is part of the authentication infrastructure.



\---



\# 8. Authentication Repositories



Existing interface:



IUserRepository



Responsibilities:



\- GetAllAsync()

\- GetByIdAsync(Guid id)

\- GetByEmailAsync(string email)

\- AddAsync(User user)

\- UpdateAsync(User user)

\- DeleteAsync(Guid id)

\- ExistsAsync(Guid id)



Implementation:



UserRepository



\---



Kennel repository:



IKennelRepository



Responsibilities:



\- GetAllAsync()

\- GetByIdAsync(Guid id)

\- AddAsync(Kennel kennel)

\- UpdateAsync(Kennel kennel)

\- DeleteAsync(Guid id)

\- ExistsAsync(Guid id)



Implementation:



KennelRepository



\---



\# 9. ViewModels



BaseViewModel exists.



It implements:



INotifyPropertyChanged



and provides:



SetProperty()



Authentication ViewModels:



\- LoginViewModel

\- RegisterViewModel



Dogs ViewModels:



\- DogListViewModel

\- DogEditViewModel



Some ViewModels may currently be incomplete.



Do not create duplicate ViewModels.



\---



\# 10. Dependency Injection



MauiProgram.cs is the central DI configuration.



Repositories are registered through interfaces.



Services are registered in the service layer.



ViewModels and Pages should also be registered through DI.



Do not instantiate repositories or services manually inside Pages.



Prefer constructor injection.



Example:



AuthenticationService should receive:



\- IUserRepository

\- IKennelRepository

\- PasswordService



through constructor injection.



\---



\# 11. Database



Database:



SQLite



Database file:



KennelPro.db



Location:



MAUI FileSystem.AppDataDirectory



Entity Framework Core manages the database.



DbContext:



AppDbContext



Do not replace SQLite with another database.



Do not introduce SQL Server unless explicitly requested.



\---



\# 12. Current Main Goal



The immediate goal is:



\## Complete Authentication



Required flow:



Application startup

↓

Check SessionManager

↓

If logged in → MainPage

↓

If not logged in → LoginPage

↓

Login

↓

MainPage



Registration:



LoginPage

↓

RegisterPage

↓

Name

Email

Password

Confirm Password

Kennel Name

↓

AuthenticationService.RegisterAsync()

↓

Create Kennel

↓

Create User

↓

Hash Password

↓

Save User

↓

Save Session

↓

MainPage



\---



\# 13. Authentication UI



Pages should be:



Pages/Authentication/



\- LoginPage.xaml

\- RegisterPage.xaml



LoginPage should contain:



\- KennelPro branding

\- Email field

\- Password field

\- Login button

\- Register navigation button



RegisterPage should contain:



\- Name

\- Email

\- Password

\- Confirm Password

\- Kennel Name

\- Create Account button

\- Back/Login navigation



UI should be modern and simple.



The application is intended to be usable by users approximately 30–50+ years old, therefore:



\- large controls;

\- readable text;

\- simple navigation;

\- minimal actions;

\- clear error messages.



Do not overcomplicate the UI.



\---



\# 14. Validation



Validators already exist.



Examples:



\- UserValidator

\- DogValidator

\- KennelValidator

\- LitterValidator



Do not put large validation blocks directly inside Pages.



Validation should be reusable.



\---



\# 15. Helpers



Helpers already exist.



Examples:



\- DateHelper

\- FileHelper

\- ImageHelper

\- ValidationHelper

\- SessionManager



Do not create duplicate utility classes.



Before creating a new helper, check whether an existing helper can be extended.



\---



\# 16. Dog Module



After authentication is fully working, implement the Dog module.



Required flow:



MainPage

↓

Dogs

↓

DogListPage

↓

DogEditPage



Required operations:



\- list dogs;

\- add dog;

\- edit dog;

\- delete dog;

\- view dog;

\- search;

\- filtering later.



Dogs must belong to the current user's kennel.



Never allow a user to accidentally access another kennel's dogs.



\---



\# 17. Medical Module



After Dog module:



\- Medical records

\- Vaccinations

\- Parasite treatments

\- Diseases

\- Medications



Medical records must be associated with dogs.



\---



\# 18. Litter Module



Required:



\- create litter;

\- mother;

\- father;

\- birth date;

\- puppies;

\- litter history.



\---



\# 19. Documents



Required:



\- document metadata;

\- file storage;

\- dog association;

\- kennel association;

\- titles;

\- certificates.



\---



\# 20. Notifications



Later implement reminders for:



\- vaccinations;

\- parasite treatments;

\- medical events;

\- breeding events;

\- important kennel events.



\---



\# 21. Future Features



Do not implement these before core functionality works:



\- cloud synchronization;

\- backup and restore;

\- QR codes;

\- PDF export;

\- localization;

\- veterinary cabinet;

\- analytics;

\- AI assistant.



These are planned features.



\---



\# 22. API



Dog API integration is planned.



Expected future functionality:



\- breed search;

\- breed descriptions;

\- breed photos;

\- characteristics;

\- FCI information where legally available.



Do not invent external API endpoints.



Keep API integration isolated inside Services/API.



\---



\# 23. Coding Rules



Use:



\- async/await;

\- nullable reference types;

\- constructor dependency injection;

\- interfaces for repositories;

\- Entity Framework Core;

\- clear namespaces;

\- meaningful names.



Avoid:



\- static global services;

\- database access from Pages;

\- business logic inside XAML;

\- duplicated models;

\- duplicated services;

\- hard-coded database paths;

\- plain-text passwords;

\- unnecessary dependencies.



\---



\# 24. Before Writing Code



When asked to implement something:



1\. Inspect the existing project.

2\. Find related classes.

3\. Find interfaces.

4\. Find repositories.

5\. Find services.

6\. Find ViewModels.

7\. Find Pages.

8\. Check MauiProgram.cs.

9\. Check AppShell.

10\. Check DbContext and relationships.



Then implement the smallest change required.



\---



\# 25. Important



Do not rewrite working code just for stylistic reasons.



Do not change architecture unless required.



Do not delete existing functionality.



Do not create duplicate classes.



Do not invent files that already exist.



If something is missing, create it.



If something is broken, fix the existing implementation.



If a requirement is ambiguous, inspect the surrounding code before making architectural decisions.



\---



\# 26. Development Order



Follow this order unless explicitly instructed otherwise:



1\. Authentication

2\. Application startup/session

3\. MainPage

4\. Dog management

5\. Medical management

6\. Litters

7\. Documents

8\. Notifications

9\. Settings

10\. API integration

11\. Backup \& Restore

12\. Cloud synchronization

13\. Localization

14\. Analytics

15\. AI Assistant



The goal is always to keep the application buildable and runnable after each stage.



\---



\# 27. Current Task



CURRENT PRIORITY:



Complete the authentication UI and make the authentication flow fully functional.



Do not start cloud synchronization, AI, analytics or other future modules until authentication works.



The application must remain buildable for Android.



Android is currently the primary platform for testing.



