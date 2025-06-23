# 🦷 Dental Clinic Management System

A modern web-based Dental Clinic Management System designed to streamline and digitize the operations of dental clinics through digital transformation.

---

## ✨ Features

* 🦷 **Interactive Dental Map** to track treatments for each tooth based on patient age.
* 📅 **Appointment Management** with scheduling, editing, canceling, and reminders.
* 📂 **Patient Records Management** (medical history, treatment logs).
* 💳 **Financial Management** .
* 🔐 **Secure Authentication** with JWT, password reset, and role-based authorization.
* 🌐 **Multilingual Support** (English and Arabic switchable UI).
* 📱 **Responsive Design** for desktop, tablet, and mobile.

---

## 🛠 Tech Stack

| Category            | Technologies                        |
| ------------------- | ----------------------------------- |
| **Frontend**        | React.js, Material UI, TypeScript   |
| **Backend**         | ASP.NET Core, Entity Framework Core |
| **Database**        | Microsoft SQL Server                |
| **Authentication**  | JWT (JSON Web Token)                |
| **Version Control** | Git, GitHub                         |

---

## 🚀 Future Enhancements

* Add a **Laboratories Section** for managing lab-related services and reports.
* Integrate **AI Smile Simulation** to visualize orthodontic results for patients.

---

# 🦷 Dental Clinic Management API Documentation

---

## 🔐 Authentication (Auth)

### POST `/api/auth/login`

* **Description**: Logs in the user and returns a JWT token.
* **Request Body**:

```json
{
  "email": "user@example.com",
  "password": "string"
}  


## 🔐 Authentication

### POST `/api/auth/login`
Logs in the user and returns a JWT token.

**Response**
```json
{
  "token": "jwt-token-value"
}
```

---

### POST `/api/auth/forgot-password`
Sends a reset link to user's email.

**Request**
```json
{
  "email": "user@example.com"
}
```

---

### POST `/api/auth/reset-password`
Resets the password using a valid token.

**Request**
```json
{
  "token": "reset-token",
  "newPassword": "string",
  "confirmPassword": "string"
}
```

---

## 👑 Admin

### GET `/api/admin/profile`
Retrieves current admin profile.

**Response**
```json
{
  "id": "guid",
  "fullName": "string",
  "email": "string",
  "phoneNumber": "string"
}
```

---

### PATCH `/api/admin/{id}`
Updates admin info.

**Request**
```json
{
  "fullName": "string",
  "email": "string",
  "phoneNumber": "string"
}
```

---

### PATCH `/api/admin/changePassword`
Changes the admin's password.

**Request**
```json
{
  "currentPassword": "string",
  "newPassword": "string"
}
```

**Response**
```json
{
  "message": "Password changed successfully",
  "requireReLogin": true
}
```

---

## 👩‍💼 Customer Service

### POST `/api/customerService`
Adds a new customer service user. (Admin only)

**Request**
```json
{
  "fullName": "string",
  "workingTime": "string",
  "email": "string",
  "phoneNumber": "string"
}
```

---

### GET `/api/customerService`
Retrieves all customer service users.

**Response**
```json
[
  {
    "id": "guid",
    "fullName": "string",
    "workingTime": "string",
    "email": "string",
    "phoneNumber": "string",
    "isActive": true
  }
]
```

---

### GET `/api/customerService/{id}`
Gets details for a specific customer service user.

---

### PATCH `/api/customerService/{id}`
Updates a customer service user.

---

### DELETE `/api/customerService/{id}`
Deletes a customer service user.

---

### PATCH `/api/customerService/changePassword`
Changes password (CustomerService role only).

**Request**
```json
{
  "currentPassword": "string",
  "newPassword": "string"
}
```

---

## 👨‍⚕️ Doctor

### POST `/api/doctor`
Adds a new doctor. (Admin only)

**Request**
```json
{
  "fullName": "string",
  "workingTime": "string",
  "email": "string",
  "phoneNumber": "string",
  "specialization": "string",
  "colorCode": "#RRGGBB"
}
```

**Response**
```json
{
  "doctorId": "guid"
}
```

---

### GET `/api/doctor`
Retrieves all doctors.

**Response**
```json
[
  {
    "id": "guid",
    "fullName": "string",
    "workingTime": "string",
    "email": "string",
    "phoneNumber": "string",
    "specialization": "string",
    "isActive": true,
    "colorCode": "#RRGGBB"
  }
]
```

---

### GET `/api/doctor/{id}`
Retrieves details for a doctor by ID.

---

### PATCH `/api/doctor/{id}`
Updates a doctor's details.

---

### DELETE `/api/doctor/{id}`
Deletes a doctor by ID.

---

### PATCH `/api/doctor/changePassword`
Doctor changes their password.

**Request**
```json
{
  "currentPassword": "string",
  "newPassword": "string"
}
```

---

### GET `/api/doctor/search?name=...&phoneNumber=...`
Searches for doctors by name or phone number.

---

### GET `/api/doctor/filter?specialization=...`
Filters doctors by specialization.

---

## 💼 Treatment

### POST `/api/treatment`
Adds a new treatment. (Admin only)

**Request**
```json
{
  "name": "string",
  "price": 0,
  "category": "string"
}
```

**Response**
```json
{
  "treatmentId": "guid"
}
```

---

### GET `/api/treatment`
Retrieves all treatments.

**Response**
```json
[
  {
    "id": "guid",
    "name": "string",
    "category": "string"
  }
]
```

---

### GET `/api/treatment/{id}`
Retrieves treatment details by ID.

---

### PATCH `/api/treatment/{id}`
Updates a treatment.

**Request**
```json
{
  "name": "string",
  "category": "string",
  "price": 0
}
```

---

### DELETE `/api/treatment/{id}`
Deletes a treatment.

---

### GET `/api/treatment/search?name=...`
Searches for treatments by name.

---

### GET `/api/treatment/filter?category=...&price=...`
Filters treatments by category and/or price.

