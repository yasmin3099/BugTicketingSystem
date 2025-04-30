# 🐞 Bug Ticketing System – API Documentation

## 🔐 User Management

### 🔸 Register User  
**Endpoint:** `POST /api/users/register`  
Registers a new user account.

**Request Body:**
```json
{
   "username":"Mohamed",
   "email":"mo@gmail.com",
   "password":"_Mo1234h",
   "role":"Developer"
}
```

---

### 🔸 Login User  
**Endpoint:** `POST /api/users/login`  
Authenticates a user and returns a JWT token.

**Request Body:**
```json
{
  "email":"mo@gmail.com",
  "password":"_Mo1234h",
}
```

**Response:**
```json
{
  "token": "jwt_token_here"
}
```

---

## 🗂️ Project Management

### 🔸 Create Project  
**Endpoint:** `POST /api/projects`  
Creates a new project.

**Request Body:**
```json
{
  "name": "latest final api",
  "description": "Api description"
}
```

---

### 🔸 Get All Projects  
**Endpoint:** `GET /api/projects`  
Returns a list of all projects.

**Response:**
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

### 🔸 Get Project Details  
**Endpoint:** `GET /api/projects/:id`  
Returns detailed info about a specific project including its bugs.

**Response:**
```json
{
    "projectId": "c8593d13-8420-4f73-a4b5-08dd880129ee",
    "name": "latest final api",
    "description": "Api description"
}
```

---

## 🐛 Bug Management

### 🔸 Create Bug  
**Endpoint:** `POST /api/bugs`  
Creates a new bug.

**Request Body:**
```json
{
  "Title":"login fail",
  "Description":"neww bug description",
  "ProjectId":"c8593d13-8420-4f73-a4b5-08dd880129ee"
}
```

---

### 🔸 Get All Bugs  
**Endpoint:** `GET /api/bugs`  
Returns all bugs.

**Response:**
```json
[
  {
 "id": "73adc756-15a9-4150-5c5a-08dd86ce994e",
 "title": "test",
 "description": "testbug",
  } 
]
```

---

### 🔸 Get Bug Details  
**Endpoint:** `GET /api/bugs/:id`  
Returns detailed info about a specific bug.

**Response:**
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

### 🔸 Assign User to Bug  
**Endpoint:** `POST /api/bugs/:id/assignees`  
Assigns a user to a bug.

**Request Body:**
```json
{
  "userId": "guid"
}
```

---

### 🔸 Remove User from Bug  
**Endpoint:** `DELETE /api/bugs/:id/assignees/:userId`  
Unassigns a user from a bug.

---

## 📎 File Attachments

### 🔸 Upload Attachment  
**Endpoint:** `POST /api/bugs/:id/attachments`  
Uploads a file to a bug (`multipart/form-data`).

**Form Field:**
- `file`: image or document file

---

### 🔸 Get Attachments for Bug  
**Endpoint:** `GET /api/bugs/:id/attachments`  
Returns all attachments for a bug.

**Response:**
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

### 🔸 Delete Attachment  
**Endpoint:** `DELETE /api/bugs/:id/attachments/:attachmentId`  
Removes an attachment from a bug.
