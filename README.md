Endpoints:
Permision: All
  Login user: http://localhost:5000/api/account/login
  Register user: http://localhost:5000/api/account/register
  
Permision: Only Admin
  Get list of users with roles: http://localhost:5000/api/admin/users-with-roles
  Edit user role: http://localhost:5000/api/admin/edit-roles/{username}?roles={roleName},{roleName} (example: edit-roles/Rob?roles=User,Admin)
  Create user (This is not register, this is run by admin. No token given): http://localhost:5000/api/admin/create-user
  Delete user: http://localhost:5000/api/admin/delete-user/{username}
