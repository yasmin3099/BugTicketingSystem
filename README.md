# 🐞 Bug Ticketing System – API Documentation

Welcome to the Bug Ticketing System API. This documentation outlines the available endpoints for managing users, projects, bugs, and attachments within your software development workflow.

---

## 🔐 User Management

### 🔹 Register User
- **Endpoint:** `POST /api/users/register`
- **Description:** Registers a new user account.

#### 📤 Request Body:
```json
{
  "username": "Mohamed",
  "email": "mo@gmail.com",
  "password": "_Mo1234h",
  "role": "Developer"
}
```

---

### 🔹 Login User
- **Endpoint:** `POST /api/users/login`
- **Description:** Authenticates a user and returns a JWT token.

#### 📤 Request Body:
```json
{
  "email": "mo@gmail.com",
  "password": "_Mo1234h"
}
```

#### 📥 Response:
```json
{
  "token": "jwt_token_here"
}
```

---

## 🗂️ Project Management

### 🔹 Create Project
- **Endpoint:** `POST /api/projects`
- **Description:** Creates a new project.

#### 📤 Request Body:
```json
{
  "name": "latest final api",
  "description": "Api description"
}
```

---

### 🔹 Get All Projects
- **Endpoint:** `GET /api/projects`
- **Description:** Retrieves all projects.

#### 📥 Response:
```json
[
  {
    "projectId": "c8593d13-8420-4f73-a4b5-08dd880129ee",
    "name": "latest final api",
    "description": "Api description"
  }
]
```

---

### 🔹 Get Project Details
- **Endpoint:** `GET /api/projects/:id`
- **Description:** Retrieves detailed information about a specific project.

#### 📥 Response:
```json
{
  "projectId": "c8593d13-8420-4f73-a4b5-08dd880129ee",
  "name": "latest final api",
  "description": "Api description"
}
```

---

## 🐛 Bug Management

### 🔹 Create Bug
- **Endpoint:** `POST /api/bugs`
- **Description:** Creates a new bug related to a specific project.

#### 📤 Request Body:
```json
{
  "Title": "login fail",
  "Description": "new bug description",
  "ProjectId": "c8593d13-8420-4f73-a4b5-08dd880129ee"
}
```

---

### 🔹 Get All Bugs
- **Endpoint:** `GET /api/bugs`
- **Description:** Retrieves all bugs.

#### 📥 Response:
```json
[
  {
    "id": "73adc756-15a9-4150-5c5a-08dd86ce994e",
    "title": "test",
    "description": "testbug"
  }
]
```

---

### 🔹 Get Bug Details
- **Endpoint:** `GET /api/bugs/:id`
- **Description:** Retrieves detailed information about a specific bug.

#### 📥 Response:
```json
{
  "id": "73adc756-15a9-4150-5c5a-08dd86ce994e",
  "title": "test",
  "description": "testbug",
  "project": {
    "projectId": "1ceb8293-467b-4cdf-cd28-08dd85cf79f4",
    "name": "test1",
    "description": "test description"
  }
}
```

---

## 👥 User-Bug Assignment

### 🔹 Assign User to Bug
- **Endpoint:** `POST /api/bugs/:id/assignees`
- **Description:** Assigns a user to a bug.

#### 📤 Request Body:
```json
{
  "userId": "guid"
}
```

---

### 🔹 Remove User from Bug
- **Endpoint:** `DELETE /api/bugs/:id/assignees/:userId`
- **Description:** Removes a user assignment from a bug.

---

## 📎 File Attachments

### 🔹 Upload Attachment
- **Endpoint:** `POST /api/bugs/:id/attachments`
- **Description:** Uploads a file (image or document) to a specific bug.

#### 📤 Form Field:
- `file`: image or document file

---

### 🔹 Get Attachments for Bug
- **Endpoint:** `GET /api/bugs/:id/attachments`
- **Description:** Returns all attachments related to a bug.

#### 📥 Response:
```json
[
  {
    "attachmentId": "e61545ff-b900-4aad-b887-ff7201ecca55",
    "fileName": "Screenshot 2025-03-15 063301.png",
    "filePath": "http://localhost:5240/attachments/1961428b-a5d5-4f07-b062-606564ea1b8b.png"
  }
]
```

---

### 🔹 Delete Attachment
- **Endpoint:** `DELETE /api/bugs/:id/attachments/:attachmentId`
- **Description:** Deletes a specific attachment from a bug.
