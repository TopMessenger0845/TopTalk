# Command Protocol Documentation

This document provides an overview of all available commands, organized by category, and pairs corresponding `Request` and `Response` commands.

## Chats

| Request Command               | Response Command              |
|-------------------------------|-------------------------------|
| CreateChatRequest.cs | CreateChatResponse.cs |
| DeleteChatRequest.cs | DeleteChatResponse.cs |
| InviteUserRequest.cs | InviteUserResponse.cs |
| SubscriptionRequest.cs | SubscriptionResponse.cs |

## Contacts

| Request Command               | Response Command              |
|-------------------------------|-------------------------------|
| AddContactRequest.cs | AddContactResponse.cs |
| DeleteContactRequest.cs | DeleteContactResponse.cs |

## Messages

| Request Command               | Response Command              |
|-------------------------------|-------------------------------|
| DeleteMessageRequest.cs | DeleteMessageResponse.cs |
| EditMessageRequest.cs | EditMessageResponse.cs |
| SendMessageRequest.cs | SendMessageResponse.cs |

## Users

| Request Command               | Response Command              |
|-------------------------------|-------------------------------|
| ChangePasswordRequest.cs | ChangePasswordResponse.cs |
| LoginRequest.cs | LoginResponse.cs |
| RegisterRequest.cs | RegisterResponse.cs |

### Note
Some additional files that do not follow the `Request`/`Response` pattern:

- **Chats**: AnswerOfInvitation.cs, InviteUserNotification.cs
