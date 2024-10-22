# Praca dyplomowa system d&d

## Description
System fo assist players in managing the flow of gameplay in D&D.

## Goal
The aim of the project is to assist players in managing the flow of gameplay in D&D 5e by developing an IT system consisting of a web application and a database. The system is intended to support players of TTRPG games and is designed with the fifth edition of Dungeons & Dragons in mind.

### Technologies
- **Frontend**: React, Typescript
- **Backend**: C# Asp .net 
- **Database**: Microsoft SQL or PostgreSQL (using Entity framework)

FIGMA LINK: https://www.figma.com/file/3K8swnK4xG7Mmle2jC6Mcn/In%C5%BCynierka-prototyp?type=design&node-id=214%3A4786&mode=design&t=DhvcnQP63MMple4K-1

## How to start
### Backend:
- VSC: Please write in the terminal: dotnet run watch
- Raider: Press the button to start :)

### Frontend:
- Please write in the terminal: npm run dev

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

