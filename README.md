# Bug Ticketing System

## Overview
The **Bug Ticketing System** is a web application that helps teams manage bugs and issues in software projects. It enables users (Managers, Developers, and Testers) to track, report, and resolve bugs effectively. The system allows users to create, view, and manage bugs, handle user accounts, and manage attachments related to bugs.

## Key Components

1. **Users**
   - Each user (Manager, Developer, Tester) can have multiple roles.
   - Users may be assigned to various bugs.

2. **Projects**
   - Each project contains multiple bugs.
   - Each bug belongs to one specific project.

3. **Bugs**
   - Bugs can have multiple assignees.
   - Bugs can have attachments such as images.

4. **Attachments**
   - Each attachment is linked to a specific bug.

## Main Functions and API Endpoints

### User Management
- **Register User:** Create a new user account.  
  `POST /api/users/register`

- **Login User:** Authenticate user and provide a token.  
  `POST /api/users/login`

### Project Management
- **Create Project:** Add a new project.  
  `POST /api/projects`

- **Get All Projects:** List all projects.  
  `GET /api/projects`

- **Get Project Details:** View specific project information and bugs.  
  `GET /api/projects/:id`

### Bug Management
- **Create Bug:** Report a new bug.  
  `POST /api/bugs`

- **Get All Bugs:** List all bugs.  
  `GET /api/bugs`

- **Get Bug Details:** View detailed info on a specific bug.  
  `GET /api/bugs/:id`

### User-Bug Relationships
- **Assign User to Bug:** Assign a user to a bug.  
  `POST /api/bugs/:id/assignees`

- **Remove User from Bug:** Unassign a user from a bug.  
  `DELETE /api/bugs/:id/assignees/:userId`

### File Management
- **Upload Attachment:** Add an attachment to a bug.  
  `POST /api/bugs/:id/attachments`

- **Get Attachments for Bug:** Retrieve all attachments for a bug.  
  `GET /api/bugs/:id/attachments`

- **Delete Attachment:** Remove an attachment from a bug.  
  `DELETE /api/bugs/:id/attachments/:attachmentId`

