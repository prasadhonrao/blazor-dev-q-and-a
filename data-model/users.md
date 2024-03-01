# Users Collection:

## Properties

- `_id` (ObjectId, automatically generated unique identifier)
- `objectId` (string, Azure B2C object ID)
- `username` (string, the username of the user)
- `firstname` (string, the first name of the user)
- `lastname` (string, the last name of the user)
- `displayname` (string, the display name of the user)
- `email` (string, the email address of the user)
- `questions` (array of Question references)
- `votes` (array of Vote references)

### Example Document:

```json
{
  "_id": ObjectId("5f9e1d1d1c9d440000a13c40"),
  "objectId": "b2c123456789",
  "username": "john_doe",
  "firstname": "John",
  "lastname": "Doe",
  "displayname": "John Doe",
  "email": "john.doe@example.com",
  "questions": [
    ObjectId("5f9e1d1d1c9d440000a13c23") // Reference to a question authored by the user
  ],
  "votes": [
    ObjectId("5f9e1d1d1c9d440000a13c23") // Reference to a question the user has voted on
  ],
}
```
