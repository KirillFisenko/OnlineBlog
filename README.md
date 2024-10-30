**OnlineBlog** — это веб-приложение онлайн-блога, где пользователи могут заводить свои страницы и делиться новостями (постами), разработанное с использованием ASP.NET Core для серверной части и React для клиентской части. 
В качестве базы данных используется MS SQL и LiteDB, а для взаимодействия с базой данных применяется Entity Framework (EF).

## Технологии
- **Серверная часть**: ASP.NET Core
- **Клиентская часть**: React
- **База данных**: MS SQL и LiteDB
- **ORM**: Entity Framework (EF)
  
## Основные функции
- **Аккаунты**: регистрация новых пользователей, просмотр профиля, редактирование профиля, удаление пользователя.
- **Лайки**: поставить лайк посту.
- **Посты**: создать пост, получить посты на основе подписок, редактировать пост, получить посты пользователя, удалить пост.
    
*Функционал на фронте реализован частично.

<img src="https://github.com/user-attachments/assets/b264d9dc-5396-485a-9bdd-cad6c21f1a7b" width="310" height="350">

<img src="https://github.com/user-attachments/assets/e598d909-0506-43b0-958e-e9bc736c18ff" width="310" height="350">

<img src="https://github.com/user-attachments/assets/bf125277-13da-44c2-80d8-6819202bc030" width="310" height="350">

<img src="https://github.com/user-attachments/assets/ed66b6d5-01a3-49ca-b8bd-bb9d36110df8" width="310" height="350">

## Установка и запуск
1) Выбрать мыльтизапуск двух приложений:
![image](https://github.com/user-attachments/assets/ab98977d-f0f1-4454-beab-24e1827b6dd1)

2) Обновить путь до xml документации:
![image](https://github.com/user-attachments/assets/725f9f90-c314-4486-932a-240b7417ad3d)

3) Скопировать себе файл по пути:  
C:\Users\1\Source\Repos\OnlineBlog\onlineblog.client\.vscode
[launch.json](https://github.com/user-attachments/files/17526324/launch.json)

4) Перейдите в директорию клиентской части и установите необходимые зависимости:
```
npm install
```

