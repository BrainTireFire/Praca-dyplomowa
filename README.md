# Praca dyplomowa system TTRPG

## Description
System fo assist players in managing the flow of gameplay in a TTRPG game.

## Goal
The aim of the project is to assist players in managing the flow of gameplay by developing an IT system consisting of a web application and a database. The system is intended to support players of TTRPG games.

### Technologies
- **Frontend**: React, Typescript
- **Backend**: C# Asp .net 
- **Database**: Microsoft SQL or PostgreSQL (using Entity framework)

FIGMA LINK: https://www.figma.com/file/3K8swnK4xG7Mmle2jC6Mcn/In%C5%BCynierka-prototyp?type=design&node-id=214%3A4786&mode=design&t=DhvcnQP63MMple4K-1

## How to start

### Database:
1: Build and Start Services
From the root of the project (where the docker-compose.yml file is located), run:
  docker-compose up -d

### Backend (.NET)

#### Running Locally (without Docker)
1. Open a terminal in the backend project directory.
2. Start the backend with hot-reload:
   ```bash
   dotnet watch run
If you're using JetBrains Rider, you can also press the ▶ Run button.

### Frontend (React / Angular / etc.)

#### Running Locally (without Docker)
1. Open a terminal in the frontend project directory.
2. Start the development server:
   ```bash
   npm run dev

## Endpoints

### Permission: All

### Login user

- **Endpoint:** http://localhost:5000/api/account/login
- **Description:** Allows login for all users.

### Register user

- **Endpoint:** http://localhost:5000/api/account/register
- **Description:** Allows user registration for all users.

### Permission: Only Admin

#### Get list of users with roles

- **Endpoint:** http://localhost:5000/api/admin/users-with-roles
- **Description:** Retrieves a list of users along with their roles. Requires admin permission.

#### Edit user role

- **Endpoint:** http://localhost:5000/api/admin/edit-roles/{username}?roles={roleName},{roleName}
- **Example:** edit-roles/Rob?roles=User,Admin
- **Description:** Edits the roles of a user. Requires admin permission.

#### Create user (Admin-only, no token given)

- **Endpoint:** http://localhost:5000/api/admin/create-user
- **Description:** Creates a new user. This endpoint is intended for admin use only.

#### Delete user

- **Endpoint:** http://localhost:5000/api/admin/delete-user/{username}
- **Description:** Deletes a user. Requires admin permission.

## Tests

- **Postman Collection:** [Postman Collection](https://github.com/BrainTireFire/Praca-dyplomowa/tree/main/Resource)

## Notes

## ONLY DEV
json server: npx json-server db.json

None

## Dev
- dotnet ef migrations add inheritanceInEffectInstance2 --output-dir Data/Migrations --context AppDbContext
- dotnet ef database update --context AppDbContext

