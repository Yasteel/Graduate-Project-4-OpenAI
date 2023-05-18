# Open AI Integration
---

## List of Todos
- [ ] Chat Interface
- [ ] Text Editing Interface
- [ ] Image Generation Interface

---

### Overall Breakdown

- [ ] Create the views
- [x] Create the models associated with the API request
- [ ] Create the models associated with the API response
- [x] Create an Interface and Service to handle API interaction
- [ ] Create a web API controller to interact with the view and the service
- [ ] Make the UI pretty (After **ALL** functionality is completed)

---

### Chat Interface

- [ ] Makes use of DevExtreme Form to get inputs
- [ ] Either a devExtreme component / regular html+css for displaying messages
- [ ] Side bar to store chat history
- [ ] Store Chat History in Local Storage

---








```JSON
{
  "id": "chatcmpl-7H7FLjpCu9yjSAFOp5VON6bCIVco6",
  "object": "chat.completion",
  "created": 1684313787,
  "model": "gpt-3.5-turbo-0301",
  "usage": {
    "prompt_tokens": 11,
    "completion_tokens": 109,
    "total_tokens": 120
  },
  "choices": [
    {
      "message": {
        "role": "assistant",
        "content": "JSON stands for JavaScript Object Notation, which is a lightweight data interchange format. It is a text-based format that is used to transmit data between a client and a server. JSON can represent simple data types, such as strings and numbers, as well as more complex data structures, such as arrays and nested objects. It is widely used in web services and APIs to exchange data in a standardized format that is easy to read and parse. JSON is also language-independent, meaning it can be used by any programming language that supports JSON encoding and decoding."
      },
      "finish_reason": "stop",
      "index": 0
    }
  ]
}
```