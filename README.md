# Praca dyplomowa system d&d

## Description

Todo

FIGMA LINK: https://www.figma.com/file/3K8swnK4xG7Mmle2jC6Mcn/In%C5%BCynierka-prototyp?type=design&node-id=214%3A4786&mode=design&t=DhvcnQP63MMple4K-1

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

None
