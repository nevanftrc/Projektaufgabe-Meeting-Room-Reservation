# Projektaufgabe - Meeting Room Reservation

## Index
- [Details](#details)
- [Infos](#infos)
- [Requirements](#requirements)
- [Bugs](#bugs)
- [Authors](#authors)

### Details
This rep contains all data of the project.

### Infos
This is a API that manages Room Reservations.
It uses Net.8

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
```sh
mongosh use RoomRevDB
 db.MeetingRooms.findOne()
```
it should return empty since there are no documents inserted beforehand.

## Bugs
- none to detail yet

## Authors
- PonixXD  
- nevanftrc
