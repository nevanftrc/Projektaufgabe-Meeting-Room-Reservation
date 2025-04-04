# Projektaufgabe - Meeting Room Reservation

## Index
- [Details](#details)
- [Infos](#infos)
- [Requirements](#requirements)
- [Bugs](#bugs)
- [Authors](#authors)

---

## Details

This repository contains all the necessary data and resources for the **Room Reservation API** project. The project is designed to help manage and track room reservations within an organization. It provides functionality for users to efficiently book, update, and delete room reservations.

The API allows the following actions:

- **Create a new reservation**: Users can book a room by providing the required details.
- **View existing reservations**: The system allows users to retrieve a list of current reservations.
- **Update reservation details**: Modifications can be made to an existing reservation if the room, time, or other details need to be changed.
- **Delete reservations**: Users can cancel or delete reservations that are no longer required.

This API is built to be scalable, allowing integration with other systems or platforms if necessary.

---

## Infos

This project is an **API** that manages **Room Reservations**. The API provides endpoints that allow users to book rooms, view bookings, and modify or cancel existing reservations. The following features are provided by the API:

- **Room Information**: Users can specify the room name, capacity, and availability when making a reservation.
- **Start and End Time**: Reservations require the specification of the start and end time for the booking.
- **Reason for Reservation**: Users can include a reason for the booking when creating or updating a reservation.

### Tech Stack

- **.NET 8**: The API is built using **.NET 8**, which provides a modern, high-performance framework for web applications and APIs. .NET 8 includes enhanced support for scalability, security, and performance, making it ideal for handling room reservations efficiently.

### Features

- **Room Management**: The API allows users to create reservations for different rooms, specifying the room details such as its name and capacity.
- **Reservation Conflict Detection**: The system checks for overlapping reservations and prevents conflicting bookings from being created.
- **CRUD Operations**: The API supports **Create**, **Read**, **Update**, and **Delete** operations for room reservations, allowing users to manage their bookings seamlessly.
- **Error Handling**: Proper error responses are provided for common issues, such as conflicting reservations or missing required fields.


### Requirements
This API is built using **MongoDB, Node.js, and React**, with Webpack for module bundling.  

#### **Required Dependencies & Versions**  
- **MongoDB** (Recommended: `v6.0` or later)  
  - Install: [MongoDB Download](https://www.mongodb.com/try/download/community)  
- **Node.js** (Recommended: `v18.x` or later)  
  - Install: [Node.js Download](https://nodejs.org/)  
- **npm** (Comes with Node.js, check version with `npm -v`)  
- **Webpack** (Required for bundling)  
  - Install via npm:  
    ```sh
    npm install -g webpack webpack-cli
    ```
#### **Additionals**
To verfiy access to the db start first the database since it will be genrated in there, change to mongo shell to see if it exists.
over shell or cmd directly -V
```sh
mongosh use RoomRevDB
 db.MeetingRooms.findOne()
```
it should return empty since there are no documents inserted beforehand.
Configs for webpack
```json
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ["babel-loader"]
      },
        {
          test: /\.css$/i,
          use: ["style-loader", "css-loader"],
        },
      ],
  },
```
and addon for css
```sh
npm install --save-dev css-loader
```

## Bugs
- none found

## Authors
- PonixXD  
- nevanftrc
- FelipeOCar
