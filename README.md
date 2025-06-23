# 🦷 Dental Clinic Management System

A modern web-based Dental Clinic Management System designed to streamline and digitize the operations of dental clinics through digital transformation.

---

## ✨ Features

- 🦷 **Interactive Dental Map** to track treatments for each tooth based on patient age.
- 📅 **Appointment Management** with scheduling, editing, canceling, and reminders.
- 📂 **Patient Records Management** (medical history, treatment logs).
- 💳 **Financial Management**
- 🔒 **Secure Authentication** with JWT, password reset, and role-based authorization.
- 🌐 **Multilingual Support** (English and Arabic switchable UI).
- 📱 **Responsive Design** for desktop, tablet, and mobile.

---

## 🛠 Tech Stack

| Category      | Technologies |
| ------------- | ------------ |
| **Frontend**  | React.js, Material UI, TypeScript |
| **Backend**   | ASP.NET Core, Entity Framework Core |
| **Database**  | Microsoft SQL Server |
| **Authentication** | JWT (JSON Web Token) |
| **Version Control** | Git, GitHub |

---

## 🚀 Future Enhancements

- Add a **Laboratories Section** for managing lab-related services and reports.
- Integrate **AI Smile Simulation** to visualize orthodontic results for patients.

---

## 📘 API Documentation

### 🔐 Authentication (Auth)

#### POST `/api/auth/login`
- Logs in the user and returns a JWT token.

#### POST `/api/auth/forgot-password`
- Sends a reset link to user's email.

#### POST `/api/auth/reset-password`
- Resets the password using a valid token.

---

### 🧑‍🎼 Admin

#### GET `/api/admin/profile`
- Retrieves current admin profile.

#### PATCH `/api/admin/{id}`
- Updates admin info.

#### PATCH `/api/admin/changePassword`
- Changes the admin's password.

---

### 👩‍💼 Customer Service

#### POST `/api/customerService`
- Adds a new customer service user. *(Admin only)*

#### GET `/api/customerService`
- Retrieves all customer service users. *(Admin only)*

#### GET `/api/customerService/{id}`
- Gets details for a specific customer service user.

#### PATCH `/api/customerService/{id}`
- Updates a customer service user.

#### DELETE `/api/customerService/{id}`
- Deletes a customer service user.

#### PATCH `/api/customerService/changePassword`
- Changes password (CustomerService role only).

---

### 👨‍⚕️ Doctor

#### POST `/api/doctor`
- Adds a new doctor. *(Admin only)*

#### GET `/api/doctor`
- Retrieves all doctors. *(Admin only)*

#### GET `/api/doctor/{id}`
- Retrieves details for a doctor by ID.

#### PATCH `/api/doctor/{id}`
- Updates a doctor's details.

#### DELETE `/api/doctor/{id}`
- Deletes a doctor by ID.

#### PATCH `/api/doctor/changePassword`
- Doctor changes their password.

#### GET `/api/doctor/search?name=...&phoneNumber=...`
- Searches for doctors by name or phone number.

#### GET `/api/doctor/filter?specialization=...`
- Filters doctors by specialization.

---

### 💊 Treatment

#### POST `/api/treatment`
- Adds a new treatment. *(Admin only)*  
- Request Body:
```json
{
  "name": "Filling",
  "price": 50,
  "category": "Restorative"
}







