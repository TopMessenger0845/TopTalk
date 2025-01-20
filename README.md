# Command Protocol Documentation
���� �������� ������������� ������ ����� ���� ��������� ������, ��������������� �� ����������, � ����� �������� ��������������� ��� ������ Request � Response. ��� �������� �������������, �������� ������ ��� �������� ���������� ��������� � Message Builder Template.

## 0. ��������� �������� ��� �������������
��� ���� ����� ������ ������������� ������ **Message Builder Template**, ��������� ��������� ����:

1. �������� **��������� ������ ������������**:
   - � Visual Studio �������� ���� **�������� (Tools)**.
   - �������� **��������� ������** � ����� **������������**.

2. � ��������� ������ ��������� ��������� ������� ��� ��������� ��������:

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
��������� �������������� �����, ������� �� ������� ������� `Request`/`Response`:

- **Chats**: AnswerOfInvitation.cs, InviteUserNotification.cs
