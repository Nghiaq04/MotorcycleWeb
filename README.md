# 🏍️ Motorcycle E-Commerce Website (ASP.NET MVC)

## 📌 Giới thiệu
Đây là dự án **Website thương mại điện tử bán xe máy** được xây dựng bằng **ASP.NET MVC**.

Hệ thống cho phép người dùng:
- Tìm kiếm và so sánh sản phẩm
- Đặt hàng online
- Quản lý đơn hàng cá nhân

Ngoài ra, hệ thống còn cung cấp **trang quản trị (Admin)** để quản lý sản phẩm, danh mục và theo dõi doanh thu.

---

## 🖼️ Giao diện

### 🏠 Trang chủ
![Home](images/home.png)

### 🛒 Trang sản phẩm
![Products](images/products.png)

### ⚖️ So sánh sản phẩm
![Compare](images/compare.png)

### 📊 Báo cáo doanh thu
![Report](images/report.png)

---

## 📧 Chức năng Email

### 🔑 Quên mật khẩu
Gửi email chứa link đặt lại mật khẩu

![Reset Password](images/reset-password.png)

---

### ✅ Xác nhận đặt hàng thành công (User)
Email gửi cho khách hàng sau khi đặt hàng

![Order Confirmation](images/order-confirm.png)

---

### 🔔 Thông báo đơn hàng mới (Admin)
Email gửi cho admin khi có đơn hàng mới

![New Order Notification](images/order-admin.png)

---

## 🚀 Chức năng

### 👤 Người dùng
- 🔐 Đăng ký / Đăng nhập
- 🔎 Tìm kiếm sản phẩm
- ⚖️ So sánh sản phẩm
- 🛒 Giỏ hàng & đặt hàng
- 📦 Xem lịch sử đơn hàng
- 📧 Nhận email xác nhận đơn hàng
- 🔑 Quên mật khẩu (gửi mail reset)

---

### 🛠️ Admin
- 📦 CRUD sản phẩm
- 🗂️ CRUD danh mục
- 📊 Báo cáo doanh thu (Chart.js)

---

## 🏗️ Công nghệ sử dụng

- ASP.NET MVC (.NET Framework)
- SQL Server
- Entity Framework
- Bootstrap, JavaScript
- Chart.js
- SMTP (Gmail)

---

## 📂 Cấu trúc thư mục


FashionWeb/
│
├── Controllers/
├── Models/
├── Views/
├── Areas/
│ └── Admin/
│ ├── Controllers/
│ ├── Views/
│
├── Content/
├── Scripts/
├── App_Start/
├── Web.config
