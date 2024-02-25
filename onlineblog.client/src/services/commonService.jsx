const ACCOUNT_URL = 'account';
const TOKEN_URL = 'token';
const USERS_URL = 'users';
const SUBSCRIBE_URL = 'subscribe';
const LIKES_URL = 'likes';
const NEWS_URL = 'news';
const BASE_URL = 'login';

function sendRequest(url, baseUrl, successAction, errorAction) {
    fetch(url)
        .then(response => {
            if (response.status === 401) {
                // Ошибка авторизации 401
                window.location.href = baseUrl; // Редирект на базовый URL
            } else {
                // Обработка успешного ответа
                successAction();
            }
        })
        .catch(error => {
            // Обработка других ошибок
            errorAction();
        });

}

export async function getToken(login, password) {
    const url = TOKEN_URL;
    const token = await sendAuthenticatedRequest(url, 'POST', login, password);
    console.log(token);
}

async function sendAuthenticatedRequest(url, method, username, password, data) {
    // Создаем объект заголовков
    var headers = new Headers();

    // Устанавливаем заголовок авторизации
    headers.set('Authorization', 'Basic ' + btoa(username + ':' + password));

    // Устанавливаем тип контента (если есть данные)
    if (data) {
        headers.set('Content-Type', 'application/json');
    }

    // Создаем объект параметров запроса
    var requestOptions = {
        method: method,
        headers: headers,
        body: data ? JSON.stringify(data) : undefined
    };

    // Отправляем запрос с помощью fetch
    var resultFetch = await fetch(url, requestOptions);
    if (resultFetch.ok) {
        // Запрос успешно выполнен
        const result = await resultFetch.json();
        return result
    } else {
        // Произошла ошибка при выполнении запроса
        throw new Error('Ошибка ' + resultFetch.status + ': ' + resultFetch.statusText);
    };
}