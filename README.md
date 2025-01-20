# Command Protocol Documentation
Этот документ предоставляет полный обзор всех доступных команд, сгруппированных по категориям, а также описание соответствующих пар команд Request и Response. Для удобства разработчиков, добавлен шаблон для быстрого построения сообщений — Message Builder Template.

## 0. Установка шаблонов для разработчиков
Для того чтобы быстро импортировать шаблон **Message Builder Template**, выполните следующие шаги:

1. Откройте **Командную строку разработчика**:
   - В Visual Studio откройте меню **Средства (Tools)**.
   - Выберите **Командная строка** и затем **Разработчика**.

2. В командной строке выполните следующую команду для установки шаблонов:

   ```powershell
   powershell.exe -NoProfile -ExecutionPolicy Bypass -File "./TopTalk.Core/Scripts/InstallTemplates.ps1"


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
Некоторые дополнительные файлы, которые не следуют шаблону `Request`/`Response`:

- **Chats**: AnswerOfInvitation.cs, InviteUserNotification.cs
