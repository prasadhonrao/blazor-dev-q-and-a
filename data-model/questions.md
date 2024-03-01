# Questions Collections:

## Properties

- `_id` (ObjectId, automatically generated unique identifier)
- `title` (string, the title of the question)
- `description` (string, the description/body of the question)
- `dateCreated` (date, the date when the question was created)
- `tags` (array of Tag references)
- `user` (User reference, representing the user who posted the question)
- `votes` (array of User references, representing users who voted on the question)
- `status` (Status reference, representing the current status of the question)
- `notes` (string, admin notes associated with the question)
- `approvedForRelease` (boolean, flag indicating if the question is approved for release)
- `archived` (boolean, flag indicating if the question is archived)

### Example Document:

**Questions:**

```javascript
{
  "_id": ObjectId("5f9e1d1d1c9d440000a13c23"),
  "title": "How to use MongoDB with Node.js?",
  "description": "I'm trying to integrate MongoDB with my Node.js application. Any guidance?",
  "dateCreated": ISODate("2022-01-03T08:00:00Z"),
  "tags": [
    ObjectId("5f9e1d1d1c9d440000a13c12") // Reference to the 'mongodb' tag
  ],
  "user": ObjectId("5f9e1d1d1c9d440000a13c40"), // Reference to the user who posted the question
  "votes": [
    ObjectId("5f9e1d1d1c9d440000a13c40"), // Reference to a user who voted on the question
    ObjectId("5f9e1d1d1c9d440000a13c41") // Another user reference
    // ... (other user references)
  ],
  "status": ObjectId("5f9e1d1d1c9d440000a13c30"), // Reference to the 'Open' status
  "notes": "Admin note: Needs further clarification before approval.",
  "approvedForRelease": false,
  "archived": false
}
```
