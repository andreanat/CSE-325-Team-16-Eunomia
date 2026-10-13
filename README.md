# Eunomia — Personal Media Activity Tracker
**CSE 325: .NET Software Development — Team 16 Final Project**  

---

## Overview

**Eunomia** is a full-stack web application designed and built with **.NET 10 Blazor Interactive Server** and **PostgreSQL**. It serves as a personal media journal that allows users to catalog, evaluate, and reflect upon what they watch, read, play, and listen to across five core categories: **Movies**, **TV Shows**, **Books**, **Music**, and **Games**.

### Quick Links
- **Live Cloud Deployment**: [https://cse-325-team-16-eunomia.onrender.com/](https://cse-325-team-16-eunomia.onrender.com/)
- **GitHub Repository**: [https://github.com/mrmister44/CSE-325-Team-16-Eunomia](https://github.com/mrmister44/CSE-325-Team-16-Eunomia)
- **Trello Project Board**: *(Insert team Trello link)*
- **YouTube Video Demonstration**: *(Insert team YouTube link)*

---

## Key Features

### 1. User Authentication & Session Security
- **Authentication Default**: Unauthenticated visitors are automatically routed to the **Log In** page (`/login`) before they can access personal journal content.
- **Member Registration** (`/register`):
  - Captures unique **Username**, **First Name**, **Last Name**, valid **Email Address**, and **Password**.
  - Client- and server-side validation rules with immediate user feedback.
  - Prevents duplicate usernames and duplicate emails with database-level uniqueness enforcement.
- **Secure Password Handling**:
  - Passwords are **never stored in plaintext**.
  - All credentials are encrypted, salted, and hashed using **BCrypt** (`workFactor: 11`).
- **Session Management**:
  - Dynamic navigation menu updates in real time to show the signed-in user's name and `@username`.
  - Secure **Log Out** button clears the session and returns the user to the login screen.
- **10 Pre-Seeded Evaluation Accounts**:
  - The login interface includes an interactive **Test & Grading Hub** that allows instructors and peers to autofill credentials for any of the 10 pre-loaded demo accounts with a single click.

### 2. Personal Media Journal (Full CRUD)
- **Create**: Add new media titles with customizable categories, reading/watching statuses, 1–10 star ratings, personal reflections/notes, and started/completed dates.
- **Read**: View personal collections formatted as cards displaying title, category, reading status, rating, and notes.
- **Update**:
  - Quick-update reading status directly from the card dropdown.
  - Comprehensive edit modal to modify title, category, notes, rating, and tracking dates.
- **Delete**: Confirmation modal protecting against accidental deletion before permanently removing an entry from the database.

### 3. Dynamic Dashboard & Real-Time Statistics
- **Live Database Aggregations**:
  - **Finished / Completed** count
  - **In Progress / Currently Enjoying** count
  - **On the List / Want to Try** count
  - Total collection titles counter
- **Search & Filtering**:
  - Real-time text search across titles and personal notes.
  - Category filtering (All, Movies, TV Shows, Books, Music, Games).
  - Status filtering with interactive stat tiles.
  - Sorting options: Title (A-Z), Rating (High-to-Low), or Recently Added.

---

## 10 Pre-Loaded Test Accounts (Quick Evaluation Guide)

The PostgreSQL database is pre-seeded with 10 user accounts with BCrypt-hashed passwords for instant testing:

| # | Username | Name | Email | Password |
|---|---|---|---|---|
| 1 | `alex.morgan` | Alex Morgan | `alex.morgan@eunomia.app` | `Password123!` |
| 2 | `sarah.chen` | Sarah Chen | `sarah.chen@eunomia.app` | `Password123!` |
| 3 | `marcus.vance` | Marcus Vance | `marcus.vance@eunomia.app` | `Password123!` |
| 4 | `elena.rostova` | Elena Rostova | `elena.rostova@eunomia.app` | `Password123!` |
| 5 | `jordan.lee` | Jordan Lee | `jordan.lee@eunomia.app` | `Password123!` |
| 6 | `maya.patel` | Maya Patel | `maya.patel@eunomia.app` | `Password123!` |
| 7 | `liam.connor` | Liam Connor | `liam.connor@eunomia.app` | `Password123!` |
| 8 | `chloe.bennett` | Chloe Bennett | `chloe.bennett@eunomia.app` | `Password123!` |
| 9 | `diego.ramirez` | Diego Ramirez | `diego.ramirez@eunomia.app` | `Password123!` |
| 10 | `zara.ahmed` | Zara Ahmed | `zara.ahmed@eunomia.app` | `Password123!` |

> **Tip**: On the [`/login`](https://cse-325-team-16-eunomia.onrender.com/login) page, click the **"Fill"** button next to any account in the side panel to instantly populate the form, then click **Sign In**.

---

## Technology Stack

- **Application Framework**: [.NET 10 SDK](https://dotnet.microsoft.com/)
- **UI Architecture**: ASP.NET Core Blazor (Interactive Server Mode)
- **Database**: PostgreSQL (Hosted on Render Cloud)
- **Object-Relational Mapping (ORM)**: Entity Framework Core 10 (`Npgsql.EntityFrameworkCore.PostgreSQL`)
- **Cryptography & Security**: `BCrypt.Net-Next` (adaptive salt and key derivation)
- **Front-End Styling**: Bootstrap 5 + Custom CSS (editorial aesthetic, responsive layout)
- **Containerization & Deployment**: Docker multi-stage build running on Render Web Services

---

## Project Structure

```text
CSE-325-Team-16-Eunomia/
├── Components/
│   ├── App.razor                  # Root HTML document and script bootstrap
│   ├── Routes.razor               # Blazor router and route table
│   ├── _Imports.razor             # Global razor namespace imports
│   ├── Layout/
│   │   ├── MainLayout.razor       # Base layout with authentication guard
│   │   ├── NavMenu.razor          # Responsive sidebar navigation with session indicator
│   │   └── ReconnectModal.razor   # Blazor Server circuit reconnection dialog
│   └── Pages/
│       ├── Home.razor             # Protected media library & dashboard
│       ├── Login.razor            # Member authentication with test account hub
│       ├── Register.razor         # User account registration with validation
│       ├── Error.razor            # Production error display
│       └── NotFound.razor         # 404 page handler
├── Data/
│   ├── AppDbContext.cs            # EF Core DbContext for Users and MediaItems
│   └── DbInitializer.cs           # Database migration runner & 10 demo user seeder
├── Models/
│   ├── UserAccount.cs             # User entity (Username, Name, Email, PasswordHash)
│   ├── MediaItem.cs               # Media journal entry entity
│   ├── LoginModel.cs              # Authentication form model
│   └── RegisterModel.cs           # Registration form model with DataAnnotations
├── Services/
│   ├── IUserService.cs            # Authentication & account service contract
│   ├── UserService.cs             # Registration, BCrypt verify, and user queries
│   ├── IMediaService.cs           # Media CRUD and dashboard stat queries contract
│   ├── MediaService.cs            # PostgreSQL persistence implementation for media
│   └── UserSessionService.cs      # Scoped state service managing logged-in user circuit
├── Properties/
│   └── launchSettings.json        # Local development profiles
├── appsettings.json               # Database connection string and logging settings
├── Dockerfile                     # Multi-stage container build for Render CI/CD
└── Program.cs                     # Service registrations, DB resolution, and middleware pipeline
```

---

## Getting Started (Local Development)

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later installed.
- Git.

### 1. Clone the Repository
```bash
git clone https://github.com/mrmister44/CSE-325-Team-16-Eunomia.git
cd CSE-325-Team-16-Eunomia
```

### 2. Configure Database Connection
By default, [`appsettings.json`]
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=dpg-db5dl78473hc73apcogg-a.ohio-postgres.render.com;Port=5432;Database=eunomia_db;Username=eunomia_db_user;Password=Gg6SmwMr8pgSlERo2tScoFeU4SnOLnOP;SslMode=Require"
  }
}
```
*Note: In production on Render, the application automatically reads the cloud-injected `DATABASE_URL` environment variable.*

### 3. Run the Application
```bash
dotnet restore
dotnet build
dotnet run
```
Open your browser to the local URL displayed in the terminal (typically `http://localhost:5000` or `https://localhost:7000`).

---

## End-User Guide (How to Use the Application)

1. **Accessing the App**:
   - Navigate to the application URL. If not logged in, you will be redirected to [`/login`](https://cse-325-team-16-eunomia.onrender.com/login).
2. **Signing In**:
   - Enter your username and password, or click **"Fill"** on any of the 10 demo accounts listed in the right panel and click **Sign in**.
3. **Registering a New Account**:
   - Click **"Create an account"** or go to [`/register`](https://cse-325-team-16-eunomia.onrender.com/register).
   - Enter a unique username, your first and last name, a valid email address, and a password (minimum 6 characters).
   - Upon submitting, your account is saved to PostgreSQL and you are logged into your dashboard.
4. **Navigating the Dashboard**:
   - The summary tiles at the top display counts of **Finished**, **In Progress**, and **Want to try** titles. Clicking any tile filters the list to that status.
5. **Adding a Title**:
   - Click the **"+ Add a title"** button.
   - Enter the title, select the category, assign an initial status, give an optional rating (1–10), and write personal notes.
   - Click **Save title** to record it in the database.
6. **Editing or Updating Status**:
   - Quick change: Select a new status from the dropdown on any media card.
   - Deep edit: Click **"Edit details"** on a card to revise ratings, notes, or dates.
7. **Viewing Details & Deletion**:
   - Click **"View details"** to open a full editorial inspection modal.
   - Click **"Delete"** and confirm in the modal to permanently remove an entry.
8. **Logging Out**:
   - Click the **"🚪 Log out"** button in the sidebar navigation to end your session.

---

## Design, Usability & Accessibility Standards

- **Responsive Design**: Fluid layout configured with CSS Grid, Flexbox, and Bootstrap 5 to guarantee accessibility on mobile phones, tablets, and desktop displays.
- **Accessibility (WCAG 2.2 AA)**:
  - Accessible form labeling with explicit `<label for="...">` associations and `aria-describedby` error bindings.
  - High-contrast visual hierarchy designed to avoid eye strain while meeting contrast requirements.
  - Dialog modals include ARIA landmark roles (`role="dialog"`, `aria-modal="true"`, `aria-labelledby`).
- **Cohesive Branding**: Editorial paper theme (`#fffefa` panels, `#28634f` forest green accent, serif typography for headings) offering an intentional aesthetic suited for a media journal.

